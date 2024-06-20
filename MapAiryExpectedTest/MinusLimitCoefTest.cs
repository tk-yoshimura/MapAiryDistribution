using MapAiryExpected;
using MultiPrecision;
using System.Numerics;

namespace MapAiryExpectedTest {
    [TestClass]
    public class MinusLimitCoefTest {
        [TestMethod]
        public void PDFCoefTest() {
            Assert.AreEqual(
                new(BigInteger.Parse("389686629574638365640674616326204491681890167886647825092095419072935699926212685307267832993391867901425408571809985326865176346260679443359375")
                    , BigInteger.Parse("104143424250312223145378531102893360534392739944864996511681476034991915139883963271390913424785408")), 
                MinusLimitCoef.PDFTerm(45)
            );
            Assert.AreEqual(
                new(BigInteger.Parse("-1235120243885285313775632124154955706048613536902988687016898432825997373431346893313674685103707555571565689542451135667093238715915533510498046875")
                    , BigInteger.Parse("9997768728029973421956338985877762611301703034707039665121421699359223853428860474053527688779399168")), 
                MinusLimitCoef.PDFTerm(46)
            );
            Assert.AreEqual(
                new(BigInteger.Parse("2001814565488544867592739937393696827835173110076280068798132725968518083428358502088705726335636660546042902146727984243421860833720548194940185546875")
                    , BigInteger.Parse("479892898945438724253904271322132605342481745665937903925828241569242744964585302754569329061411160064")), 
                MinusLimitCoef.PDFTerm(47)
            );

            for (int i = 0; i < 60; i++) {
                Fraction f = MinusLimitCoef.PDFTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }
        }

        [TestMethod]
        public void CDFCoefTest() {
            for (int i = 0; i < 60; i++) {
                Fraction f = MinusLimitCoef.CDFTerm(i);

                Console.WriteLine($"{i}\t{f}");
            }

            Console.WriteLine($"---------");

            BigInteger g = 1;
            for (int i = 0; i < 60; i++) {
                Fraction f = MinusLimitCoef.CDFTerm(i);

                Console.WriteLine($"{i}\t{f * g}");

                if (i == 32) {
                    Assert.AreEqual(BigInteger.One, (f * g).Denom);
                    Assert.AreEqual(BigInteger.Parse("384515714305538729765212659740818276597535036878622352456379456831626348453854076990681487028680192702900941949462890625"), (f * g).Numer);
                }

                g *= 48 * (i + 1);
            }
        }
    }
}