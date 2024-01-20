using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

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

        public void AppendBlock(int voice, Duration duration, bool grace)
        {
            source.AppendBlock(voice, duration, grace);
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

        public void ClearVoice(int voice)
        {
            source.ClearVoice(voice);
            notifyEntityChanged.Invalidate(source);
        }

        public IEnumerable<IMeasureBlockEditor> EditBlocks(int voice)
        {
            return source.EditBlocks(voice).Select(e => e.UseStateWatcher(source, notifyEntityChanged));
        }

        public IEnumerable<IMeasureBlock> EnumerateBlocks(int voice)
        {
            return source.EnumerateBlocks(voice);
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return source.EnumerateVoices();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public void PrependBlock(int voice, Duration duration, bool grace)
        {
            source.PrependBlock(voice, duration, grace);
            notifyEntityChanged.Invalidate(source);
        }

        public IInstrumentMeasureLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
