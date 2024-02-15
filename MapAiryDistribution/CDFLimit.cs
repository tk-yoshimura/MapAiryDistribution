using MultiPrecision;

namespace MapAiryDistribution {
    public class CDFLimit<N, M> where N : struct, IConstant where M : struct, IConstant {
        private static readonly List<MultiPrecision<M>> coef_table = [];

        public static MultiPrecision<N> Value(MultiPrecision<N> x, bool complementary = false, int max_terms = 8192) {
            ArgumentOutOfRangeException.ThrowIfNegative(x);

            MultiPrecision<M> xe = x.Convert<M>();
            MultiPrecision<M> v = 1 / xe, v3 = v * v * v, v6 = v3 * v3;

            MultiPrecision<M> s = 0, u = 2 * MultiPrecision<M>.Sqrt(v3 * MultiPrecision<M>.RcpPI);

            for (int k = 1, conv_times = 0; k <= max_terms; k += 2) {
                MultiPrecision<M> c0 = CoefTable(k), c1 = CoefTable(k + 1);

                MultiPrecision<M> ds = u * (c0 - v3 * c1);

                if (s.Exponent - ds.Exponent > MultiPrecision<N>.Bits) {
                    conv_times++;

                    if (conv_times >= 4) {
                        return complementary ? s.Convert<N>() : (1 - s).Convert<N>();
                    }
                }
                else {
                    conv_times = 0;
                }

                if (s.Exponent > MultiPrecision<M>.Bits - MultiPrecision<N>.Bits) {
                    break;
                }

                s += ds;
                u *= v6;
            }

            return MultiPrecision<N>.NaN;
        }

        public static MultiPrecision<M> CoefTable(int n) {
            for (int k = coef_table.Count; k <= n; k++) {
                MultiPrecision<M> c = PDFLimit<N, M>.PlusCoefTable(k) * 2 / (3 - 6 * k);

                coef_table.Add(c);
            }

            return coef_table[n];
        }
    }
}
