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

            Assert.AreEqual(simplified.Numerator, 1);
            Assert.AreEqual(simplified.Denominator, 2);

            fraction = new Fraction(3, 8);
            simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numerator, 3);
            Assert.AreEqual(simplified.Denominator, 8);
        }

        [TestMethod]
        public void TestSimplify2()
        {
            var fraction = new Duration(4, 16);
            var simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numerator, 1);
            Assert.AreEqual(simplified.Denominator.Value, 4);

            fraction = new Duration(3, 8);
            simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numerator, 3);
            Assert.AreEqual(simplified.Denominator.Value, 8);

            fraction = new Duration(0, 8);
            simplified = fraction.Simplify();

            Assert.AreEqual(simplified.Numerator, 0);
            Assert.AreEqual(simplified.Denominator.Value, 1);
        }

        [TestMethod]
        public void TestAddOperator()
        {
            var duration = new Duration(3, 8);
            var secondDuration = new Duration(8, 64);
            var added = duration + secondDuration;

            Assert.AreEqual(added.Numerator, 1);
            Assert.AreEqual(added.Denominator.Value, 2);

            var thirdDuration = new Duration(1, 16);
            added += thirdDuration;
            Assert.AreEqual(added.Numerator, 9);
            Assert.AreEqual(added.Denominator.Value, 16);
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
            Assert.AreEqual(rythmicDuration.Numerator, 1);
            Assert.AreEqual(rythmicDuration.Denominator.Value, 8);

            rythmicDuration = new RythmicDuration(8, 1);
            Assert.AreEqual(rythmicDuration.Numerator, 3);
            Assert.AreEqual(rythmicDuration.Denominator.Value, 16);

            rythmicDuration = new RythmicDuration(4, 2);
            Assert.AreEqual(rythmicDuration.Numerator, 7);
            Assert.AreEqual(rythmicDuration.Denominator.Value, 16);
        }

        [TestMethod]
        public void TestTuplet()
        {
            var oneEighth = new RythmicDuration(8);
            var threeEights = new RythmicDuration[]
            {
                new RythmicDuration(8),
                new RythmicDuration(4)
            };
            var tuplet = new Tuplet(new Duration(1, 4), threeEights);

            //the length of one eights in a tuplet of three eights in the space of one fourth is one twelveth.
            var adjustedLength = tuplet.ToActualDuration(oneEighth);
            Assert.AreEqual(adjustedLength.Numerator, 1);
            Assert.AreEqual(adjustedLength.Denominator, 12);

            var oneSixteenth = new RythmicDuration(16);
            var thirteenOneSixtheenths = new RythmicDuration[13];
            for (int i = 0; i < 13; i++)
            {
                thirteenOneSixtheenths[i] = oneSixteenth;
            }
            tuplet = new Tuplet(new Duration(1, 4), thirteenOneSixtheenths);
            adjustedLength = tuplet.ToActualDuration(oneSixteenth);

            Assert.AreEqual(adjustedLength.Numerator, 1);
            Assert.AreEqual(adjustedLength.Denominator, 52);
        }

        [TestMethod]
        public void TestRythmicDurationFromDuration()
        {
            var fraction = new Fraction(7, 8);
            Assert.IsTrue(RythmicDuration.TryConstruct(fraction, out var converted));
            Assert.AreEqual(converted.Dots, 2);
            Assert.AreEqual(converted.PowerOfTwo.Value, 2);

            var duration = new Duration(1, 8);
            var canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 0);
            Assert.AreEqual(converted.PowerOfTwo.Value, 8);

            duration = new Duration(3, 8);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 1);
            Assert.AreEqual(converted.PowerOfTwo.Value, 4);

            duration = new Duration(7, 16);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 2);
            Assert.AreEqual(converted.PowerOfTwo.Value, 4);

            duration = new Duration(5, 8);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsFalse(canConvert);
            Assert.IsNull(converted);

            //consider 7 / 32 => 3 / 16 => 1 / 8 with two dots
            //consider 15 / 32 =>  7 / 16 => 3 / 8 => 1 / 4 with 3 dots
            //consider 63 / 256 => 31 / 128 => 15 / 64 => 7 / 32 => 3 / 16 => 1 / 8 with 5 dots
            //consider 7 / 64 => 3 / 32 => 1 / 16 with 2 dots

            duration = new Duration(7, 32);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 2);
            Assert.AreEqual(converted.PowerOfTwo.Value, 8);

            duration = new Duration(15, 32);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 3);
            Assert.AreEqual(converted.PowerOfTwo.Value, 4);

            duration = new Duration(63, 256);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 5);
            Assert.AreEqual(converted.PowerOfTwo.Value, 8);

            duration = new Duration(7, 64);
            canConvert = RythmicDuration.TryConstruct(duration, out converted);
            Assert.IsTrue(canConvert);
            Assert.AreEqual(converted.Dots, 2);
            Assert.AreEqual(converted.PowerOfTwo.Value, 16);
        }
    }
}