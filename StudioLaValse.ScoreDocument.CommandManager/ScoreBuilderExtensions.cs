using StudioLaValse.ScoreDocument.CommandManager.Private;

namespace StudioLaValse.ScoreDocument.CommandManager
{
    /// <summary>
    /// Extensions to the base score builder class.
    /// </summary>
    public static class ScoreBuilderExtensions
    {
        /// <summary>
        /// Attach a command manager to the score builder, so that all actions on the score builder may be undone.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="commandManager"></param>
        /// <returns>A new implementation of the <see cref="BaseScoreBuilder"/>. Note that this is a new instance, so operations on the old instance will not propagate to the new.</returns>
        public static BaseScoreBuilder UserCommandManager(this BaseScoreBuilder score, ICommandManager commandManager)
        {
            return new ScoreBuilderWithCommandManager(score, commandManager);
        }
    }
}
