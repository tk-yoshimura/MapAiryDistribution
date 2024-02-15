using MultiPrecision;

namespace MapAiryDistribution {
    internal class Program {
        static void Main() {
            for (double x = -12; x <= 12; x += 1 / 32d) {
                MultiPrecision<Pow2.N16> y = PDFN16.Value(x);
                MultiPrecision<Pow2.N16> y_expected = PDFNearZero<Pow2.N16, Pow2.N128>.Value(x, exp_scaled: true);

                Console.WriteLine($"{x}\n{y}\n{y_expected},{y_expected == y}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
