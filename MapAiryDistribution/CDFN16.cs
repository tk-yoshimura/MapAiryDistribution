using MultiPrecision;

namespace MapAiryDistribution {
    public static class CDFN16 {
        public static MultiPrecision<Pow2.N16> Value(MultiPrecision<Pow2.N16> x, bool complementary = false) {
            if (x > 6.5) {
                return CDFLimit<Pow2.N16, N24>.Value(x, complementary);
            }

            if (x >= -4.25 && x <= 5) { 
                return CDFNearZero<Pow2.N16, N24>.Value(x, complementary);
            }

            if (x >= -5 && x <= 6.25) { 
                return CDFNearZero<Pow2.N16, Pow2.N32>.Value(x, complementary);
            }

            if (x >= -6.25) {
                return CDFNearZero<Pow2.N16, N48>.Value(x, complementary);
            }

            return MultiPrecision<Pow2.N16>.NaN;
        }
    }
}
