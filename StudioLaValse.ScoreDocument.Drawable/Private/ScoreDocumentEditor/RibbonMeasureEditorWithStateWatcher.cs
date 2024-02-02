using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class RibbonMeasureEditorWithStateWatcher : IInstrumentMeasureEditor
    {
        private readonly IInstrumentMeasureEditor source;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public RibbonMeasureEditorWithStateWatcher(IInstrumentMeasureEditor source, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int MeasureIndex => source.MeasureIndex;

        public int RibbonIndex => source.RibbonIndex;

        public TimeSignature TimeSignature => source.TimeSignature;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public void AddVoice(int voice)
        {
            source.AddVoice(voice);
            notifyEntityChanged.Invalidate(source);
        }

        public void ApplyLayout(IInstrumentMeasureLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(source);
        }

        public void Clear()
        {
            source.Clear();
            notifyEntityChanged.Invalidate(source);
        }

        public void RemoveVoice(int voice)
        {
            source.RemoveVoice(voice);
            notifyEntityChanged.Invalidate(source);
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return source.EnumerateVoices();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IInstrumentMeasureLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public IMeasureBlockChainEditor EditBlockChainAt(int voice)
        {
            return source.EditBlockChainAt(voice).UseStateWatcher(source, notifyEntityChanged);
        }

        public IMeasureBlockChain BlockChainAt(int voice)
        {
            return source.BlockChainAt(voice);
        }
    }
}
