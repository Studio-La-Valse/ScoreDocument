namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class RibbonMeasureEditorWithCommandManager : IInstrumentMeasureEditor
    {
        private readonly IInstrumentMeasureEditor source;
        private readonly ICommandManager commandManager;

        public RibbonMeasureEditorWithCommandManager(IInstrumentMeasureEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public int MeasureIndex => source.MeasureIndex;

        public int RibbonIndex => source.RibbonIndex;

        public TimeSignature TimeSignature => source.TimeSignature;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public void AddVoice(int voice)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento(voice);
                    source.AddVoice(voice);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ClearVoice(voice);
                    source.ApplyMemento(memento);
                }));
        }

        public void AppendBlock(int voice, Duration duration, bool grace)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento(voice);
                    source.AppendBlock(voice, duration, grace);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ClearVoice(voice);
                    source.ApplyMemento(memento);
                }));
        }

        public void PrependBlock(int voice, Duration duration, bool grace)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento(voice);
                    source.PrependBlock(voice, duration, grace);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ClearVoice(voice);
                    source.ApplyMemento(memento);
                }));
        }

        public void ClearVoice(int voice)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento(voice);
                    source.ClearVoice(voice);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ClearVoice(voice);
                    source.ApplyMemento(memento);
                }));
        }

        public IEnumerable<IMeasureBlockEditor> EditBlocks(int voice)
        {
            return source.EditBlocks(voice).Select(e => e.UseTransaction(commandManager));
        }

        public IEnumerable<IMeasureBlock> EnumerateBlocks(int voice)
        {
            return source.EnumerateBlocks(voice);
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return source.EnumerateVoices();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public void ApplyLayout(IInstrumentMeasureLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IInstrumentMeasureLayout? memento = null;
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

        public IInstrumentMeasureLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            InstrumentMeasureMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Clear();
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ApplyMemento(memento);
                }));
        }
    }
}
