namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class MeasureBlockChainWithCommandManager : IMeasureBlockChainEditor
    {
        private readonly IMeasureBlockChainEditor source;
        private readonly ICommandManager commandManager;

        public MeasureBlockChainWithCommandManager(IMeasureBlockChainEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public int Voice => source.Voice;

        public void Append(RythmicDuration duration, bool grace)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Append(duration, grace);
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

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
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

        public void Divide(params int[] steps)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Divide(steps);
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

        public void DivideEqual(int number)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.DivideEqual(number);
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

        public IEnumerable<IMeasureBlockEditor> EditBlocks()
        {
            return source.EditBlocks().Select(e => e.UseTransaction(commandManager));
        }

        public IEnumerable<IMeasureBlock> EnumerateBlocks()
        {
            return source.EnumerateBlocks();
        }

        public void Insert(Position position, RythmicDuration duration, bool grace)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Insert(position, duration, grace);
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

        public void Prepend(RythmicDuration duration, bool grace)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            RibbonMeasureVoiceMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Prepend(duration, grace);
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
