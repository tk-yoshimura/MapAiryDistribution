using MapAiryPadeApprox;
using MultiPrecision;

namespace MapAiryEvalExpected {
    internal class ExpectedQuantileN16 {
        static void Main() {
            using (StreamWriter sw = new("../../../../results/quantile_precision142.csv")) {
                sw.WriteLine("quantile,x");

                for (int k = 5000; k > 1; k--) {
                    MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Div(k, 10000);
                    MultiPrecision<Pow2.N16> x = QuantilePadeN16.Value(p, complementary: false);

                    Console.WriteLine($"{p},{x:e142}");
                    sw.WriteLine($"{p},{x:e142}");
                }

                for (long k = 10000; k < 1000000000000000000L; k *= 10) {
                    MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Div(1, k);
                    MultiPrecision<Pow2.N16> x = QuantilePadeN16.Value(p, complementary: false);

                    Console.WriteLine($"{p},{x:e142}");
                    sw.WriteLine($"{p},{x:e142}");
                }

                for (int k = 5000; k > 1; k--) {
                    MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Div(k, 10000);
                    MultiPrecision<Pow2.N16> x = QuantilePadeN16.Value(p, complementary: true);

                    Console.WriteLine($"{1 - p},{x:e142}");
                    sw.WriteLine($"{1 - p},{x:e142}");
                }

                for (long k = 10000; k < 1000000000000000000L; k *= 10) {
                    MultiPrecision<Pow2.N16> p = MultiPrecision<Pow2.N16>.Div(1, k);
                    MultiPrecision<Pow2.N16> x = QuantilePadeN16.Value(p, complementary: true);

                    Console.WriteLine($"{1 - p},{x:e142}");
                    sw.WriteLine($"{1 - p},{x:e142}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
