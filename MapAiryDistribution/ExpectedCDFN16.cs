using MultiPrecision;

namespace MapAiryDistribution {
    internal class ExpectedCDFN16 {
        static void Main() {
            MultiPrecision<Pow2.N16> y = CDFLimit<Pow2.N16, Pow2.N32>.Value(8);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
