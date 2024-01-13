using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor;

namespace StudioLaValse.ScoreDocument.Drawable
{
    public static class ScoreBuilderExtensions
    {
        public static BaseScoreBuilder UseStateWatcher(this ScoreBuilder builder, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new ScoreBuilderWithStateWatcher(builder, notifyEntityChanged);
        }
    }
}
