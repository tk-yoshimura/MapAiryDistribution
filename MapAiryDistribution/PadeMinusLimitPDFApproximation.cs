using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionCurveFitting;

namespace MapAiryDistribution {
    internal class PDFMinusLimitPadeApproximation {
        static void Main_() {
            List<(MultiPrecision<Pow2.N64> xmin, MultiPrecision<Pow2.N64> xmax, MultiPrecision<Pow2.N64> limit_range)> ranges = [
                (0, 1, 1 / 4096d),
                (1, 2, 1 / 4096d),
                (2, 4, 1 / 4096d),
                (4, 8, 1 / 4096d),
                (8, 10, 1 / 4096d),
            ];

            List<(MultiPrecision<Pow2.N64> x, MultiPrecision<Pow2.N64> y)> expecteds = [];

            using (BinaryReader sr = new(File.OpenRead("../../../../results_disused/pdf_precision150_large.bin"))) {
                while (sr.BaseStream.Position < sr.BaseStream.Length) {
                    MultiPrecision<Pow2.N64> x = -sr.ReadMultiPrecision<Pow2.N16>().Convert<Pow2.N64>();
                    MultiPrecision<Pow2.N64> y = sr.ReadMultiPrecision<Pow2.N16>().Convert<Pow2.N64>();

                    if (x < ranges[0].xmin) {
                        continue;
                    }

                    if (x > ranges[^1].xmax) {
                        continue;
                    }

                    MultiPrecision<Pow2.N64> u = MultiPrecision<Pow2.N64>.Log2(y);

                    expecteds.Add((x, u));
                }
            }

            expecteds.Reverse();

            using (StreamWriter sw = new("../../../../results_disused/pade_minuslimitpdf_precision150.csv")) {
                bool approximate(MultiPrecision<Pow2.N64> xmin, MultiPrecision<Pow2.N64> xmax) {
                    Console.WriteLine($"[{xmin}, {xmax}]");

                    List<(MultiPrecision<Pow2.N64> x, MultiPrecision<Pow2.N64> y)> expecteds_range
                        = expecteds.Where(item => item.x >= xmin && item.x <= xmax).ToList();

                    MultiPrecision<Pow2.N64> y0 = expecteds_range.Where(item => item.x == xmin).First().y;

                    Vector<Pow2.N64> xs = expecteds_range.Select(item => item.x - xmin).ToArray();
                    Vector<Pow2.N64> ys = expecteds_range.Select(item => item.y).ToArray();

                    for (int coefs = 5; coefs <= expecteds_range.Count / 2 && coefs <= 128; coefs++) {
                        foreach ((int m, int n) in CurveFittingUtils.EnumeratePadeDegree(coefs, 2)) {
                            PadeFitter<Pow2.N64> pade = new(xs, ys, m, n, intercept: y0);

                            Vector<Pow2.N64> param = pade.ExecuteFitting();
                            Vector<Pow2.N64> errs = pade.Error(param);

                            MultiPrecision<Pow2.N64> max_rateerr = CurveFittingUtils.MaxRelativeError(ys, pade.FittingValue(xs, param));

                            Console.WriteLine($"m={m},n={n}");
                            Console.WriteLine($"{max_rateerr:e20}");

                            if (coefs > 8 && max_rateerr > "1e-12") {
                                return false;
                            }

                            if (coefs > 32 && max_rateerr > "1e-50") {
                                return false;
                            }

                            if (max_rateerr > "1e-145") {
                                break;
                            }

                            if (max_rateerr < "1e-150" &&
                                !CurveFittingUtils.HasLossDigitsPolynomialCoef(param[..m], 0, xmax - xmin) &&
                                !CurveFittingUtils.HasLossDigitsPolynomialCoef(param[m..], 0, xmax - xmin)) {

                                sw.WriteLine($"x=[{xmin},{xmax}]");
                                sw.WriteLine($"m={m},n={n}");
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
