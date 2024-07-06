using StudioLaValse.ScoreDocument.Implementation.Private.Layout;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class MeasureBlockChain : ScoreElement
    {
        private readonly List<MeasureBlock> blocks;
        private readonly ScoreDocumentStyleTemplate scoreDocumentStyle;
        private readonly IKeyGenerator<int> keyGenerator;

        public InstrumentMeasure RibbonMeasure { get; }
        public int Voice { get; }
        public TimeSignature TimeSignature => RibbonMeasure.TimeSignature;



        public MeasureBlockChain(InstrumentMeasure ribbonMeasure, ScoreDocumentStyleTemplate scoreDocumentStyle, int voice, IKeyGenerator<int> keyGenerator, Guid guid) : base(keyGenerator, guid)
        {
            this.keyGenerator = keyGenerator;

            blocks = [];

            RibbonMeasure = ribbonMeasure;
            this.scoreDocumentStyle = scoreDocumentStyle;
            Voice = voice;
        }




        public MeasureBlock? BlockRight(MeasureBlock block)
        {
            var index = IndexOfOrThrow(block);
            return blocks.ElementAtOrDefault(index + 1);
        }
        public MeasureBlock? BlockLeft(MeasureBlock block)
        {
            var index = IndexOfOrThrow(block);
            return blocks.ElementAtOrDefault(index - 1);
        }


        public int IndexOfOrThrow(MeasureBlock block)
        {
            var index = blocks.IndexOf(block);
            return index == -1 ? throw new Exception("Measure block does not exist in this measure chain") : index;
        }


        public void Divide(params int[] steps)
        {
            var stepsAsRythmicDurations = TimeSignature.Divide(steps).ToArray();

            Divide(stepsAsRythmicDurations);
        }
        public void Divide(params RythmicDuration[] steps)
        {
            if (blocks.Any(b => b.Containers.Any(c => c.EnumerateNotesCore().Any())))
            {
                throw new InvalidOperationException("There are already blocks that contain content in this block chain.");
            }
            if (!steps.Sum().Equals(TimeSignature as Duration))
            {
                throw new InvalidOperationException("The sum of the specified steps does not equal the total duration of the measure.");
            }

            Clear();
            foreach (var rythmicDuration in steps)
            {
                AppendCore(rythmicDuration, Guid.NewGuid(), Guid.NewGuid());
            }
        }
        public void DivideEqual(int number)
        {
            var stepsAsRythmicDurations = TimeSignature.DivideEqual(number).ToArray();

            Divide(stepsAsRythmicDurations);
        }
        public MeasureBlock AppendCore(RythmicDuration rythmicDuration, Guid blockGuid, Guid secondaryLayoutGuid)
        {
            ThrowIfWillCauseOverflow(rythmicDuration);

            var layout = new AuthorMeasureBlockLayout(scoreDocumentStyle.MeasureBlockStyleTemplate, Voice);
            var secondaryLayout = new UserMeasureBlockLayout(secondaryLayoutGuid, layout, scoreDocumentStyle.MeasureBlockStyleTemplate);
            var newBlock = new MeasureBlock(rythmicDuration, this, scoreDocumentStyle, layout, secondaryLayout, keyGenerator, blockGuid);
            blocks.Add(newBlock);
            return newBlock;
        }
        public void ThrowIfWillCauseOverflow(RythmicDuration rythmicDuration)
        {
            var newLength = blocks.Select(e => e.RythmicDuration).Sum() + rythmicDuration;
            if (newLength > RibbonMeasure.TimeSignature)
            {
                throw new Exception("New measure block cannot fit in this measure.");
            }
        }
        public void Clear()
        {
            blocks.Clear();
        }


        public IEnumerable<MeasureBlock> GetBlocksCore()
        {
            return blocks;
        }
    }
}
