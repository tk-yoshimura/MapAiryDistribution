using MapAiryExpected;
using MultiPrecision;
using System.Numerics;

namespace MapAiryExpectedTest {
    [TestClass]
    public class PlusLimitCoefTest {
        [TestMethod]
        public void PDFCoefTest() {
            Assert.AreEqual(
                new(BigInteger.Parse("1948433147873191828203373081631022458409450839433239125460477095364678499631063426536339164966959339507127042859049926634325881731303397216796875")
                    , BigInteger.Parse("1928581930561337465655157983386914083970235924904907342808916222870220650738591912433165063421952")),
                PlusLimitCoef.PDFTerm(45)
            );
            Assert.AreEqual(
                new(BigInteger.Parse("-28407765609361562216839538855563981239118111348768739801388663954997939588920978546214517757385273778146010859476376120343144490466057270741455078125")
                    , BigInteger.Parse("833147394002497785163028248823146884275141919558919972093451808279935321119071706171127307398283264")),
                PlusLimitCoef.PDFTerm(46)
            );
            Assert.AreEqual(
                new(BigInteger.Parse("94085284577961608776858777057503750908253136173585163233512238120520349921132849598169169137774923045664016400896215259440827459184865765162188720703125")
                    , BigInteger.Parse("79982149824239787375650711887022100890413624277656317320971373594873790827430883792428221510235193344")),
                PlusLimitCoef.PDFTerm(47)
            );

            for (int i = 0; i < 60; i++) {
                Fraction f = PlusLimitCoef.PDFTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            for (int i = 0; i < 60; i++) {
                MultiPrecision<Pow2.N16> f = PlusLimitCoef.PDFTerm(i).ToMultiPrecision<Pow2.N16>();
                MultiPrecision<Pow2.N16> g = PlusLimitCoef<N24>.PDFTerm(i).Convert<Pow2.N16>();

                MultiPrecision<Pow2.N16>.NearlyEqualBits(f, g, ignore_bits: 1);
                Console.WriteLine($"{i}\t{g}");
            }
        }

        [TestMethod]
        public void CDFCoefTest() {
            Assert.AreEqual(
                new(BigInteger.Parse("21892507279474065485431158220573286049544391454306057589443562869266050557652398050970102977156846511316034189427527265554223390239364013671875")
                    , BigInteger.Parse("2892872895842006198482736975080371125955353887357361014213374334305330976107887868649747595132928")),
                PlusLimitCoef.CDFTerm(45)
            );
            Assert.AreEqual(
                new(BigInteger.Parse("-312173248454522661723511415995208585045253970865590547268007296208768566911219544463895799531706305254351767686553583740034554840286343634521484375")
                    , BigInteger.Parse("1249721091003746677744542373234720326412712879338379958140177712419902981678607559256690961097424896")),
                PlusLimitCoef.CDFTerm(46)
            );
            Assert.AreEqual(
                new(BigInteger.Parse("3035009179934245444414799259919475835750101166889843975274588326468398384552672567682876423799191065989161819383748879336800885780156960166522216796875")
                    , BigInteger.Parse("359919674209079043190428203491599454006861309249453427944371181176932058723438977065926996796058370048")),
                PlusLimitCoef.CDFTerm(47)
            );

            for (int i = 0; i < 60; i++) {
                Fraction f = PlusLimitCoef.CDFTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            for (int i = 0; i < 60; i++) {
                MultiPrecision<Pow2.N16> f = PlusLimitCoef.CDFTerm(i).ToMultiPrecision<Pow2.N16>();
                MultiPrecision<Pow2.N16> g = PlusLimitCoef<N24>.CDFTerm(i).Convert<Pow2.N16>();

                MultiPrecision<Pow2.N16>.NearlyEqualBits(f, g, ignore_bits: 1);
                Console.WriteLine($"{i}\t{g}");
            }
        }
    }
}