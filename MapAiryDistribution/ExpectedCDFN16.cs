using MultiPrecision;
using MultiPrecisionIntegrate;

namespace MapAiryDistribution {
    internal class ExpectedCDFN16 {
        static void Main() {
            using (StreamWriter sw = new("../../../../results/cdf_precision150.csv")) {
                sw.WriteLine("x,cdf(x),ccdf(x)");

                for (MultiPrecision<Pow2.N16> x0 = 8; x0 > 1; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 512; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: false);

                        Console.WriteLine($"{x}\n{y}");
                        sw.WriteLine($"{x},{y},{1 - y}");
                    }
                }

                for (MultiPrecision<Pow2.N16> x = -1, h = 1 / 1024d; x < 0; x += h) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: false);

                    Console.WriteLine($"{x}\n{y}");
                    sw.WriteLine($"{x},{y},{1 - y}");
                }

                for (MultiPrecision<Pow2.N16> x = 0, h = 1 / 1024d; x < 1; x += h) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: true);

                    Console.WriteLine($"{x}\n{y}");
                    sw.WriteLine($"{x},{1 - y},{y}");
                }

                for (MultiPrecision<Pow2.N16> x0 = 1; x0 < 4096; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 512; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: true);

                        Console.WriteLine($"{x}\n{y}");
                        sw.WriteLine($"{x},{1 - y},{y}");
                    }
                }

                for (MultiPrecision<Pow2.N16> x = MultiPrecision<Pow2.N16>.Ldexp(1, 12); x.Exponent <= 1024; x *= 2) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: true);

                    Console.WriteLine($"{x}\n{y}");
                    sw.WriteLine($"2^{x.Exponent},{1 - y},{y}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
