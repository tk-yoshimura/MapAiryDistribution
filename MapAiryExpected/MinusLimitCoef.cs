namespace MapAiryExpected {
    public static class MinusLimitCoef {
        private static readonly Dictionary<long, Fraction> pdf_terms = [], pdf_prod_terms = new() { { 0, 1 } };
        private static readonly Dictionary<long, Fraction> cdf_terms = [];

        private static Fraction PDFProd(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (pdf_prod_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            Fraction f = -PDFProd(i - 1) * new Fraction(checked((6 * i - 1) * (6 * i - 5)), checked(48 * i));

            pdf_prod_terms[i] = f;

            return pdf_prod_terms[i];
        }

        public static Fraction PDFTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (pdf_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            Fraction f = PDFProd(i) / checked(1 - 6 * i);

            pdf_terms[i] = f;

            return pdf_terms[i];
        }

        public static Fraction CDFTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (cdf_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            Fraction f = 0;
            for (long j = 0; j <= i; j++) {
                Fraction g = PDFTerm(j);

                for (long k = 1; k <= i - j; k++) {
                    g *= new Fraction(6 * j + 6 * k - 3, 8);
                }

                if ((i - j) % 2 == 0) {
                    f += g;
                }
                else {
                    f -= g;
                }
            }

            cdf_terms[i] = f;

            return cdf_terms[i];
        }
    }
}
