using MapAiryExpected;
using MultiPrecision;
using MultiPrecisionRootFinding;

namespace MapAiryEvalExpected {
    internal class ExpectedQuantileUpperScaledN16 {
        static void Main_() {
            using (BinaryWriter sw = new(File.Open("../../../../results_disused/quantile_upper_precision150_scaled.bin", FileMode.Create))) {
                MultiPrecision<Pow2.N16> x = "-0.2734763098101749523722883574736459560155353374157768601569825776256";

                for (MultiPrecision<Pow2.N16> u0 = 1; u0 <= 1024; u0 *= 2) {
                    for (MultiPrecision<Pow2.N16> u = u0; u < u0 * 2 && u <= 1024; u += u0 / (u0 < 4 ? 65536 : 32768)) {
                        MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Pow2(-u);

                        x = NewtonRaphsonFinder<Pow2.N16>.RootFind(
                            x => (CDFN16.Value(x, complementary: true) - p, -PDFN16.Value(x)),
                            x0: x, overshoot_decay: true, iters: 256
                        );

                        MultiPrecision<Pow2.N16> v = x * MultiPrecision<Pow2.N16>.Square(MultiPrecision<Pow2.N16>.Cbrt(p));

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
