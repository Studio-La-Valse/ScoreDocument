using System.Diagnostics;

namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class ChordEditorWithCommandManager : IChordEditor
    {
        private readonly IChordEditor source;
        private readonly ICommandManager commandManager;

        public ChordEditorWithCommandManager(IChordEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public bool Grace => source.Grace;

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        public int IndexInBlock => source.IndexInBlock;

        public void Add(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            ChordMemento? chordMemento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    chordMemento = source.GetMemento();
                    source.Add(pitches);
                },
                () =>
                {
                    if (chordMemento is null)
                    {
                        throw new UnreachableException("Memento is null");
                    }

                    source.ApplyMemento(chordMemento);
                }));

        }

        public void Set(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            ChordMemento? chordMemento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    chordMemento = source.GetMemento();
                    source.Set(pitches);
                },
                () =>
                {
                    if (chordMemento is null)
                    {
                        throw new UnreachableException("Memento is null");
                    }

                    source.ApplyMemento(chordMemento);
                }));
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            ChordMemento? chordMemento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    chordMemento = source.GetMemento();
                    source.Clear();
                },
                () =>
                {
                    if (chordMemento is null)
                    {
                        throw new UnreachableException("Memento is null");
                    }

                    source.ApplyMemento(chordMemento);
                }));
        }

        public void ApplyLayout(IChordLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IChordLayout? oldLayout = null;

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
                        throw new UnreachableException("Memento is null");
                    }

                    source.ApplyLayout(oldLayout);
                }));
        }

        public IChordLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public IEnumerable<INoteEditor> EditNotes()
        {
            return source.EditNotes().Select(e => e.UseTransaction(commandManager));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IEnumerable<INote> EnumerateNotes()
        {
            return source.EnumerateNotes();
        }
    }
}
