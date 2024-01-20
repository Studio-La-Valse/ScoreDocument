using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor;

namespace StudioLaValse.ScoreDocument.Drawable
{
    /// <summary>
    /// Extensions to the scorebuilder.
    /// </summary>
    public static class ScoreBuilderExtensions
    {
        /// <summary>
        /// Attaches the state watcher to the score builder. 
        /// Whenever changes are made to elements in the score, a notification will be send to all subscribers of the <see cref="INotifyEntityChanged{TEntity}"/> 
        /// in order to rerender the appropriate element in the visual tree. After each <see cref="BaseScoreBuilder.Edit(Action{Editor.IScoreDocumentEditor})"/> action,
        /// the <see cref="INotifyEntityChanged{TEntity}"/> will call <see cref="INotifyEntityChanged{TEntity}.RenderChanges"/> so that elements are only rerendered after
        /// the edit is complete.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="notifyEntityChanged"></param>
        /// <returns>A new implementation of the <see cref="BaseScoreBuilder"/></returns>
        public static BaseScoreBuilder UseStateWatcher(this BaseScoreBuilder builder, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new ScoreBuilderWithStateWatcher(builder, notifyEntityChanged);
        }
    }
}
