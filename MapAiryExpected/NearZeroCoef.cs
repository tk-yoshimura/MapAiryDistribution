using MultiPrecision;

namespace MapAiryExpected {
    public static class NearZeroCoef {
        private static readonly Dictionary<long, Fraction> pdf_terms = [];
        private static readonly Dictionary<long, Fraction> cdf_terms = [];

        private static readonly Dictionary<long, Fraction> pdf_exp_terms = new(){
            { 0, 1 }, { 1, -1 }, { 2, 0 }, { 3, 1 }, { 4, -new Fraction(1, 2) }, { 5, 0 }
        };
        private static readonly Dictionary<long, Fraction> cdf_exp_terms = [];

        public static Fraction PDFExpTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (pdf_exp_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            if ((i % 3) == 2) {
                pdf_exp_terms.Add(i, 0);
            }
            else {
                long n = i / 6, k = i % 6;

                Fraction f = PDFExpTerm(i - 6);
                if (k == 0) {
                    f /= checked((3 * n - 2) * (3 * n));
                }
                else if (k == 1) {
                    f /= checked((3 * n - 1) * (3 * n));
                }
                else if (k == 3) {
                    f /= checked((3 * n) * (3 * n + 1));
                }
                else {
                    f /= checked((3 * n) * (3 * n + 2));
                }

                pdf_exp_terms.Add(i, f);
            }

            return pdf_exp_terms[i];
        }

        public static Fraction PDFTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (pdf_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            Fraction f = 0;
            for (long j = i % 3; j <= i; j += 3) {
                Fraction g = PDFExpTerm(j);

                for (long k = 1; k <= (i - j) / 3; k++) {
                    g *= new Fraction(2, 3) / k;
                }

                f += g;
            }


            pdf_terms.Add(i, f);

            return pdf_terms[i];
        }

        public static Fraction CDFTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (cdf_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            Fraction f = PDFTerm(i) / (i + 1);

            cdf_terms.Add(i, f);

            return cdf_terms[i];
        }

        public static Fraction CDFExpTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (cdf_exp_terms.TryGetValue(i, out Fraction? value)) {
                return value;
            }

            Fraction f = 0;
            for (long j = i % 3; j <= i; j += 3) {
                Fraction g = CDFTerm(j);

                for (long k = 1; k <= (i - j) / 3; k++) {
                    g *= new Fraction(-2, 3) / k;
                }

                f += g;
            }


            cdf_exp_terms.Add(i, f);

            return cdf_exp_terms[i];
        }
    }

    public static class NearZeroCoef<N> where N: struct, IConstant {
        private static readonly Dictionary<long, MultiPrecision<N>> pdf_terms = [];
        private static readonly Dictionary<long, MultiPrecision<N>> cdf_terms = [];

        private static readonly Dictionary<long, MultiPrecision<N>> pdf_exp_terms = new(){
            { 0, 1 }, { 1, -1 }, { 2, 0 }, { 3, 1 }, { 4, -MultiPrecision<N>.Point5 }, { 5, 0 }
        };
        private static readonly Dictionary<long, MultiPrecision<N>> cdf_exp_terms = [];

        private static readonly MultiPrecision<N> c2d3 = MultiPrecision<N>.Div(2, 3);

        public static MultiPrecision<N> PDFExpTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (pdf_exp_terms.TryGetValue(i, out MultiPrecision<N>? value)) {
                return value;
            }

            if ((i % 3) == 2) {
                pdf_exp_terms.Add(i, 0);
            }
            else {
                long n = i / 6, k = i % 6;

                MultiPrecision<N> f = PDFExpTerm(i - 6);
                if (k == 0) {
                    f /= checked((3 * n - 2) * (3 * n));
                }
                else if (k == 1) {
                    f /= checked((3 * n - 1) * (3 * n));
                }
                else if (k == 3) {
                    f /= checked((3 * n) * (3 * n + 1));
                }
                else {
                    f /= checked((3 * n) * (3 * n + 2));
                }

                pdf_exp_terms.Add(i, f);
            }

            return pdf_exp_terms[i];
        }

        public static MultiPrecision<N> PDFTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (pdf_terms.TryGetValue(i, out MultiPrecision<N>? value)) {
                return value;
            }

            MultiPrecision<N> f = 0;
            for (long j = i % 3; j <= i; j += 3) {
                MultiPrecision<N> g = PDFExpTerm(j);

                for (long k = 1; k <= (i - j) / 3; k++) {
                    g *= c2d3 / k;
                }

                f += g;
            }


            pdf_terms.Add(i, f);

            return pdf_terms[i];
        }

        public static MultiPrecision<N> CDFTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (cdf_terms.TryGetValue(i, out MultiPrecision<N>? value)) {
                return value;
            }

            MultiPrecision<N> f = PDFTerm(i) / (i + 1);

            cdf_terms.Add(i, f);

            return cdf_terms[i];
        }

        public static MultiPrecision<N> CDFExpTerm(long i) {
            ArgumentOutOfRangeException.ThrowIfNegative(i);

            if (cdf_exp_terms.TryGetValue(i, out MultiPrecision<N>? value)) {
                return value;
            }

            MultiPrecision<N> f = 0;
            for (long j = i % 3; j <= i; j += 3) {
                MultiPrecision<N> g = CDFTerm(j);

                for (long k = 1; k <= (i - j) / 3; k++) {
                    g *= -c2d3 / k;
                }

                f += g;
            }


            cdf_exp_terms.Add(i, f);

            return cdf_exp_terms[i];
        }
    }
}
