using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Tests
{
    [TestClass]
    public class ChordTests
    {
        [TestMethod]
        public void TestChord()
        {
            var cMajor = new Chord(Step.C, ChordStructure.Major)
                .EnumerateSteps()
                .ToList();
            var realChord = new List<Step>()
            {
                Step.C,
                Step.E,
                Step.G
            };

            Assert.IsTrue(cMajor.SequenceEqual(realChord));

            var aMinor = new Chord(Step.A, ChordStructure.Minor)
                .EnumerateSteps()
                .ToList();
            realChord = new List<Step>()
            {
                Step.A,
                Step.C,
                Step.E,
            };

            Assert.IsTrue(aMinor.SequenceEqual(realChord));
        }
    }
}