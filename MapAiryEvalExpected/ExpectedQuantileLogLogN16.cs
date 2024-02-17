using MapAiryExpected;
using MultiPrecision;
using MultiPrecisionRootFinding;

namespace MapAiryEvalExpected {
    internal class ExpectedQuantileLogLogN16 {
        static void Main_() {
            //using (StreamWriter sw = new("../../../../results_disused/quantile_precision148.csv")) {
            //    sw.WriteLine("u:=-log2(p),x:=quantile(p)");

            //    MultiPrecision<Pow2.N16> x = 0;

            //    for (MultiPrecision<Pow2.N16> u0 = 1; u0 < 1024; u0 *= 2) {
            //        for (MultiPrecision<Pow2.N16> u = u0; u < u0 * 2; u += u0 / (u0 < 4 ? 65536 : 32768)) {
            //            MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Pow2(-u);

            //            x = NewtonRaphsonFinder<Pow2.N16>.RootFind(x => (CDFPadeN16.Value(x, complementary: false) - p, PDFPadeN16.Value(x)), 
            //                x, iters: 256
            //            );

            //            Console.WriteLine($"{u}\n{x}");

            //            sw.WriteLine($"{u},{x}");
            //        }
            //    }
            //}

            //using (StreamWriter sw = new("../../../../results_disused/cquantile_precision148.csv")) {
            //    sw.WriteLine("u:=-log2(p),x:=cquantile(p)");

            //    MultiPrecision<Pow2.N16> x = 0;

            //    for (MultiPrecision<Pow2.N16> u0 = 1; u0 < 1024; u0 *= 2) {
            //        for (MultiPrecision<Pow2.N16> u = u0; u < u0 * 2; u += u0 / (u0 < 4 ? 65536 : 32768)) {
            //            MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Pow2(-u);

            //            x = NewtonRaphsonFinder<Pow2.N16>.RootFind(x => (CDFPadeN16.Value(x, complementary: true) - p, -PDFPadeN16.Value(x)), 
            //                x, iters: 256
            //            );

            //            Console.WriteLine($"{u}\n{x}");

            //            sw.WriteLine($"{u},{x}");
            //        }
            //    }
            //}

            using (StreamWriter sw = new("../../../../results_disused/quantile_precision148_loglog_2.csv")) {
                sw.WriteLine("u:=-log2(p),v:=log2(cquantile(p) + 1)");

                MultiPrecision<Pow2.N16> x = "0.1837312561018422068508368747726198241616025";

                for (MultiPrecision<Pow2.N16> u0 = 2; u0 < 2048; u0 *= 2) {
                    for (MultiPrecision<Pow2.N16> u = u0; u < u0 * 2; u += u0 / (u0 < 4 ? 65536 : 32768)) {
                        MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Pow2(-u);

                        x = NewtonRaphsonFinder<Pow2.N16>.RootFind(
                            x => (CDFN16.Value(x, complementary: true) - p, -PDFN16.Value(x)),
                            x0: x, overshoot_decay: true, iters: 256
                        );

                        MultiPrecision<Pow2.N16> v = MultiPrecision<Pow2.N32>.Log2(x.Convert<Pow2.N32>() + 1).Convert<Pow2.N16>();

                        Console.WriteLine($"{u}\n{v}\n");

                        sw.WriteLine($"{u},{v}");
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
