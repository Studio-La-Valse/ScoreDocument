namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class ScoreBuilderWithCommandManager : BaseScoreBuilder
    {
        private readonly ICommandManager commandManager;

        public ScoreBuilderWithCommandManager(BaseScoreBuilder scoreBuilder, ICommandManager commandManager) : base(scoreBuilder)
        {
            this.commandManager = commandManager;
        }

        protected override void Edit(Action<IScoreDocumentEditor> action, IScoreDocumentEditor scoreDocumentEditor)
        {
            action(scoreDocumentEditor.UseTransaction(commandManager));
        }

        public override IScoreDocumentReader Build()
        {
            return base.Build();
        }
    }
}
