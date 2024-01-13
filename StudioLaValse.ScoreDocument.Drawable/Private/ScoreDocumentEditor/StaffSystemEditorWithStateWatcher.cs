using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class StaffSystemEditorWithStateWatcher : IStaffSystemEditor
    {
        private readonly IStaffSystemEditor source;
        private readonly IScoreDocument scoreDocument;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public StaffSystemEditorWithStateWatcher(IStaffSystemEditor source, IScoreDocument scoreDocument, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.scoreDocument = scoreDocument;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int Id => source.Id;

        public void ApplyLayout(IStaffSystemLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(scoreDocument);
        }

        public IStaffGroupEditor EditStaffGroup(int indexInScore)
        {
            return source.EditStaffGroup(indexInScore).UseUseStateWatcherLayout(scoreDocument, notifyEntityChanged);
        }

        public IEnumerable<IStaffGroupEditor> EditStaffGroups()
        {
            return source.EditStaffGroups().Select(e => e.UseUseStateWatcherLayout(scoreDocument, notifyEntityChanged));
        }

        public IEnumerable<IScoreMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IEnumerable<IStaffGroup> EnumerateStaffGroups()
        {
            return source.EnumerateStaffGroups();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffSystemLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
