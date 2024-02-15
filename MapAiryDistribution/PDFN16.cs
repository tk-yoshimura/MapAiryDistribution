using MultiPrecision;

namespace MapAiryDistribution {
    public static class PDFN16 {
        public static MultiPrecision<Pow2.N16> Value(MultiPrecision<Pow2.N16> x) {
            if (x >= 6.5) {
                return PDFLimit<Pow2.N16, N24>.PlusValue(x);
            }

            if (x <= -6.40625) {
                return PDFLimit<Pow2.N16, N24>.MinusValue(x);
            }

            if (x >= -5.09375 && x <= 5.03125) {
                return PDFNearZero<Pow2.N16, N24>.Value(x);
            }

            if (x >= -6.40625 && x <= 6.34375) {
                return PDFNearZero<Pow2.N16, Pow2.N32>.Value(x);
            }

            if (x >= -8 && x <= 8) {
                return PDFNearZero<Pow2.N16, N48>.Value(x);
            }

            return MultiPrecision<Pow2.N16>.NaN;
        }
    }
}
