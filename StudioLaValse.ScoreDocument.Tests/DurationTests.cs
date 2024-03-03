using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Tests
{
    [TestClass]
    public class DurationTests
    {
        [TestMethod]
        public void TestDivisions()
        {
            _Assert(new TimeSignature(4, 4), [3, 3, 2], [new RythmicDuration(4, 1), new RythmicDuration(4, 1), new RythmicDuration(4)]);
            _Assert(new TimeSignature(4, 4), [1, 1], [new RythmicDuration(2), new RythmicDuration(2)]);
            _Assert(new TimeSignature(7, 8), [2, 3, 2], [new RythmicDuration(4), new RythmicDuration(4, 1), new RythmicDuration(4)]);
            _Assert(new TimeSignature(3, 8), [1, 1, 1], [new RythmicDuration(8), new RythmicDuration(8), new RythmicDuration(8)]);
            _Assert(new TimeSignature(3, 8), [3, 3], [new RythmicDuration(8, 1), new RythmicDuration(8, 1)]);
            _Assert(new TimeSignature(4, 4), [7, 1], [new RythmicDuration(2, 2), new RythmicDuration(8)]);
            _Assert(new TimeSignature(4, 4), [15, 1], [new RythmicDuration(2, 3), new RythmicDuration(16)]);
        }

        private void _Assert(TimeSignature timeSignature, int[] values, RythmicDuration[] expectedLenghts)
        {
            var lengths = timeSignature.Divide(values);
            Assert.IsTrue(lengths.SequenceEqual(expectedLenghts));
        }

        [TestMethod]
        public void TestDivisionsExceptions()
        {
            _AssertException(new TimeSignature(4, 4), [0]);
            _AssertException(new TimeSignature(4, 4), [7, 5]);
            _AssertException(new TimeSignature(4, 4), [2, 2, 2]);
            _AssertException(new TimeSignature(5, 8), [1]);
        }

        private void _AssertException(TimeSignature timeSignature, int[] values)
        {
            Assert.ThrowsException<InvalidOperationException>(() => timeSignature.Divide(values).ToArray());
        }

        [TestMethod]
        public void TestEqualDivisions()
        {
            _AssertEqual(new TimeSignature(4, 4), 2, [new RythmicDuration(2), new RythmicDuration(2)]);
            _AssertEqual(new TimeSignature(3, 8), 2, [new RythmicDuration(8, 1), new RythmicDuration(8, 1)]);
            _AssertEqual(new TimeSignature(3, 8), 6, [new RythmicDuration(16), new RythmicDuration(16), new RythmicDuration(16), new RythmicDuration(16), new RythmicDuration(16), new RythmicDuration(16)]);
            _AssertEqual(new TimeSignature(6, 8), 2, [new RythmicDuration(4, 1), new RythmicDuration(4, 1)]);
            _AssertEqual(new TimeSignature(6, 8), 3, [new RythmicDuration(4), new RythmicDuration(4), new RythmicDuration(4)]);
            _AssertEqual(new TimeSignature(7, 16), 1, [new RythmicDuration(4, 2)]);
        }

        private void _AssertEqual(TimeSignature timeSignature, int number, RythmicDuration[] expectedLenghts)
        {
            var lengths = timeSignature.DivideEqual(number);
            Assert.IsTrue(lengths.SequenceEqual(expectedLenghts));
        }
    }
}
