using MapAiryExpected;
using MultiPrecision;

namespace MapAiryEvalExpected {
    internal class ExpectedCDFN16 {
        static void Main_() {
            using (BinaryWriter sw = new(File.Open("../../../../results_disused/cdf_precision150_large.bin", FileMode.Create))) {

                for (MultiPrecision<Pow2.N16> x = -8.75, h = 1 / 512d; x < -8; x += h) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: false);

                    Console.WriteLine($"{x}\n{y}");
                    sw.Write(x);
                    sw.Write(y);
                    sw.Write(1 - y);
                }

                for (MultiPrecision<Pow2.N16> x0 = 8; x0 > 1; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 4096; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: false);

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                        sw.Write(1 - y);
                    }
                }

                for (MultiPrecision<Pow2.N16> x = -1, h = 1 / 8192d; x < 0; x += h) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: false);

                    Console.WriteLine($"{x}\n{y}");
                    sw.Write(x);
                    sw.Write(y);
                    sw.Write(1 - y);
                }

                for (MultiPrecision<Pow2.N16> x = 0, h = 1 / 8192d; x < 1; x += h) {
                    MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: true);

                    Console.WriteLine($"{x}\n{y}");
                    sw.Write(x);
                    sw.Write(1 - y);
                    sw.Write(y);
                }

                for (MultiPrecision<Pow2.N16> x0 = 1; x0.Exponent < 1024; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 4096; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = CDFN16.Value(x, complementary: true);

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(1 - y);
                        sw.Write(y);
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
