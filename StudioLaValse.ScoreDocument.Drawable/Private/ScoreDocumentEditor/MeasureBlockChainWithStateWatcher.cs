using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class MeasureBlockChainWithStateWatcher : IMeasureBlockChainEditor
    {
        private readonly IMeasureBlockChainEditor source;
        private readonly IInstrumentMeasure host;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public MeasureBlockChainWithStateWatcher(IMeasureBlockChainEditor source, IInstrumentMeasure host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.host = host;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int Voice => source.Voice;

        public void Append(RythmicDuration duration, bool grace)
        {
            source.Append(duration, grace);
            notifyEntityChanged.Invalidate(host);
        }

        public void Clear()
        {
            source.Clear();
            notifyEntityChanged.Invalidate(host);
        }

        public void Divide(params int[] steps)
        {
            source.Divide(steps);
            notifyEntityChanged.Invalidate(host);
        }

        public void DivideEqual(int number)
        {
            source.DivideEqual(number);
            notifyEntityChanged.Invalidate(host);
        }

        public IEnumerable<IMeasureBlockEditor> EditBlocks()
        {
            return source.EditBlocks().Select(e => e.UseStateWatcher(host, notifyEntityChanged));
        }

        public IEnumerable<IMeasureBlock> EnumerateBlocks()
        {
            return source.EnumerateBlocks();
        }

        public void Insert(Position position, RythmicDuration duration, bool grace)
        {
            source.Insert(position, duration, grace);
            notifyEntityChanged.Invalidate(host);
        }

        public void Prepend(RythmicDuration duration, bool grace)
        {
            source.Prepend(duration, grace);
            notifyEntityChanged.Invalidate(host);
        }
    }
}
