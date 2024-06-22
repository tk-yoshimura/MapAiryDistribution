using MapAiryExpected;
using MultiPrecision;
using MultiPrecisionRootFinding;

namespace MapAiryEvalExpected {
    internal class ExpectedQuantilePlusScaledN16 {
        static void Main_() {
            using (StreamWriter sw = new("../../../../results_disused/quantile_precision148_plus_scaled.csv")) {
                sw.WriteLine("u:=-log2(p),v:=cquantile(p)*p^(2/3)");

                MultiPrecision<Pow2.N16> x = "0.1837312561018422068508368747726198241616025315632555412006096259699";

                for (MultiPrecision<Pow2.N16> u0 = 2; u0 < 1024; u0 *= 2) {
                    for (MultiPrecision<Pow2.N16> u = u0; u < u0 * 2; u += u0 / (u0 < 4 ? 65536 : 32768)) {
                        MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Pow2(-u);

                        x = NewtonRaphsonFinder<Pow2.N16>.RootFind(
                            x => (CDFN16.Value(x, complementary: true) - p, -PDFN16.Value(x)),
                            x0: x, overshoot_decay: true, iters: 256
                        );

                        MultiPrecision<Pow2.N16> v = x * MultiPrecision<Pow2.N16>.Square(MultiPrecision<Pow2.N16>.Cbrt(p));

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
