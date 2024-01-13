using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Tests
{
    [TestClass]
    public class ScaleTests
    {
        [TestMethod]
        public void TestScale()
        {
            var fSharp = Step.FSharp;
            var major = ScaleStructure.Major;
            var fSharpMajor = new Scale(fSharp, major);
            var oneOctave = fSharpMajor.EnumerateSteps(8).ToList();
            var realScale = new List<Step>()
            {
                Step.FSharp,
                Step.GSharp,
                Step.ASharp,
                Step.B,
                Step.CSharp,
                Step.DSharp,
                Step.ESharp,
                Step.FSharp
            };

            Assert.IsTrue(oneOctave.SequenceEqual(realScale));

            var minor = ScaleStructure.Minor;
            var fSharpMinor = new Scale(fSharp, minor);
            oneOctave = fSharpMinor.EnumerateSteps().ToList();
            realScale = new List<Step>()
            {
                Step.FSharp,
                Step.GSharp,
                Step.A,
                Step.B,
                Step.CSharp,
                Step.D,
                Step.E,
                Step.FSharp
            };

            Assert.IsTrue(oneOctave.SequenceEqual(realScale));
        }

        [TestMethod]
        public void TestSteps()
        {
            var fSharp = Step.FSharp;

            var fifth = Interval.Fifth;
            Assert.IsTrue(fSharp + fifth == Step.CSharp);

            var nineth = Interval.AugmentedOctave;
            Assert.IsTrue(fSharp + nineth == Step.FDoubleSharp);

            var minorSecond = Interval.MinorSecond;
            Assert.IsTrue(fSharp + minorSecond == Step.G);

            var dimUnison = Interval.DiminishedUnison;
            Assert.IsTrue(fSharp + dimUnison == Step.F);
        }

        [TestMethod]
        public void TestPitch()
        {
            var fSharp = Step.FSharp;
            var pitch = new Pitch(fSharp, 4);

            var fifth = Interval.Fifth;
            Assert.IsTrue(pitch + fifth == new Pitch(Step.CSharp, 5));

            var nineth = Interval.AugmentedOctave;
            Assert.IsTrue(pitch + nineth == new Pitch(Step.FDoubleSharp, 5));

            var minorSecond = Interval.MinorSecond;
            Assert.IsTrue(pitch + minorSecond == new Pitch(Step.G, 4));

            var dimUnison = Interval.DiminishedUnison;
            Assert.IsTrue(pitch + dimUnison == new Pitch(Step.F, 4));

            var c = new Pitch(Step.C, 4);
            Assert.IsTrue(c + dimUnison == new Pitch(Step.CFlat, 3));
        }
    }
}