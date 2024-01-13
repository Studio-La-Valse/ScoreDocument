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

        public void ApplyLayout(IStaffLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IStaffLayout? oldLayout = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    oldLayout = source.ReadLayout().Copy();
                    source.ApplyLayout(layout);
                },
                () =>
                {
                    if (oldLayout is null)
                    {
                        throw new Exception("Memento not recorded!");
                    }

                    source.ApplyLayout(oldLayout);
                }));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
