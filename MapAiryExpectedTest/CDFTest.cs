using MapAiryExpected;
using MultiPrecision;

namespace MapAiryExpectedTest {
    [TestClass]
    public class CDFTest {
        [TestMethod]
        public void Border6p625Test() {
            Assert.IsTrue(
                CDFLimit<Pow2.N16, N24>.Value(6.5, complementary: true) ==
                CDFNearZero<Pow2.N16, N48>.Value(6.5, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(6.5, complementary: true) ==
                CDFLimit<Pow2.N16, N24>.Value(6.5, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(6.5, complementary: true) ==
                CDFNearZero<Pow2.N16, N48>.Value(6.5, complementary: true)
            );
        }

        [TestMethod]
        public void Border6p25Test() {
            Assert.IsTrue(
                CDFNearZero<Pow2.N16, Pow2.N32>.Value(6.25, complementary: true) ==
                CDFNearZero<Pow2.N16, N48>.Value(6.25, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(6.25, complementary: true) ==
                CDFNearZero<Pow2.N16, Pow2.N32>.Value(6.25, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(6.25, complementary: true) ==
                CDFNearZero<Pow2.N16, N48>.Value(6.25, complementary: true)
            );
        }

        [TestMethod]
        public void Border5Test() {
            Assert.IsTrue(
                CDFNearZero<Pow2.N16, N24>.Value(5, complementary: true) ==
                CDFNearZero<Pow2.N16, Pow2.N32>.Value(5, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(5, complementary: true) ==
                CDFNearZero<Pow2.N16, N24>.Value(5, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(5, complementary: true) ==
                CDFNearZero<Pow2.N16, Pow2.N32>.Value(5, complementary: true)
            );
        }

        [TestMethod]
        public void BorderM4Test() {
            Assert.IsTrue(CDFNearZero<Pow2.N16, N24>.Value(-4) == CDFNearZero<Pow2.N16, Pow2.N32>.Value(-4));

            Assert.IsTrue(CDFN16.Value(-4) == CDFNearZero<Pow2.N16, N24>.Value(-4));
            Assert.IsTrue(CDFN16.Value(-4) == CDFNearZero<Pow2.N16, Pow2.N32>.Value(-4));
        }

        [TestMethod]
        public void BorderM5Test() {
            Assert.IsTrue(CDFNearZero<Pow2.N16, Pow2.N32>.Value(-5) == CDFNearZero<Pow2.N16, N48>.Value(-5));

            Assert.IsTrue(CDFN16.Value(-5) == CDFNearZero<Pow2.N16, Pow2.N32>.Value(-5));
            Assert.IsTrue(CDFN16.Value(-5) == CDFNearZero<Pow2.N16, N48>.Value(-5));
        }

        [TestMethod]
        public void BorderM6p25Test() {
            Assert.IsTrue(CDFNearZero<Pow2.N16, N48>.Value(-6.25) == CDFNearZero<Pow2.N16, Pow2.N64>.Value(-6.25));

            Assert.IsTrue(CDFN16.Value(-6.25) == CDFNearZero<Pow2.N16, N48>.Value(-6.25));
            Assert.IsTrue(CDFN16.Value(-6.25) == CDFNearZero<Pow2.N16, Pow2.N64>.Value(-6.25));
        }

        [TestMethod]
        public void BorderM7p25Test() {
            Assert.IsTrue(CDFNearZero<Pow2.N16, Pow2.N64>.Value(-7.25) == CDFNearZero<Pow2.N16, N80>.Value(-7.25));

            Assert.IsTrue(CDFN16.Value(-7.25) == CDFNearZero<Pow2.N16, Pow2.N64>.Value(-7.25));
            Assert.IsTrue(CDFN16.Value(-7.25) == CDFNearZero<Pow2.N16, N80>.Value(-7.25));
        }

        [TestMethod]
        public void BorderM8Test() {
            Assert.IsTrue(CDFNearZero<Pow2.N16, N80>.Value(-8) == CDFNearZero<Pow2.N16, N96>.Value(-8));

            Assert.IsTrue(CDFN16.Value(-8) == CDFNearZero<Pow2.N16, N80>.Value(-8));
            Assert.IsTrue(CDFN16.Value(-8) == CDFNearZero<Pow2.N16, N96>.Value(-8));
        }
    }
}