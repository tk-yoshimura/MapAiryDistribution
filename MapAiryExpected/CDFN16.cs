﻿using MultiPrecision;

namespace MapAiryExpected {
    public static class CDFN16 {
        public static MultiPrecision<Pow2.N16> Value(MultiPrecision<Pow2.N16> x, bool complementary = false) {
            if (x > 6.625) {
                return CDFLimit<Pow2.N16, N24>.Value(x, complementary);
            }

            if (x >= -4 && x <= 5) {
                return CDFNearZero<Pow2.N16, N24>.Value(x, complementary);
            }

            if (x >= -5 && x <= 6.25) {
                return CDFNearZero<Pow2.N16, Pow2.N32>.Value(x, complementary);
            }

            if (x >= -6.25) {
                return CDFNearZero<Pow2.N16, N48>.Value(x, complementary);
            }

            if (x >= -7.25) {
                return CDFNearZero<Pow2.N16, Pow2.N64>.Value(x, complementary);
            }

            if (x >= -8) {
                return CDFNearZero<Pow2.N16, N80>.Value(x, complementary);
            }

            if (x >= -8.75) {
                return CDFNearZero<Pow2.N16, N96>.Value(x, complementary);
            }

            return MultiPrecision<Pow2.N16>.NaN;
        }
    }
}
