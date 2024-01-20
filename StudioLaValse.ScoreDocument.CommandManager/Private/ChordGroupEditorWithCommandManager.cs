namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class ChordGroupEditorWithCommandManager : IMeasureBlockEditor
    {
        private readonly IMeasureBlockEditor source;
        private readonly ICommandManager commandManager;

        public ChordGroupEditorWithCommandManager(IMeasureBlockEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public bool Grace => source.Grace;

        public Duration Duration => source.Duration;

        public int Id => source.Id;

        public void AppendChord(RythmicDuration rythmicDuration)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            MeasureBlockMemento? memento = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.AppendChord(rythmicDuration);
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

        public void Splice(int index)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            MeasureBlockMemento? memento = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Splice(index);
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

        public void ApplyLayout(INoteGroupLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            INoteGroupLayout? memento = null;

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

        public IEnumerable<IChordEditor> EditChords()
        {
            return source.EditChords().Select(e => e.UseTransaction(commandManager));
        }

        public IEnumerable<IChord> EnumerateChords()
        {
            return source.EnumerateChords();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public INoteGroupLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public void Rebeam()
        {
            source.Rebeam();
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            MeasureBlockMemento? memento = null;

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
