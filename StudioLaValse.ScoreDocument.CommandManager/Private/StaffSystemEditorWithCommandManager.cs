namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class StaffSystemEditorWithCommandManager : IStaffSystemEditor
    {
        private readonly IStaffSystemEditor source;
        private readonly ICommandManager commandManager;

        public StaffSystemEditorWithCommandManager(IStaffSystemEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public int Id => source.Id;

        public void ApplyLayout(IStaffSystemLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IStaffSystemLayout? oldLayout = null;

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

        public IStaffGroupEditor EditStaffGroup(int indexInScore)
        {
            return source.EditStaffGroup(indexInScore).UseTransaction(commandManager);
        }

        public IEnumerable<IStaffGroupEditor> EditStaffGroups()
        {
            return source.EditStaffGroups().Select(e => e.UseTransaction(commandManager));
        }

        public IEnumerable<IScoreMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IEnumerable<IStaffGroup> EnumerateStaffGroups()
        {
            return source.EnumerateStaffGroups();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffSystemLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
