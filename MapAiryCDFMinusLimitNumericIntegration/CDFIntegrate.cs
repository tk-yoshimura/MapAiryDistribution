using MapAiryExpected;
using MultiPrecision;
using MultiPrecisionIntegrate;

namespace MapAiryCDFMinusLimitNumericIntegration {
    internal class CDFIntegrate {
        static void Main_() {
            List<MultiPrecision<N20>> xs = [];

            for (MultiPrecision<N20> h = 1d / 1024, x0 = 8, x1 = 16; x1 <= 256; h *= 2, x0 *= 2, x1 *= 2) {
                for (MultiPrecision<N20> x = x0; x < x1; x += h) {
                    xs.Add(-x);
                }
            }
            xs.Add(-256);

            using (StreamWriter sw = new("../../../../results_disused/cdfintegrate_precision160.csv")) {
                sw.WriteLine("x0,x1,integrate,error/eps,error,relative_error");

                MultiPrecision<N20> x1 = xs[0];

                foreach (MultiPrecision<N20> x0 in xs.Skip(1)) {
                    Console.WriteLine($"{x0},{x1}");

                    MultiPrecision<N20> eps = (PDFLimit<N20, N24>.MinusValue(x0) + PDFLimit<N20, N24>.MinusValue(x1)) * (x1 - x0);
                    
                    (MultiPrecision<N20> y, MultiPrecision<N20> error, long eval_points) = GaussKronrodIntegral<N20>.AdaptiveIntegrate(
                        x => PDFLimit<N20, N24>.MinusValue(x.Convert<N20>()),
                        x0.Convert<N20>(), x1.Convert<N20>(), eps.Convert<N20>() * 1e-160, GaussKronrodOrder.G64K129, discontinue_eval_points: 65536
                    );

                    MultiPrecision<N20> relative_eps = error / eps;
                    MultiPrecision<N20> relative_error = error / y;

                    Console.WriteLine($"{y:e16},{eps:e8},{error:e8},{relative_eps:e8}");
                    sw.WriteLine($"{x0},{x1},{y},{relative_eps:e8},{error:e8},{relative_error:e8}");

                    sw.Flush();

                    x1 = x0;
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
