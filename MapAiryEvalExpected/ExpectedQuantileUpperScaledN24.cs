using MapAiryExpected;
using MultiPrecision;
using MultiPrecisionRootFinding;

namespace MapAiryEvalExpected {
    internal class ExpectedQuantileUpperScaledN24 {
        static void Main() {
            using (BinaryWriter sw = new(File.Open("../../../../results_disused/quantile_upper_precision230_scaled.bin", FileMode.Create))) {
                MultiPrecision<N24> x = "3.361262993547751e2";

                for (MultiPrecision<N24> u0 = 16; u0 <= 1024; u0 *= 2) {
                    for (MultiPrecision<N24> u = u0; u < u0 * 2 && u <= 1024; u += u0 / (u0 < 4 ? 65536 : 32768)) {
                        MultiPrecision<N24> p = MultiPrecision<N24>.Pow2(-u);

                        x = NewtonRaphsonFinder<N24>.RootFind(
                            x => (CDFPlusLimit<N24, Pow2.N32>.Value(x, complementary: true) - p, -PDFPlusLimit<N24, Pow2.N32>.Value(x)),
                            x0: x, overshoot_decay: true, iters: 256
                        );

                        MultiPrecision<N24> v = x * MultiPrecision<N24>.Square(MultiPrecision<N24>.Cbrt(p));

                        Console.WriteLine($"{u}\n{v}\n");

                        sw.Write(u);
                        sw.Write(v);
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
