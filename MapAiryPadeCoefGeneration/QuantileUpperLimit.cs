﻿using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionCurveFitting;

namespace MapAiryPadeCoefGeneration {
    internal class QuantileUpperLimit {
        static void Main_() {
            List<(MultiPrecision<Pow2.N64> pmin, MultiPrecision<Pow2.N64> pmax, MultiPrecision<Pow2.N64> limit_range)> ranges = [];

            for (MultiPrecision<Pow2.N64> pmin = 16; pmin < 512; pmin *= 2) {
                ranges.Add((pmin, pmin * 2, pmin / 128));
            }
            ranges.Add((512, 768, 0.25));

            List<(MultiPrecision<Pow2.N64> p, MultiPrecision<Pow2.N64> y)> expecteds = [];

            using (BinaryReader sr = new(File.OpenRead("../../../../results_disused/quantile_upper_precision230_scaled.bin"))) {
                while (sr.BaseStream.Position < sr.BaseStream.Length) {
                    MultiPrecision<Pow2.N64> p = sr.ReadMultiPrecision<N24>().Convert<Pow2.N64>();
                    MultiPrecision<Pow2.N64> x = sr.ReadMultiPrecision<N24>().Convert<Pow2.N64>();

                    expecteds.Add((p, x));
                }
            }

            using (StreamWriter sw = new("../../../../results_disused/pade_quantile_upper_precision150_scaled_exp16.csv")) {
                bool approximate(MultiPrecision<Pow2.N64> pmin, MultiPrecision<Pow2.N64> pmax) {
                    Console.WriteLine($"[{pmin}, {pmax}]");

                    List<(MultiPrecision<Pow2.N64> p, MultiPrecision<Pow2.N64> y)> expecteds_range
                        = expecteds.Where(item => item.p >= pmin && item.p <= pmax).ToList();

                    Console.WriteLine($"expecteds {expecteds_range.Count} samples");

                    Vector<Pow2.N64> xs = expecteds_range.Select(item => item.p - pmin).ToArray();
                    Vector<Pow2.N64> ys = expecteds_range.Select(item => item.y).ToArray();

                    MultiPrecision<Pow2.N64> y0 = ys[0];

                    for (int coefs = 5; coefs <= expecteds_range.Count / 2 && coefs <= 128; coefs++) {
                        foreach ((int m, int n) in CurveFittingUtils.EnumeratePadeDegree(coefs, 2)) {
                            PadeFitter<Pow2.N64> pade = new(xs, ys, m, n, intercept: y0);

                            Vector<Pow2.N64> param = pade.ExecuteFitting();

                            MultiPrecision<Pow2.N64> max_rateerr = CurveFittingUtils.MaxRelativeError(ys, pade.FittingValue(xs, param));

                            Console.WriteLine($"m={m},n={n}");
                            Console.WriteLine($"{max_rateerr:e20}");

                            Console.WriteLine($"mcount: {param.Count(item => item.val.Sign != Sign.Plus)}");

                            if (coefs > 8 && max_rateerr > "1e-13") {
                                return false;
                            }

                            if (coefs > 16 && max_rateerr > "1e-26") {
                                return false;
                            }

                            if (coefs > 32 && max_rateerr > "1e-52") {
                                return false;
                            }

                            if (max_rateerr > "1e-50") {
                                coefs += 16;
                                break;
                            }

                            if (max_rateerr > "1e-100") {
                                coefs += 8;
                                break;
                            }

                            if (max_rateerr > "1e-135") {
                                coefs += 4;
                                break;
                            }

                            if (max_rateerr > "1e-140") {
                                coefs += 2;
                                break;
                            }

                            if (max_rateerr > "1e-145") {
                                break;
                            }

                            if (max_rateerr < "1e-150" &&
                                !CurveFittingUtils.HasLossDigitsPolynomialCoef(param[..m], 0, pmax - pmin) &&
                                !CurveFittingUtils.HasLossDigitsPolynomialCoef(param[m..], 0, pmax - pmin)) {

                                sw.WriteLine($"p=[{pmin},{pmax}]");
                                sw.WriteLine($"m={m},n={n}");
                                sw.WriteLine($"expecteds {expecteds_range.Count} samples");
                                sw.WriteLine($"sample rate {(double)expecteds_range.Count / (param.Dim - 1)}");

                                sw.WriteLine("numer");
                                foreach (var (_, val) in param[..m]) {
                                    sw.WriteLine($"{val:e155}");
                                }
                                sw.WriteLine("denom");
                                foreach (var (_, val) in param[m..]) {
                                    sw.WriteLine($"{val:e155}");
                                }

                                sw.WriteLine("coef");
                                foreach ((var numer, var denom) in CurveFittingUtils.EnumeratePadeCoef(param, m, n)) {
                                    sw.WriteLine($"(\"{numer:e155}\", \"{denom:e155}\"),");
                                }

                                sw.WriteLine("relative err");
                                sw.WriteLine($"{max_rateerr:e20}");
                                sw.Flush();

                                return true;
                            }
                        }
                    }

                    return false;
                }

                Segmenter<Pow2.N64> segmenter = new(ranges, approximate);
                segmenter.Execute();

                foreach ((var xmin, var xmax, bool is_successs) in segmenter.ApproximatedRanges) {
                    sw.WriteLine($"[{xmin},{xmax}],{(is_successs ? "OK" : "NG")}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
