﻿using MapAiryExpected;
using MapAiryPadeApprox;
using MultiPrecision;

namespace MapAiryEvalPadeApprox {
    internal class EvalPDFPadeN16 {
        static void Main_() {
            MultiPrecision<Pow2.N16> max_err = "1e-151";

            using (StreamWriter sw = new("../../../../results_disused/pdf_pade_eval.csv")) {
                sw.WriteLine("x,pdf(x),pdf_pade(x),err");

                for (double x0 = -256; x0 < -1; x0 /= 2) {
                    for (double x = x0; x < x0 / 2; x += -x0 / 512) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);
                        MultiPrecision<Pow2.N16> y_approx = PDFPadeN16.Value(x);
                        MultiPrecision<Pow2.N16> err = MultiPrecision<Pow2.N16>.Abs(y - y_approx) / y;

                        Console.WriteLine($"{x}\n{y}\n{y_approx}\n{err:e10}");
                        sw.WriteLine($"{x},{y},{y_approx},{err:e10}");

                        if (err >= max_err) {
                            Console.ReadLine();
                        }
                    }
                }

                for (double x = -1; x < 1; x += 1d / 1024) {
                    MultiPrecision<Pow2.N16> y = PDFN16.Value(x);
                    MultiPrecision<Pow2.N16> y_approx = PDFPadeN16.Value(x);
                    MultiPrecision<Pow2.N16> err = MultiPrecision<Pow2.N16>.Abs(y - y_approx) / y;

                    Console.WriteLine($"{x}\n{y}\n{y_approx}\n{err:e10}");
                    sw.WriteLine($"{x},{y},{y_approx},{err:e10}");

                    if (err >= max_err) {
                        Console.ReadLine();
                    }
                }

                for (double x0 = 1; x0 < 4294967296; x0 *= 2) {
                    for (double x = x0; x < x0 * 2; x += x0 / 512) {
                        MultiPrecision<Pow2.N16> y = PDFN16.Value(x);
                        MultiPrecision<Pow2.N16> y_approx = PDFPadeN16.Value(x);
                        MultiPrecision<Pow2.N16> err = MultiPrecision<Pow2.N16>.Abs(y - y_approx) / y;

                        Console.WriteLine($"{x}\n{y}\n{y_approx}\n{err:e10}");
                        sw.WriteLine($"{x},{y},{y_approx},{err:e10}");

                        if (err >= max_err) {
                            Console.ReadLine();
                        }
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
