using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class InstrumentRibbonEditorWithStateWatcher : IInstrumentRibbonEditor
    {
        private readonly IInstrumentRibbonEditor source;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public InstrumentRibbonEditorWithStateWatcher(IInstrumentRibbonEditor source, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int IndexInScore => source.IndexInScore;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public void ApplyLayout(IInstrumentRibbonLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(source);
        }

        public IInstrumentMeasureEditor EditMeasure(int measureIndex)
        {
            return source.EditMeasure(measureIndex).UseStateWatcher(notifyEntityChanged);
        }

        public IEnumerable<IInstrumentMeasureEditor> EditMeasures()
        {
            return source.EditMeasures().Select(e => e.UseStateWatcher(notifyEntityChanged));
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IInstrumentRibbonLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
