using MultiPrecision;

namespace MapAiryExpected {
    public class CDFNearZero<N, M> where N : struct, IConstant where M : struct, IConstant {
        private static readonly MultiPrecision<M> g1 = 1 / (MultiPrecision<M>.Gamma(MultiPrecision<M>.Div(1, 3)) * MultiPrecision<M>.Cbrt(3));
        private static readonly MultiPrecision<M> g2 = 1 / (MultiPrecision<M>.Gamma(MultiPrecision<M>.Div(2, 3)) * MultiPrecision<M>.Cbrt(9));

        private static readonly List<(MultiPrecision<M> c0, MultiPrecision<M> c1)> cdfcoef_table = [];

        public static MultiPrecision<N> Value(MultiPrecision<N> x, bool complementary = false, int max_terms = 2048) {
            if (x == 0) {
                return complementary ? MultiPrecision<N>.Div(1, 3) : MultiPrecision<N>.Div(2, 3);
            }

            MultiPrecision<M> xe = x.Convert<M>();
            MultiPrecision<M> x3 = MultiPrecision<M>.Cube(xe);

            MultiPrecision<M> s = complementary ? MultiPrecision<M>.Div(1, 3) : MultiPrecision<M>.Div(2, 3);
            MultiPrecision<M> u = 2 * (complementary ? -xe : xe);

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

        public static (MultiPrecision<M> c0, MultiPrecision<M> c1) CDFCoefTable(int n) {
            for (int k = cdfcoef_table.Count; k <= n; k++) {
                MultiPrecision<M> c0 = g1 * NearZeroCoef<Plus4<M>>.CDFTerm(3 * k).Convert<M>();
                MultiPrecision<M> c1 = g2 * NearZeroCoef<Plus4<M>>.CDFTerm(3 * k + 1).Convert<M>();

                cdfcoef_table.Add((c0, c1));
            }

            return cdfcoef_table[n];
        }
    }
}
