using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Builder
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
        IScoreBuilder Edit(Action<IScoreDocumentEditor> action);

        /// <summary>
        /// Apply the specified action only on the elements that have one of the specified element ids. The action is delayed until <see cref="Build"/> is called.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="elementIds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        IScoreBuilder Edit<TElement>(IEnumerable<int> elementIds, Action<TElement> action) where TElement : IScoreElementEditor;

        /// <summary>
        /// Build the pending score builder actions.
        /// </summary>
        /// <returns></returns>
        IScoreDocumentReader Build();
    }
}
