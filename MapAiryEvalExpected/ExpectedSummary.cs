using MapAiryExpected;
using MultiPrecision;

namespace MapAiryEvalExpected {
    internal class ExpectedSummary {
        static void Main() {
            using (StreamWriter sw = new("../../../../results/pdf_precision150.csv")) {
                sw.WriteLine("x,pdf(x)");

                for (MultiPrecision<Pow2.N16> x0 = 256; x0 > 1; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 512; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.WriteLine($"{x},{y}");
                    }
                }

                for (MultiPrecision<Pow2.N16> x = -1, h = 1 / 1024d; x < 1; x += h) {
                    MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                    Console.WriteLine($"{x}\n{y}");
                    sw.WriteLine($"{x},{y}");
                }

                for (MultiPrecision<Pow2.N16> x0 = 1; x0.Exponent < 12; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 512; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.WriteLine($"{x},{y}");
                    }
                }

                for (int exp = 12; exp <= 1024; exp++) {
                    MultiPrecision<Pow2.N16> x = MultiPrecision<Pow2.N16>.Ldexp(1, exp);
                    MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                    Console.WriteLine($"{x}\n{y}");
                    sw.WriteLine($"2^{exp},{y}");
                }
            }

            using (StreamWriter sw = new("../../../../results/cdf_precision150.csv")) {
                sw.WriteLine("x,cdf(x),ccdf(x)");

                for (MultiPrecision<Pow2.N16> x0 = 256; x0 > 1; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 512; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = CDFN16.Value(x);
                        MultiPrecision<Pow2.N16> yc = CDFN16.Value(x, complementary: true);

                        Console.WriteLine($"{x}\n{y}\n{yc}");
                        sw.WriteLine($"{x},{y},{yc}");
                    }
                }

                for (MultiPrecision<Pow2.N16> x = -1, h = 1 / 1024d; x < 1; x += h) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x);
                    MultiPrecision<Pow2.N16> yc = CDFN16.Value(x, complementary: true);

                    Console.WriteLine($"{x}\n{y}\n{yc}");
                    sw.WriteLine($"{x},{y},{yc}");
                }

                for (MultiPrecision<Pow2.N16> x0 = 1; x0.Exponent < 12; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 512; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = CDFN16.Value(x);
                        MultiPrecision<Pow2.N16> yc = CDFN16.Value(x, complementary: true);

                        Console.WriteLine($"{x}\n{y}\n{yc}");
                        sw.WriteLine($"{x},{y},{yc}");
                    }
                }

                for (int exp = 12; exp <= 1024; exp++) {
                    MultiPrecision<Pow2.N16> x = MultiPrecision<Pow2.N16>.Ldexp(1, exp);
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x);
                    MultiPrecision<Pow2.N16> yc = CDFN16.Value(x, complementary: true);

                    Console.WriteLine($"{x}\n{y}\n{yc}");
                    sw.WriteLine($"2^{exp},{y},{yc}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
