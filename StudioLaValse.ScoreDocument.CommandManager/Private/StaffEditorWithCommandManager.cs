namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class StaffEditorWithCommandManager : IStaffEditor
    {
        private readonly IStaffEditor source;
        private readonly ICommandManager commandManager;

        public StaffEditorWithCommandManager(IStaffEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public int IndexInStaffGroup => source.IndexInStaffGroup;

        public int Id => source.Id;

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }
    }
}
