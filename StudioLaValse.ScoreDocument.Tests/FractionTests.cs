using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Tests
{
    [TestClass]
    public class FractionTests
    {
        [TestMethod]
        public void TestSimplify()
        {
            var fraction = new Fraction(4, 8);
            var simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numinator, 1);
            Assert.AreEqual(simplified.Denominator, 2);

            fraction = new Fraction(3, 8);
            simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numinator, 3);
            Assert.AreEqual(simplified.Denominator, 8);
        }

        [TestMethod]
        public void TestSimplify2()
        {
            var fraction = new Duration(4, 16);
            var simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numinator, 1);
            Assert.AreEqual(simplified.Denominator, 4);

            fraction = new Duration(3, 8);
            simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numinator, 3);
            Assert.AreEqual(simplified.Denominator, 8);

            fraction = new Duration(0, 8);
            simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numinator, 0);
            Assert.AreEqual(simplified.Denominator, 1);
        }

        [TestMethod]
        public void TestAddOperator()
        {
            var duration = new Duration(3, 8);
            var secondDuration = new Duration(8, 64);
            var added = duration + secondDuration;

            Assert.AreEqual(added.Numinator, 1);
            Assert.AreEqual(added.Denominator, 2);

            var thirdDuration = new Duration(1, 16);
            added += thirdDuration;
            Assert.AreEqual(added.Numinator, 9);
            Assert.AreEqual(added.Denominator, 16);
        }

        [TestMethod]
        public void TestPowerOfTwo()
        {
            var powerOfTwo = new PowerOfTwo(0);
            Assert.IsTrue(powerOfTwo == 1);

            powerOfTwo = new PowerOfTwo(3);
            Assert.AreEqual(powerOfTwo.Value, 8);
            Assert.IsTrue(powerOfTwo == 8);
            Assert.IsFalse(powerOfTwo == 7);

            //                                              4
            var canHalf = powerOfTwo.TryDivideByTwo(out powerOfTwo);
            Assert.IsTrue(canHalf);
            Assert.IsNotNull(powerOfTwo);

            //                                          2
            canHalf = powerOfTwo.TryDivideByTwo(out powerOfTwo);
            Assert.IsTrue(canHalf);
            Assert.IsNotNull(powerOfTwo);

            //                                          1
            canHalf = powerOfTwo.TryDivideByTwo(out powerOfTwo);
            Assert.IsTrue(canHalf);
            Assert.IsNotNull(powerOfTwo);

            //                                          null
            canHalf = powerOfTwo.TryDivideByTwo(out var _powerOfTwo);
            Assert.IsFalse(canHalf);
            Assert.IsNull(_powerOfTwo);

            var two = powerOfTwo.Double();
            Assert.IsTrue(two == 2);
        }

        [TestMethod]
        public void TestRythmicDuration()
        {
            var rythmicDuration = new RythmicDuration(8);
            Assert.AreEqual(rythmicDuration.Numinator, 1);
            Assert.AreEqual(rythmicDuration.Denominator, 8);

            rythmicDuration = new RythmicDuration(8, 1);
            Assert.AreEqual(rythmicDuration.Numinator, 3);
            Assert.AreEqual(rythmicDuration.Denominator, 16);

            rythmicDuration = new RythmicDuration(4, 2);
            Assert.AreEqual(rythmicDuration.Numinator, 7);
            Assert.AreEqual(rythmicDuration.Denominator, 16);
        }

        [TestMethod]
        public void TestTuplet()
        {
            var oneEight = new RythmicDuration(8);
            var threeEights = new RythmicDuration[]
            {
                new RythmicDuration(8),
                new RythmicDuration(4)
            };
            var tuplet = new Tuplet(new Duration(1, 4), threeEights);

            //the length of one eights in a tuplet of three eights in the space of one fourth is one twelveth.
            var adjustedLength = oneEight.ToActualDuration(tuplet);
            Assert.AreEqual(adjustedLength.Numinator, 1);
            Assert.AreEqual(adjustedLength.Denominator, 12);

            var oneSixteenth = new RythmicDuration(16);
            var thirteenOneSixtheenths = new RythmicDuration[13];
            for (int i = 0; i < 13; i++)
            {
                thirteenOneSixtheenths[i] = oneSixteenth;
            }
            tuplet = new Tuplet(new Duration(1, 4), thirteenOneSixtheenths);
            adjustedLength = oneSixteenth.ToActualDuration(tuplet);

            Assert.AreEqual(adjustedLength.Numinator, 1);
            Assert.AreEqual(adjustedLength.Denominator, 52);
        }
    }
}