using MultiPrecision;

namespace MapAiryExpected {
    public class PDFNearZero<N, M> where N : struct, IConstant where M : struct, IConstant {
        private static readonly MultiPrecision<M> g1 = 1 / (MultiPrecision<M>.Gamma(MultiPrecision<M>.Div(1, 3)) * MultiPrecision<M>.Cbrt(3));
        private static readonly MultiPrecision<M> g2 = 1 / (MultiPrecision<M>.Gamma(MultiPrecision<M>.Div(2, 3)) * MultiPrecision<M>.Cbrt(9));

        private static readonly List<(MultiPrecision<M> c0, MultiPrecision<M> c1, MultiPrecision<M> c3, MultiPrecision<M> c4)> coef_table = [];

        public static MultiPrecision<N> Value(MultiPrecision<N> x, int max_terms = 2048) {
            MultiPrecision<M> xe = x.Convert<M>();
            MultiPrecision<M> x2 = MultiPrecision<M>.Square(xe), x6 = x2 * x2 * x2;

            MultiPrecision<M> s = 0, u = 1;

            for (int k = 0, conv_times = 0; k <= max_terms; k++) {
                (MultiPrecision<M> c0, MultiPrecision<M> c1, MultiPrecision<M> c3, MultiPrecision<M> c4) = CoefTable(k);

                MultiPrecision<M> ds = u * (c0 + xe * (c1 + x2 * (c3 + xe * c4)));

                if (s.Exponent - ds.Exponent > MultiPrecision<N>.Bits) {
                    conv_times++;

                    if (conv_times >= 4) {
                        s *= 2 * MultiPrecision<M>.Exp(2 * MultiPrecision<M>.Cube(xe) / 3);

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
                u *= x6;
            }

            return MultiPrecision<N>.NaN;
        }

        public static (MultiPrecision<M> c0, MultiPrecision<M> c1, MultiPrecision<M> c3, MultiPrecision<M> c4) CoefTable(int n) {
            for (int k = coef_table.Count; k <= n; k++) {
                MultiPrecision<M> c0 = g1 * NearZeroCoef<Plus4<M>>.PDFExpTerm(6 * k).Convert<M>();
                MultiPrecision<M> c1 = g2 * NearZeroCoef<Plus4<M>>.PDFExpTerm(6 * k + 1).Convert<M>();
                MultiPrecision<M> c3 = g1 * NearZeroCoef<Plus4<M>>.PDFExpTerm(6 * k + 3).Convert<M>();
                MultiPrecision<M> c4 = g2 * NearZeroCoef<Plus4<M>>.PDFExpTerm(6 * k + 4).Convert<M>();

                coef_table.Add((c0, c1, c3, c4));
            }

            return coef_table[n];
        }
    }
}
