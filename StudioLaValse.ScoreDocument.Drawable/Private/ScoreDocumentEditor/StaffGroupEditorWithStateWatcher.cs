using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class StaffGroupEditorWithStateWatcher : IStaffGroupEditor
    {
        private readonly IStaffGroupEditor source;
        private readonly IScoreDocument scoreDocument;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public StaffGroupEditorWithStateWatcher(IStaffGroupEditor source, IScoreDocument scoreDocument, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.scoreDocument = scoreDocument;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int IndexInScore => source.IndexInScore;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public void ApplyLayout(IStaffGroupLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(scoreDocument);
        }

        public IStaffEditor EditStaff(int staffIndex)
        {
            return source.EditStaff(staffIndex).UseStateWatcher(scoreDocument, notifyEntityChanged);
        }

        public IEnumerable<IStaffEditor> EditStaves()
        {
            return source.EditStaves().Select(e => e.UseStateWatcher(scoreDocument, notifyEntityChanged));
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IEnumerable<IStaff> EnumerateStaves()
        {
            return source.EnumerateStaves();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffGroupLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
