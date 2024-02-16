using MultiPrecision;

namespace MapAiryDistribution {
    public class CDFNearZero<N, M> where N : struct, IConstant where M : struct, IConstant {
        private static readonly List<(MultiPrecision<M> c0, MultiPrecision<M> c1)> airycoef_table = [], pdfcoef_table = [], cdfcoef_table = [];

        public static MultiPrecision<N> Value(MultiPrecision<N> x, bool complementary = false, int max_terms = 8192) {
            if (x == 0) {
                return complementary ? MultiPrecision<N>.Div(1, 3) : MultiPrecision<N>.Div(2, 3);
            }

            MultiPrecision<M> xe = x.Convert<M>();
            MultiPrecision<M> x3 = MultiPrecision<M>.Cube(xe);

            MultiPrecision<M> s = complementary ? MultiPrecision<M>.Div(1, 3) : MultiPrecision<M>.Div(2, 3);
            MultiPrecision<M> u = complementary ? -xe : xe;

            for (int k = 0, conv_times = 0; k <= max_terms; k++) {
                (MultiPrecision<M> c0, MultiPrecision<M> c1) = CDFCoefTable(k);

                MultiPrecision<M> ds = u * (c0 + xe * c1);

                if (s.Exponent - ds.Exponent > MultiPrecision<N>.Bits) {
                    conv_times++;

                    if (conv_times >= 4) {
                        return s.Convert<N>();
                    }
                }
                else {
                    conv_times = 0;
                }

                if (s.Exponent > MultiPrecision<M>.Bits - MultiPrecision<N>.Bits) {
                    break;
                }

                s += ds;
                u *= x3;
            }

            return MultiPrecision<N>.NaN;
        }

        public static (MultiPrecision<M> c0, MultiPrecision<M> c1) AiryCoefTable(int n) {
            for (int k = airycoef_table.Count; k <= n; k += 2) {
                (MultiPrecision<M> c0, MultiPrecision<M> c1, MultiPrecision<M> c3, MultiPrecision<M> c4) = PDFNearZero<N, M>.CoefTable(k / 2);

                airycoef_table.Add((c0, c1));
                airycoef_table.Add((c3, c4));
            }

            return airycoef_table[n];
        }

        public static (MultiPrecision<M> c0, MultiPrecision<M> c1) PDFCoefTable(int n) {
            for (int k = pdfcoef_table.Count; k <= n; k++) {
                MultiPrecision<M> s0 = 0, s1 = 0, r = 1;

                for (int i = k, j = 1; i >= 0; i--, j++) {
                    (MultiPrecision<M> c0, MultiPrecision<M> c1) = AiryCoefTable(i);
                    s0 += c0 * r;
                    s1 += c1 * r;
                    r = r * 2 / (3 * j);
                }

                pdfcoef_table.Add((2 * s0, 2 * s1));
            }

            return pdfcoef_table[n];
        }

        public static (MultiPrecision<M> c0, MultiPrecision<M> c1) CDFCoefTable(int n) {
            for (int k = cdfcoef_table.Count; k <= n; k++) {
                (MultiPrecision<M> c0, MultiPrecision<M> c1) = PDFCoefTable(k);

                c0 /= checked(3 * k + 1);
                c1 /= checked(3 * k + 2);

                cdfcoef_table.Add((c0, c1));
            }

            return cdfcoef_table[n];
        }
    }
}
