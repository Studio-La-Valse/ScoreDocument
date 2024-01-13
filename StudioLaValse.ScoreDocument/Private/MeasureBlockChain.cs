namespace StudioLaValse.ScoreDocument.Private
{
    internal class MeasureBlockChain
    {
        private readonly List<MeasureBlock> blocks;
        private readonly IKeyGenerator<int> keyGenerator;


        public InstrumentMeasure RibbonMeasure { get; }
        public int Voice { get; }



        public IEnumerable<MeasureBlock> Blocks =>
            blocks;




        public MeasureBlockChain(InstrumentMeasure ribbonMeasure, int voice, IKeyGenerator<int> keyGenerator)
        {
            this.keyGenerator = keyGenerator;

            blocks = [];

            RibbonMeasure = ribbonMeasure;

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
            if (index == -1)
            {
                throw new Exception("Measure block does not exist in this measure chain");
            }

            return index;
        }



        public IMeasureBlockEditor Prepend(Duration duration, bool grace)
        {
            var newLength = blocks.Select(e => e.Duration).Sum() + duration;
            if (newLength > RibbonMeasure.TimeSignature)
            {
                throw new Exception("New measure block cannot fit in this measure.");
            }

            var newBlock = new MeasureBlock(duration, this, grace, keyGenerator);
            blocks.Insert(0, newBlock);
            return newBlock;
        }
        public IMeasureBlockEditor Append(Duration duration, bool grace)
        {
            var newLength = blocks.Select(e => e.Duration).Sum() + duration;
            if (newLength > RibbonMeasure.TimeSignature)
            {
                throw new Exception("New measure block cannot fit in this measure.");
            }

            var newBlock = new MeasureBlock(duration, this, grace, keyGenerator);
            blocks.Add(newBlock);
            return newBlock;
        }
    }
}
