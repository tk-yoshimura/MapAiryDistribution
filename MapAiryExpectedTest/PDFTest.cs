using MapAiryExpected;
using MultiPrecision;

namespace MapAiryExpectedTest {
    [TestClass]
    public class PDFTest {
        [TestMethod]
        public void PlusLimitTest() {
            for (double x = 32; x >= 4; x -= 0.125) {
                Console.WriteLine($"{x}\t {PDFPlusLimit<Pow2.N16, N24>.Value(x)}");
            }
        }

        [TestMethod]
        public void MinusLimitTest() {
            for (double x = -32; x <= -4; x += 0.125) {
                Console.WriteLine($"{x}\t {PDFMinusLimit<Pow2.N16, N24>.Value(x)}");
            }
        }

        [TestMethod]
        public void NearZeroTest() {
            for (double x = -7; x <= 7; x += 0.125) {
                Console.WriteLine($"N24\t{x}\t {PDFNearZero<Pow2.N16, N24>.Value(x)}");
                Console.WriteLine($"N32\t{x}\t {PDFNearZero<Pow2.N16, Pow2.N32>.Value(x)}");
                Console.WriteLine($"N48\t{x}\t {PDFNearZero<Pow2.N16, N48>.Value(x)}");
            }
        }

        [TestMethod]
        public void BorderM6p5Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, N48>.Value(-6.5) == PDFMinusLimit<Pow2.N16, N24>.Value(-6.5));

            Assert.IsTrue(PDFN16.Value(-6.5) == PDFNearZero<Pow2.N16, N48>.Value(-6.5));
            Assert.IsTrue(PDFN16.Value(-6.5) == PDFMinusLimit<Pow2.N16, N24>.Value(-6.5));
        }

        [TestMethod]
        public void BorderM6p375Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, N48>.Value(-6.375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(-6.375));

            Assert.IsTrue(PDFN16.Value(-6.375) == PDFNearZero<Pow2.N16, N48>.Value(-6.375));
            Assert.IsTrue(PDFN16.Value(-6.375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(-6.375));
        }

        [TestMethod]
        public void BorderM5Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, Pow2.N32>.Value(-5) == PDFNearZero<Pow2.N16, N24>.Value(-5));

            Assert.IsTrue(PDFN16.Value(-5) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(-5));
            Assert.IsTrue(PDFN16.Value(-5) == PDFNearZero<Pow2.N16, N24>.Value(-5));
        }

        [TestMethod]
        public void BorderP5Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, Pow2.N32>.Value(5) == PDFNearZero<Pow2.N16, N24>.Value(5));

            Assert.IsTrue(PDFN16.Value(5) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(5));
            Assert.IsTrue(PDFN16.Value(5) == PDFNearZero<Pow2.N16, N24>.Value(5));
        }

        public void BorderP6p375Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, N48>.Value(6.375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(6.375));

            Assert.IsTrue(PDFN16.Value(6.375) == PDFNearZero<Pow2.N16, N48>.Value(6.375));
            Assert.IsTrue(PDFN16.Value(6.375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(6.375));
        }

        [TestMethod]
        public void BorderP6p625Test() {
            Assert.IsTrue(PDFPlusLimit<Pow2.N16, N24>.Value(6.625) == PDFNearZero<Pow2.N16, N48>.Value(6.625));
            Assert.IsTrue(PDFN16.Value(6.625) == PDFPlusLimit<Pow2.N16, N24>.Value(6.625));
            Assert.IsTrue(PDFN16.Value(6.625) == PDFNearZero<Pow2.N16, N48>.Value(6.625));
        }
    }
}