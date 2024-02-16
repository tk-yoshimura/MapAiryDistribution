using MapAiryExpected;
using MultiPrecision;

namespace MapAiryEvalExpected {
    internal class ExpectedPDFN16 {
        static void Main_() {
            using (BinaryWriter sw = new(File.Open("../../../../results_disused/pdf_precision150_large.bin", FileMode.Create))) {

                for (MultiPrecision<Pow2.N16> x0 = 16; x0 > 1; x0 /= 2) {
                    for (MultiPrecision<Pow2.N16> x = -x0, h = x0 / 4096; x < -x0 / 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                    }
                }

                for (MultiPrecision<Pow2.N16> x = -1, h = 1 / 8192d; x < 1; x += h) {
                    MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                    Console.WriteLine($"{x}\n{y}");
                    sw.Write(x);
                    sw.Write(y);
                }

                for (MultiPrecision<Pow2.N16> x0 = 1; x0.Exponent < 1024; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 4096; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.Write(x);
                        sw.Write(y);
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
