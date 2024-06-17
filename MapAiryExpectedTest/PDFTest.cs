using MapAiryExpected;
using MultiPrecision;

namespace MapAiryExpectedTest {
    [TestClass]
    public class PDFTest {
        [TestMethod]
        public void Border6p5Test() {
            Assert.IsTrue(PDFLimit<Pow2.N16, N24>.PlusValue(6.5) == PDFNearZero<Pow2.N16, N48>.Value(6.5));
            Assert.IsTrue(PDFN16.Value(6.5) == PDFLimit<Pow2.N16, N24>.PlusValue(6.5));
            Assert.IsTrue(PDFN16.Value(6.5) == PDFNearZero<Pow2.N16, N48>.Value(6.5));
        }

        [TestMethod]
        public void Border6p34375Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, Pow2.N32>.Value(6.34375) == PDFNearZero<Pow2.N16, N48>.Value(6.34375));
            Assert.IsTrue(PDFN16.Value(6.34375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(6.34375));
            Assert.IsTrue(PDFN16.Value(6.34375) == PDFNearZero<Pow2.N16, N48>.Value(6.34375));
        }

        [TestMethod]
        public void Border5p03125Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, N24>.Value(5.03125) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(5.03125));
            Assert.IsTrue(PDFN16.Value(5.03125) == PDFNearZero<Pow2.N16, N24>.Value(5.03125));
            Assert.IsTrue(PDFN16.Value(5.03125) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(5.03125));
        }

        [TestMethod]
        public void BorderM5p09375Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, N24>.Value(-5.09375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(-5.09375));
            Assert.IsTrue(PDFN16.Value(-5.09375) == PDFNearZero<Pow2.N16, N24>.Value(-5.09375));
            Assert.IsTrue(PDFN16.Value(-5.09375) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(-5.09375));
        }

        [TestMethod]
        public void BorderM6p40625Test() {
            Assert.IsTrue(PDFNearZero<Pow2.N16, Pow2.N32>.Value(-6.40625) == PDFNearZero<Pow2.N16, N48>.Value(-6.40625));
            Assert.IsTrue(PDFNearZero<Pow2.N16, Pow2.N32>.Value(-6.40625) == PDFLimit<Pow2.N16, N48>.MinusValue(-6.40625));

            Assert.IsTrue(PDFN16.Value(-6.40625) == PDFNearZero<Pow2.N16, N48>.Value(-6.40625));
            Assert.IsTrue(PDFN16.Value(-6.40625) == PDFNearZero<Pow2.N16, Pow2.N32>.Value(-6.40625));
            Assert.IsTrue(PDFN16.Value(-6.40625) == PDFLimit<Pow2.N16, N48>.MinusValue(-6.40625));

        }
    }
}