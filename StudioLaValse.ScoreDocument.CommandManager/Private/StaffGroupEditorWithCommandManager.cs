namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class StaffGroupEditorWithCommandManager : IStaffGroupEditor
    {
        private readonly IStaffGroupEditor source;
        private readonly ICommandManager commandManager;

        public StaffGroupEditorWithCommandManager(IStaffGroupEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public int IndexInScore => source.IndexInScore;

        public void ApplyLayout(IStaffGroupLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IStaffGroupLayout? oldLayout = null;

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

        public IStaffEditor EditStaff(int staffIndex)
        {
            return source.EditStaff(staffIndex).UseTransaction(commandManager);
        }

        public IEnumerable<IStaffEditor> EditStaves()
        {
            return source.EditStaves().Select(e => e.UseTransaction(commandManager));
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IEnumerable<IStaff> EnumerateStaves()
        {
            return source.EnumerateStaves();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffGroupLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
