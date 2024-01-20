using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class ScoreDocumentEditorWithStateWatcher : IScoreDocumentEditor
    {
        private readonly IScoreDocumentEditor source;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public ScoreDocumentEditorWithStateWatcher(IScoreDocumentEditor source, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int NumberOfMeasures => source.NumberOfMeasures;

        public int NumberOfInstruments => source.NumberOfInstruments;

        public int Id => source.Id;

        public void AddInstrumentRibbon(Instrument instrument)
        {
            source.AddInstrumentRibbon(instrument);
            notifyEntityChanged.Invalidate(source);
        }

        public void AppendScoreMeasure(TimeSignature? timeSignature = null)
        {
            source.AppendScoreMeasure(timeSignature);
            notifyEntityChanged.Invalidate(source);
        }

        public void ApplyLayout(IScoreDocumentLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(source);
        }

        public void Clear()
        {
            source.Clear();
            notifyEntityChanged.Invalidate(source);
        }

        public IInstrumentRibbonEditor EditInstrumentRibbon(int indexInScore)
        {
            return source.EditInstrumentRibbon(indexInScore);
        }

        public IEnumerable<IInstrumentRibbonEditor> EditInstrumentRibbons()
        {
            return source.EditInstrumentRibbons().Select(e => e.UseStateWatcher(notifyEntityChanged));
        }

        public IScoreMeasureEditor EditScoreMeasure(int indexInScore)
        {
            return source.EditScoreMeasure(indexInScore).UseStateWatcher(source, notifyEntityChanged);
        }

        public IEnumerable<IScoreMeasureEditor> EditScoreMeasures()
        {
            return source.EditScoreMeasures().Select(e => e.UseStateWatcher(source, notifyEntityChanged));
        }

        public IEnumerable<IStaffSystemEditor> EditStaffSystems()
        {
            return source.EditStaffSystems().Select(e => e.UseStateWatcher(source, notifyEntityChanged));
        }

        public IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons()
        {
            return source.EnumerateInstrumentRibbons();
        }

        public IEnumerable<IScoreMeasure> EnumerateScoreMeasures()
        {
            return source.EnumerateScoreMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
        {
            source.InsertScoreMeasure(index, timeSignature);
            notifyEntityChanged.Invalidate(source);
        }

        public IScoreDocumentLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public void RemoveInstrumentRibbon(int elementId)
        {
            source.RemoveInstrumentRibbon(elementId);
            notifyEntityChanged.Invalidate(source);
        }

        public void RemoveScoreMeasure(int indexInScore)
        {
            source.RemoveScoreMeasure(indexInScore);
            notifyEntityChanged.Invalidate(source);
        }
    }
}
