using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Tests
{
    [TestClass]
    public class KeySignatureTests
    {
        [TestMethod]
        public void TestScales()
        {
            var keySignature = new KeySignature(Step.AFlat, MajorOrMinor.Minor);

            var clef = Clef.Treble;
            var predictedLines = keySignature
                .EnumerateFlatLines(clef)
                .ToList();
            var trueLines = new List<int>
            {
                4, 1, 5, 2, 6, 3, 7
            };

            Assert.IsTrue(predictedLines.SequenceEqual(trueLines));

            clef = Clef.Bass;
            predictedLines = keySignature
                .EnumerateFlatLines(clef)
                .ToList();
            trueLines = new List<int>
            {
                6, 3, 7, 4, 8, 5, 9
            };

            Assert.IsTrue(predictedLines.SequenceEqual(trueLines));

            keySignature = new KeySignature(Step.FSharp, MajorOrMinor.Major);
            clef = Clef.Treble;
            predictedLines = keySignature
                .EnumerateSharpLines(clef)
                .ToList();

            trueLines = new List<int>
            {
                0, 3, -1, 2, 5, 1
            };

            Assert.IsTrue(predictedLines.SequenceEqual(trueLines));
            clef = Clef.Bass;

            predictedLines = keySignature
                .EnumerateSharpLines(clef)
                .ToList();

            trueLines = new List<int>
            {
                2, 5, 1, 4, 7, 3
            };

            Assert.IsTrue(predictedLines.SequenceEqual(trueLines));
        }
    }
}