using StudioLaValse.ScoreDocument.CommandManager.Private;

namespace StudioLaValse.ScoreDocument.CommandManager
{
    public static class ScoreBuilderExtensions
    {
        public static BaseScoreBuilder UserCommandManager(this BaseScoreBuilder score, ICommandManager commandManager)
        {
            return new ScoreBuilderWithCommandManager(score, commandManager);
        }
    }
}
