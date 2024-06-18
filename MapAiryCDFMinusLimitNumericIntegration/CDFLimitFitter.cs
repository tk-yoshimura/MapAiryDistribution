using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionCurveFitting;

namespace MapAiryCDFMinusLimitNumericIntegration {
    internal class CDFLimitFitter {
         static void Main() {
            MultiPrecision<Pow2.N64> c = 1 / (2 * MultiPrecision<Pow2.N64>.Sqrt(MultiPrecision<Pow2.N64>.PI));

            List<(MultiPrecision<Pow2.N64> x, MultiPrecision<Pow2.N64> y)> scaled_cdf = [];

            using (StreamWriter sw = new("../../../../results_disused/cdf_lower_precision160_scaled.csv")) {
                using (StreamReader sr = new("../../../../results_disused/cdf_lower_precision160.csv")) {
                    sr.ReadLine();

                    while (!sr.EndOfStream) {
                        string? line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line)) {
                            continue;
                        }

                        string[] line_split = line.Split(',');

                        MultiPrecision<Pow2.N64> x = line_split[0];
                        MultiPrecision<Pow2.N64> u = MultiPrecision<Pow2.N64>.Cube(1 / -x);

                        MultiPrecision<Pow2.N64> y = line_split[^1] / c;

                        scaled_cdf.Add((u, y));

                        sw.WriteLine($"{u:e20},{y:e20}");
                    }
                }
            }

            scaled_cdf = scaled_cdf[..20000].ToList();

            PolynomialFitter<Pow2.N64> fitter = new(
                scaled_cdf.Select(v => v.x).ToArray(), scaled_cdf.Select(v => v.y).ToArray(), 34, intercept: 1
            );

            Vector<Pow2.N64> coef = fitter.ExecuteFitting(weights: scaled_cdf.Select(v => 1 / v.x).ToArray());

            foreach ((int n, MultiPrecision<Pow2.N64> val) in coef) {
                MultiPrecision<Pow2.N64> f = val * MultiPrecision<Pow2.N64>.Pow(48, n) * MultiPrecision<Pow2.N64>.Gamma(n + 1);

                Console.WriteLine(MultiPrecision<Pow2.N64>.Round(f));

                MultiPrecision<Pow2.N64> d = f - MultiPrecision<Pow2.N64>.Round(f);

                if (MultiPrecision<Pow2.N64>.Abs(d) > 1e-6) {
                    break;
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
