using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class ScoreBuilderWithStateWatcher : BaseScoreBuilder
    {
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public ScoreBuilderWithStateWatcher(BaseScoreBuilder baseScoreBuilder, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged) : base(baseScoreBuilder)
        {
            this.notifyEntityChanged = notifyEntityChanged;
        }

        protected override void Edit(Action<IScoreDocumentEditor> action, IScoreDocumentEditor scoreDocumentEditor)
        {
            base.Edit(action, scoreDocumentEditor.UseStateWatcher(notifyEntityChanged));
            notifyEntityChanged.RenderChanges();
        }
    }
}
