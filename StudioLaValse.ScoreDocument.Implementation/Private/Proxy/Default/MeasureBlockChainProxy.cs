using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Models;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class MeasureBlockChainProxy : IMeasureBlockChain
    {
        private readonly MeasureBlockChain source;
        private readonly ILayoutSelector layoutSelector;

        public TimeSignature TimeSignature => source.TimeSignature;



        public MeasureBlockChainProxy(MeasureBlockChain source, ILayoutSelector layoutSelector)
        {
            this.source = source;
            this.layoutSelector = layoutSelector;
        }




        public void Clear()
        {
            source.Clear();
        }

        public void Divide(params int[] steps)
        {
            source.Divide(steps);
        }

        public void Divide(params RythmicDuration[] steps)
        {
            source.Divide(steps);
        }

        public void DivideEqual(int number)
        {
            source.DivideEqual(number);
        }

        public IEnumerable<IMeasureBlock> ReadBlocks()
        {
            return source.GetBlocksCore().Select(e => e.Proxy(layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadBlocks();
        }
    }
}
