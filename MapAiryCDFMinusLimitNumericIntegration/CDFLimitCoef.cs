using MultiPrecision;
using MultiPrecisionSeriesAcceleration;

namespace MapAiryCDFMinusLimitNumericIntegration {
    internal class CDFLimitCoef {
        static void Main_() {
            List<(MultiPrecision<Pow2.N32> x, MultiPrecision<Pow2.N32> y)> scaled_cdf = [];

            using (StreamReader sr = new("../../../../results_disused/cdf_lower_precision160.csv")) {
                sr.ReadLine();

                while (!sr.EndOfStream) {
                    string? line = sr.ReadLine();

                    if (string.IsNullOrEmpty(line)) {
                        continue;
                    }

                    string[] line_split = line.Split(',');

                    if (line_split[0].Contains('.')) {
                        continue;
                    }

                    MultiPrecision<Pow2.N32> x = line_split[0];

                    if (MultiPrecision<Pow2.N32>.Ldexp(x, -x.Exponent) != -1) {
                        continue;
                    }

                    MultiPrecision<Pow2.N32> y = line_split[^1];

                    scaled_cdf.Add((x, y));
                }
            }

            scaled_cdf.Reverse();

            WynnEpsilonAccelerator<Pow2.N32> accelerator = new();

            for (int i = 0; i < scaled_cdf.Count; i++) {
                accelerator.Append(scaled_cdf[i].y);

                Console.WriteLine($"{accelerator.LastValue:e20}");
                Console.WriteLine($"{accelerator.Error:e10}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
