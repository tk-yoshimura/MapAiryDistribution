using MultiPrecision;

namespace MapAiryExpected {
    public class PDFLimit<N, M> where N : struct, IConstant where M : struct, IConstant {
        private static readonly List<MultiPrecision<M>> coef_table = [1];
        private static readonly List<MultiPrecision<M>> pluscoef_table = [], minuscoef_table = [];

        public static MultiPrecision<N> PlusValue(MultiPrecision<N> x, bool exp_scaled = true, int max_terms = 8192) {
            ArgumentOutOfRangeException.ThrowIfNegative(x);

            MultiPrecision<M> xe = x.Convert<M>();
            MultiPrecision<M> v = 1 / xe, v3 = v * v * v, v6 = v3 * v3;

            MultiPrecision<M> s = 0, u = 2 * MultiPrecision<M>.Sqrt(xe * MultiPrecision<M>.RcpPI);

            for (int k = 0, conv_times = 0; k <= max_terms; k += 2) {
                MultiPrecision<M> c0 = PlusCoefTable(k), c1 = PlusCoefTable(k + 1);

                MultiPrecision<M> ds = u * (c0 - v3 * c1);

                if (s.Exponent - ds.Exponent > MultiPrecision<N>.Bits) {
                    conv_times++;

                    if (conv_times >= 4) {
                        if (!exp_scaled) {
                            s *= MultiPrecision<M>.Exp(-2 * MultiPrecision<M>.Cube(xe) / 3) / 2;
                        }

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
                u *= v6;
            }

            return MultiPrecision<N>.NaN;
        }

        public static MultiPrecision<N> MinusValue(MultiPrecision<N> x, bool exp_scaled = true, int max_terms = 8192) {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(x, 0);

            MultiPrecision<M> xe = MultiPrecision<N>.Abs(x).Convert<M>();
            MultiPrecision<M> v = 1 / xe, v3 = v * v * v, v6 = v3 * v3;

            MultiPrecision<M> s = 0, u = MultiPrecision<M>.Sqrt(xe * MultiPrecision<M>.RcpPI);

            for (int k = 0, conv_times = 0; k <= max_terms; k += 2) {
                MultiPrecision<M> c0 = MinusCoefTable(k), c1 = MinusCoefTable(k + 1);

                MultiPrecision<M> ds = u * (c0 - v3 * c1);

                if (s.Exponent - ds.Exponent > MultiPrecision<N>.Bits) {
                    conv_times++;

                    if (conv_times >= 4) {
                        if (exp_scaled) {
                            s *= 2 * MultiPrecision<M>.Exp(-4 * MultiPrecision<M>.Cube(xe) / 3);
                        }
                        else {
                            s *= MultiPrecision<M>.Exp(-2 * MultiPrecision<M>.Cube(xe) / 3);
                        }

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
                u *= v6;
            }

            return MultiPrecision<N>.NaN;
        }

        public static MultiPrecision<M> CoefTable(int n) {
            for (int k = coef_table.Count; k <= n; k++) {
                MultiPrecision<M> c = coef_table[^1] * (6 * k - 1) * (6 * k - 5) / (48 * k);

                coef_table.Add(c);
            }

            return coef_table[n];
        }

        public static MultiPrecision<M> PlusCoefTable(int n) {
            for (int k = pluscoef_table.Count; k <= n; k++) {
                MultiPrecision<M> c = CoefTable(k) * (6 * k) / (1 - 6 * k);

                pluscoef_table.Add(c);
            }

            return pluscoef_table[n];
        }

        public static MultiPrecision<M> MinusCoefTable(int n) {
            for (int k = minuscoef_table.Count; k <= n; k++) {
                MultiPrecision<M> c = CoefTable(k) / (1 - 6 * k);

                minuscoef_table.Add(c);
            }

            return minuscoef_table[n];
        }
    }
}
