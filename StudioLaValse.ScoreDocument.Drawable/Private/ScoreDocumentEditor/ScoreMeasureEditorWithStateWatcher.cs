using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class ScoreMeasureEditorWithStateWatcher : IScoreMeasureEditor
    {
        private readonly IScoreMeasureEditor source;
        private readonly IScoreDocument host;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public ScoreMeasureEditorWithStateWatcher(IScoreMeasureEditor source, IScoreDocument host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.host = host;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int IndexInScore => source.IndexInScore;

        public TimeSignature TimeSignature => source.TimeSignature;

        public int Id => source.Id;

        public void ApplyLayout(IScoreMeasureLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(host);
        }

        public IInstrumentMeasureEditor EditMeasure(int ribbonIndex)
        {
            return source.EditMeasure(ribbonIndex).UseStateWatcher(notifyEntityChanged);
        }

        public IEnumerable<IInstrumentMeasureEditor> EditMeasures()
        {
            return source.EditMeasures().Select(e => e.UseStateWatcher(notifyEntityChanged));
        }

        public IStaffSystemEditor EditStaffSystemOrigin()
        {
            return source.EditStaffSystemOrigin().UseStateWatcher(host, notifyEntityChanged);
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffSystem GetStaffSystemOrigin()
        {
            return source.GetStaffSystemOrigin();
        }

        public IScoreMeasureLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
