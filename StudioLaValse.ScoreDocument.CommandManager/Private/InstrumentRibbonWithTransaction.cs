namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class InstrumentRibbonWithTransaction : IInstrumentRibbonEditor
    {
        private readonly IInstrumentRibbonEditor source;
        private readonly ICommandManager commandManager;

        public InstrumentRibbonWithTransaction(IInstrumentRibbonEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public int IndexInScore => source.IndexInScore;

        public void ApplyLayout(IInstrumentRibbonLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IInstrumentRibbonLayout? memento = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.ReadLayout();
                    source.ApplyLayout(layout);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ApplyLayout(memento);
                }));
        }

        public IRibbonMeasureEditor EditMeasure(int measureIndex)
        {
            return source.EditMeasure(measureIndex).UseTransaction(commandManager);
        }

        public IEnumerable<IRibbonMeasureEditor> EditMeasures()
        {
            return source.EditMeasures().Select(e => e.UseTransaction(commandManager));
        }

        public IEnumerable<IRibbonMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IInstrumentRibbonLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
