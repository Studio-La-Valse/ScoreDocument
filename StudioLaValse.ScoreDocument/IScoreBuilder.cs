using StudioLaValse.ScoreDocument.Models;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The main interface for the scorebuilder.
    /// </summary>
    public interface IScoreBuilder
    {
        /// <summary>
        /// Specify an action to apply to the score document editor. The action is delayed until <see cref="Build"/> is called.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IScoreBuilder Edit(Action<IScoreDocument> action);

        /// <summary>
        /// Apply the specified action only on the elements that have one of the specified element ids. The action is delayed until <see cref="Build"/> is called.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="elementIds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        IScoreBuilder Edit<TElement>(IEnumerable<int> elementIds, Action<TElement> action) where TElement : IUniqueScoreElement;

        /// <summary>
        /// Build the pending score builder actions.
        /// </summary>
        /// <returns></returns>
        IScoreBuilder Build();

        /// <summary>
        /// Create a serializable model from the internal score document.
        /// Includes the original author layout.
        /// </summary>
        /// <returns></returns>
        ScoreDocumentModel Freeze();

        /// <summary>
        /// Create a layout model from the internal score document.
        /// The model represents the user editable layout.
        /// Be aware that the resulting model may differ from the presented layout in the score document,
        /// because an internal layout selector may select the original author layout in the <see cref="IScoreDocument"/> interface.
        /// To get the original author layout, see <see cref="Freeze"/>.
        /// </summary>
        /// <returns></returns>
        ScoreDocumentLayoutDictionary FreezeLayout();
    }
}
