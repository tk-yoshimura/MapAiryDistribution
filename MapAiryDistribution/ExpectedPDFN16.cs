﻿using MultiPrecision;

namespace MapAiryDistribution {
    internal class ExpectedPDFN16 {
        static void Main() {
            using (StreamWriter sw = new("../../../../results/pdf_precision150.csv")) {
                sw.WriteLine("x,pdf(x)");

                for (MultiPrecision<Pow2.N16> x0 = 16; x0 > 1; x0 /= 2) {
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

                for (MultiPrecision<Pow2.N16> x0 = 1; x0 < 4096; x0 *= 2) {
                    for (MultiPrecision<Pow2.N16> x = x0, h = x0 / 512; x < x0 * 2; x += h) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                        Console.WriteLine($"{x}\n{y}");
                        sw.WriteLine($"{x},{y}");
                    }
                }

                for (MultiPrecision<Pow2.N16> x = MultiPrecision<Pow2.N16>.Ldexp(1, 12); x.Exponent <= 1024; x *= 2) {
                    MultiPrecision<Pow2.N16> y = PDFN16.Value(x);

                    Console.WriteLine($"{x}\n{y}");
                    sw.WriteLine($"2^{x.Exponent},{y}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
