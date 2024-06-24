using MapAiryExpected;
using MultiPrecision;
using MultiPrecisionRootFinding;

namespace MapAiryEvalExpected {
    internal class ExpectedQuantileLowerN16 {
        static void Main_() {
            using (BinaryWriter sw = new(File.Open("../../../../results_disused/quantile_lower_precision150.bin", FileMode.Create))) {
                MultiPrecision<Pow2.N16> x = "-0.2734763098101749523722883574736459560155353374157768601569825776256";

                for (MultiPrecision<Pow2.N16> u0 = 1; u0 <= 1048576; u0 *= 2) {
                    for (MultiPrecision<Pow2.N16> u = u0; u < u0 * 2 && u <= 1048576; u += u0 / (u0 < 4 ? 65536 : 32768)) {
                        MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Pow2(-u);

                        x = NewtonRaphsonFinder<Pow2.N16>.RootFind(
                            x => (CDFN16.Value(x) - p, PDFN16.Value(x)),
                            x0: x, overshoot_decay: true, iters: 256
                        );

                        Console.WriteLine($"{u}\n{x}\n");

                        sw.Write(u);
                        sw.Write(x);
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
