using MapAiryExpected;
using MultiPrecision;

namespace MapAiryCDFMinusLimitNumericIntegration {
    class CDFGenExpected {
        static void Main_() {
            List<(MultiPrecision<Pow2.N32> x0, MultiPrecision<Pow2.N32> x1, MultiPrecision<Pow2.N32> integral)> integrals = [];

            using (StreamReader sr = new("../../../../results_disused/cdfintegrate_precision160.csv")) {
                sr.ReadLine();
                while (!sr.EndOfStream) {
                    string? line = sr.ReadLine();

                    if (string.IsNullOrWhiteSpace(line)) {
                        break;
                    }

                    string[] line_split = line.Split(',');

                    MultiPrecision<Pow2.N32> x0 = line_split[0];
                    MultiPrecision<Pow2.N32> x1 = line_split[1];
                    MultiPrecision<Pow2.N32> integral = line_split[2];

                    integrals.Add((x0, x1, integral));
                }
            }

            integrals.Reverse(); 


            using (StreamWriter sw_lower = new("../../../../results_disused/cdf_lower_precision160.csv")) {
                sw_lower.WriteLine("x,cdf,logcdf,cdf*exp(-4/3 * x^3)*(-x)^(3/2)");

                MultiPrecision<Pow2.N32> sum_cdf = 0;
    
                foreach ((MultiPrecision<Pow2.N32> x0, MultiPrecision<Pow2.N32> x1, MultiPrecision<Pow2.N32> integral) in integrals) {
                    sum_cdf += integral;

                    if (x1 < -128) {
                        continue;
                    }

                    MultiPrecision<Pow2.N32> log_cdf = MultiPrecision<Pow2.N32>.Log(sum_cdf);
                    MultiPrecision<Pow2.N32> scaled_cdf = sum_cdf
                        * MultiPrecision<Pow2.N32>.Exp(MultiPrecision<Pow2.N32>.Div(-4, 3) * x1 * x1 * x1)
                        * MultiPrecision<Pow2.N32>.Cube(MultiPrecision<Pow2.N32>.Sqrt(-x1));

                    Console.WriteLine($"{x1},{sum_cdf:e8},{scaled_cdf:e8}");
                    sw_lower.WriteLine($"{x1},{sum_cdf:e161},{log_cdf:e160},{scaled_cdf:e160}");

                    if (x1 >= 8) {
                        break;
                    }
                }

                for (MultiPrecision<Pow2.N16> h = 1d / 2048, x0 = 8, x1 = 4; x1 >= 1; h /= 2, x0 /= 2, x1 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0 - h; x >= x1; x -= h) {
                        MultiPrecision<Pow2.N16> cdf = CDFN16.Value(-x);
                        MultiPrecision<Pow2.N16> log_cdf = MultiPrecision<Pow2.N16>.Log(cdf);
                        MultiPrecision<Pow2.N16> scaled_cdf = cdf
                            * MultiPrecision<Pow2.N16>.Exp(MultiPrecision<Pow2.N16>.Div(4, 3) * x * x * x)
                            * MultiPrecision<Pow2.N16>.Cube(MultiPrecision<Pow2.N16>.Sqrt(x));

                        Console.WriteLine($"-{x},{cdf:e8},{scaled_cdf:e8}");
                        sw_lower.WriteLine($"-{x},{cdf},{log_cdf},{scaled_cdf}");
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}