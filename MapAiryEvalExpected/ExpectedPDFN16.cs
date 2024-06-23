using MapAiryExpected;
using MultiPrecision;

namespace MapAiryEvalExpected {
    internal class ExpectedPDFN16 {
        static void Main_() {
            using (BinaryWriter sw = new(File.Open("../../../../results_disused/pdf_limit_precision150.bin", FileMode.Create))) {

                for (MultiPrecision<Pow2.N16> x0 = 256; x0 > 8; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 16384; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFMinusLimit<Pow2.N16, N24>.Value(x, exp_scale: false)
                            * MultiPrecision<Pow2.N16>.Sqrt(MultiPrecision<Pow2.N16>.PI / -x) / 2;

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                    }
                }

                for (MultiPrecision<Pow2.N16> x0 = 8; x0 >= 0.5; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 16384; x < -x0 / 2 && x <= -0.5; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x) * MultiPrecision<N24>.Exp(-4 * MultiPrecision<N24>.Cube(x.Convert<N24>()) / 3).Convert<Pow2.N16>()
                            * MultiPrecision<Pow2.N16>.Sqrt(MultiPrecision<Pow2.N16>.PI / -x) / 2;

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                    }
                }
            }

            using (BinaryWriter sw = new(File.Open("../../../../results_disused/pdf_precision150.bin", FileMode.Create))) {

                for (MultiPrecision<Pow2.N16> x0 = 16; x0 > 1; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 16384; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                    }
                }

                for (MultiPrecision<Pow2.N16> x = -1, h = 1 / 32768d; x < 1; x += h) {
                    MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                    Console.WriteLine($"{x}\n{y}");
                    sw.Write(x);
                    sw.Write(y);
                }

                for (MultiPrecision<Pow2.N16> x0 = 1; x0.Exponent < 32; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 16384; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                    }
                }

                for (int xexp = 32; xexp < 1024; xexp *= 2) {
                    for (MultiPrecision<Pow2.N16> x0 = MultiPrecision<Pow2.N16>.Ldexp(1, xexp); x0.Exponent < xexp * 2; x0 *= 2) {
                        for (MultiPrecision<Pow2.N16> x = x0, h = x0 / (262144 / xexp); x < x0 * 2; x += h) {
                            MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                            Console.WriteLine($"{x}\n{y}");
                            sw.Write(x);
                            sw.Write(y);
                        }
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
