﻿using MultiPrecision;

namespace MapAiryExpected {
    public class CDFMinusLimit<N, M> where N : struct, IConstant where M : struct, IConstant {
        private static readonly List<MultiPrecision<M>> coef_table = [];

        public static MultiPrecision<N> Value(MultiPrecision<N> x, bool complementary = false, int max_terms = 2048) {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(x, 0);

            MultiPrecision<M> xe = MultiPrecision<N>.Abs(x).Convert<M>();
            MultiPrecision<M> v = 1 / xe, v3 = v * v * v, v6 = v3 * v3;

            MultiPrecision<M> s = 0, u = MultiPrecision<M>.Sqrt(v3 * MultiPrecision<M>.RcpPI) / 2;

            for (int k = 0, conv_times = 0; k <= max_terms; k += 2) {
                MultiPrecision<M> c0 = CoefTable(k), c1 = CoefTable(k + 1);

                MultiPrecision<M> ds = u * (c0 + v3 * c1);

                if (s.Exponent - ds.Exponent > MultiPrecision<N>.Bits) {
                    conv_times++;

                    if (conv_times >= 4) {
                        s *= MultiPrecision<M>.Exp(-4 * MultiPrecision<M>.Cube(xe) / 3);

                        if (complementary) {
                            s = 1 - s;
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
                MultiPrecision<M> c = MinusLimitCoef<Plus4<M>>.CDFTerm(k).Convert<M>();

                coef_table.Add(c);
            }

            return coef_table[n];
        }
    }
}
