using MapAiryExpected;
using MultiPrecision;

namespace MapAiryExpectedTest {
    [TestClass]
    public class CDFTest {
        [TestMethod]
        public void PlusLimitTest() {
            for (double x = 32; x >= 4; x -= 0.125) {
                Console.WriteLine($"{x}\t {CDFPlusLimit<Pow2.N16, N24>.Value(x, complementary: true)}");
            }
        }

        [TestMethod]
        public void MinusLimitTest() {
            for (double x = -32; x <= -4; x += 0.125) {
                Console.WriteLine($"{x}\t {CDFMinusLimit<Pow2.N16, N24>.Value(x)}");
            }
        }

        [TestMethod]
        public void NearZeroTest() {
            for (double x = -7; x < 0; x += 0.125) {
                Console.WriteLine($"N24\t{x}\t {CDFNearZero<Pow2.N16, N24>.Value(x)}");
                Console.WriteLine($"N32\t{x}\t {CDFNearZero<Pow2.N16, Pow2.N32>.Value(x)}");
                Console.WriteLine($"N48\t{x}\t {CDFNearZero<Pow2.N16, N48>.Value(x)}");
            }

            for (double x = 0; x <= 7; x += 0.125) {
                Console.WriteLine($"N24\t{x}\t {CDFNearZero<Pow2.N16, N24>.Value(x, complementary: true)}");
                Console.WriteLine($"N32\t{x}\t {CDFNearZero<Pow2.N16, Pow2.N32>.Value(x, complementary: true)}");
                Console.WriteLine($"N48\t{x}\t {CDFNearZero<Pow2.N16, N48>.Value(x, complementary: true)}");
            }
        }

        [TestMethod]
        public void Border6p625Test() {
            Assert.IsTrue(
                CDFPlusLimit<Pow2.N16, N24>.Value(6.5, complementary: true) ==
                CDFNearZero<Pow2.N16, N48>.Value(6.5, complementary: true)
            );

            Assert.IsTrue(
                CDFN16.Value(6.5, complementary: true) ==
                CDFPlusLimit<Pow2.N16, N24>.Value(6.5, complementary: true)
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