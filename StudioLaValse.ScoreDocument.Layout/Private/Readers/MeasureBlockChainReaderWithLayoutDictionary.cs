using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class MeasureBlockChainReaderWithLayoutDictionary : IMeasureBlockChainReader
    {
        private readonly IMeasureBlockChainReader source;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;

        public MeasureBlockChainReaderWithLayoutDictionary(IMeasureBlockChainReader source, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.source = source;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        public int Voice => source.Voice;

        public IEnumerable<IMeasureBlock> EnumerateBlocks()
        {
            return source.EnumerateBlocks();
        }

        public IEnumerable<IMeasureBlockReader> ReadBlocks()
        {
            return source.ReadBlocks().Select(e => e.UseLayout(scoreLayoutDictionary));
        }
    }
}
