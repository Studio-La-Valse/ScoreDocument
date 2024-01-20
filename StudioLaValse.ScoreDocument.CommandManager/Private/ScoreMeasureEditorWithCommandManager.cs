namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class ScoreMeasureEditorWithCommandManager : IScoreMeasureEditor
    {
        private readonly IScoreMeasureEditor source;
        private readonly ICommandManager commandManager;

        public ScoreMeasureEditorWithCommandManager(IScoreMeasureEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public int IndexInScore => source.IndexInScore;

        public TimeSignature TimeSignature => source.TimeSignature;

        public int Id => source.Id;

        public void ApplyLayout(IScoreMeasureLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IScoreMeasureLayout? oldLayout = null;

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

        public IInstrumentMeasureEditor EditMeasure(int ribbonIndex)
        {
            return source.EditMeasure(ribbonIndex).UseTransaction(commandManager);
        }

        public IEnumerable<IInstrumentMeasureEditor> EditMeasures()
        {
            return source.EditMeasures().Select(e => e.UseTransaction(commandManager));
        }

        public IStaffSystemEditor EditStaffSystemOrigin()
        {
            return source.EditStaffSystemOrigin().UseTransaction(commandManager);
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffSystem GetStaffSystemOrigin()
        {
            return source.GetStaffSystemOrigin();
        }

        public IScoreMeasureLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
