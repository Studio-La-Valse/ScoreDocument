using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class StaffEditorWithLayoutStateWatcher : IStaffEditor
    {
        private readonly IStaffEditor source;
        private readonly IScoreDocument scoreDocument;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public StaffEditorWithLayoutStateWatcher(IStaffEditor source, IScoreDocument scoreDocument, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.scoreDocument = scoreDocument;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int IndexInStaffGroup => source.IndexInStaffGroup;

        public int Id => source.Id;

        public void ApplyLayout(IStaffLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(scoreDocument);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
