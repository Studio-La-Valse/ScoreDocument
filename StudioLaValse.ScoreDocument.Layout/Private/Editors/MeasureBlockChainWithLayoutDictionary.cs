using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class MeasureBlockChainWithLayoutDictionary : IMeasureBlockChainEditor
    {
        private readonly IMeasureBlockChainEditor source;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;

        public MeasureBlockChainWithLayoutDictionary(IMeasureBlockChainEditor source, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.source = source;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        public int Voice => source.Voice;

        public void Append(RythmicDuration duration, bool grace)
        {
            source.Append(duration, grace);
        }

        public void Clear()
        {
            source.Clear();
        }

        public void Divide(params int[] steps)
        {
            source.Divide(steps);
        }

        public void DivideEqual(int number)
        {
            source.DivideEqual(number);
        }

        public IEnumerable<IMeasureBlockEditor> EditBlocks()
        {
            return source.EditBlocks().Select(e => e.UseLayout(scoreLayoutDictionary));
        }

        public IEnumerable<IMeasureBlock> EnumerateBlocks()
        {
            return source.EnumerateBlocks();
        }

        public void Insert(Position position, RythmicDuration duration, bool grace)
        {
            source.Insert(position, duration, grace);
        }

        public void Prepend(RythmicDuration duration, bool grace)
        {
            source.Prepend(duration, grace);
        }
    }
}
