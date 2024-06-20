using MultiPrecision;

namespace MapAiryExpected {
    public static class PDFN16 {
        public static MultiPrecision<Pow2.N16> Value(MultiPrecision<Pow2.N16> x) {
            if (x <= -6.5) {
                return PDFMinusLimit<Pow2.N16, N24>.Value(x);
            }

            if (x >= 6.625) {
                return PDFPlusLimit<Pow2.N16, N24>.Value(x);
            }

            if (x <= -6.375 || x >= 6.375) {
                return PDFNearZero<Pow2.N16, N48>.Value(x);
            }

            if (x <= -5 || x >= 5) {
                return PDFNearZero<Pow2.N16, Pow2.N32>.Value(x);
            }

            return PDFNearZero<Pow2.N16, N24>.Value(x);
        }
    }
}
