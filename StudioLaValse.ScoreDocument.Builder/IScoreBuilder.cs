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
        /// Specify an action to apply to the score document editor. The action is delayed until <see cref="Build"/> is called.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IScoreBuilder Edit(Action<IScoreDocumentEditor, IScoreLayoutDictionary> action);
        /// <summary>
        /// Build the pending score builder actions.
        /// </summary>
        /// <returns></returns>
        IScoreBuilder Build();
    }
}
