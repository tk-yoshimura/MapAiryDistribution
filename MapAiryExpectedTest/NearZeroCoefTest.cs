using MapAiryExpected;
using MultiPrecision;
using System.Numerics;

namespace MapAiryExpectedTest {
    [TestClass]
    public class NearZeroCoefTest {
        [TestMethod]
        public void PDFExpCoefTest() {
            Assert.AreEqual(new(1, 268334780313600), NearZeroCoef.PDFExpTerm(45));
            Assert.AreEqual(new(-1, 1061932177152000), NearZeroCoef.PDFExpTerm(46));
            Assert.AreEqual(0, NearZeroCoef.PDFExpTerm(47));
            Assert.AreEqual(new(1, 6440034727526400), NearZeroCoef.PDFExpTerm(48));
            Assert.AreEqual(new(-1, 25486372251648000), NearZeroCoef.PDFExpTerm(49));
            Assert.AreEqual(0, NearZeroCoef.PDFExpTerm(50));

            for (int i = 0; i < 60; i++) {
                Fraction f = NearZeroCoef.PDFExpTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            for (int i = 0; i < 60; i++) {
                MultiPrecision<Pow2.N16> f = NearZeroCoef.PDFExpTerm(i).ToMultiPrecision<Pow2.N16>();
                MultiPrecision<Pow2.N16> g = NearZeroCoef<N24>.PDFExpTerm(i).Convert<Pow2.N16>();

                MultiPrecision<Pow2.N16>.NearlyEqualBits(f, g, ignore_bits: 1);
                Console.WriteLine($"{i}\t{g}");
            }
        }

        [TestMethod]
        public void PDFCoefTest() {
            Assert.AreEqual(new(77081860213, BigInteger.Parse("713020519923199488000")), NearZeroCoef.PDFTerm(45));
            Assert.AreEqual(new(-23569729, 677496156251904000), NearZeroCoef.PDFTerm(46));
            Assert.AreEqual(0, NearZeroCoef.PDFTerm(47));
            Assert.AreEqual(new(1640039579, BigInteger.Parse("180131499770071449600")), NearZeroCoef.PDFTerm(48));
            Assert.AreEqual(new(-2286263713, BigInteger.Parse("796735479752239104000")), NearZeroCoef.PDFTerm(49));
            Assert.AreEqual(0, NearZeroCoef.PDFExpTerm(50));

            for (int i = 0; i < 60; i++) {
                Fraction f = NearZeroCoef.PDFTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            for (int i = 0; i < 60; i++) {
                MultiPrecision<Pow2.N16> f = NearZeroCoef.PDFTerm(i).ToMultiPrecision<Pow2.N16>();
                MultiPrecision<Pow2.N16> g = NearZeroCoef<N24>.PDFTerm(i).Convert<Pow2.N16>();

                MultiPrecision<Pow2.N16>.NearlyEqualBits(f, g, ignore_bits: 1);
                Console.WriteLine($"{i}\t{g}");
            }
        }

        [TestMethod]
        public void CDFExpCoefTest() {
            Assert.AreEqual(new(-3383087, BigInteger.Parse("9240544683286076160000")), NearZeroCoef.CDFExpTerm(45));
            Assert.AreEqual(new(5547827, BigInteger.Parse("64495011224067372748800")), NearZeroCoef.CDFExpTerm(46));
            Assert.AreEqual(0, NearZeroCoef.CDFExpTerm(47));
            Assert.AreEqual(new(2755547239, BigInteger.Parse("152136327665621957898240000")), NearZeroCoef.CDFExpTerm(48));
            Assert.AreEqual(new(-116796193, BigInteger.Parse("27640719096028874035200000")), NearZeroCoef.CDFExpTerm(49));
            Assert.AreEqual(0, NearZeroCoef.CDFExpTerm(50));

            for (int i = 0; i < 60; i++) {
                Fraction f = NearZeroCoef.CDFExpTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            for (int i = 0; i < 60; i++) {
                MultiPrecision<Pow2.N16> f = NearZeroCoef.CDFExpTerm(i).ToMultiPrecision<Pow2.N16>();
                MultiPrecision<Pow2.N16> g = NearZeroCoef<N24>.CDFExpTerm(i).Convert<Pow2.N16>();

                MultiPrecision<Pow2.N16>.NearlyEqualBits(f, g, ignore_bits: 1);
                Console.WriteLine($"{i}\t{g}");
            }
        }

        [TestMethod]
        public void CDFCoefTest() {
            Assert.AreEqual(new(77081860213, BigInteger.Parse("32798943916467176448000")), NearZeroCoef.CDFTerm(45));
            Assert.AreEqual(new(-23569729, BigInteger.Parse("31842319343839488000")), NearZeroCoef.CDFTerm(46));
            Assert.AreEqual(0, NearZeroCoef.CDFTerm(47));
            Assert.AreEqual(new(1640039579, BigInteger.Parse("8826443488733501030400")), NearZeroCoef.CDFTerm(48));
            Assert.AreEqual(new(-2286263713, BigInteger.Parse("39836773987611955200000")), NearZeroCoef.CDFTerm(49));
            Assert.AreEqual(0, NearZeroCoef.CDFTerm(50));

            for (int i = 0; i < 60; i++) {
                Fraction f = NearZeroCoef.CDFTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            for (int i = 0; i < 60; i++) {
                MultiPrecision<Pow2.N16> f = NearZeroCoef.CDFTerm(i).ToMultiPrecision<Pow2.N16>();
                MultiPrecision<Pow2.N16> g = NearZeroCoef<N24>.CDFTerm(i).Convert<Pow2.N16>();

                MultiPrecision<Pow2.N16>.NearlyEqualBits(f, g, ignore_bits: 1);
                Console.WriteLine($"{i}\t{g}");
            }
        }
    }
}