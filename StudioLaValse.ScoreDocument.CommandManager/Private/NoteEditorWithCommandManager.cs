namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class NoteEditorWithCommandManager : INoteEditor
    {
        private readonly INoteEditor source;
        private readonly ICommandManager commandManager;

        public NoteEditorWithCommandManager(INoteEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public Pitch Pitch
        {
            get
            {
                return this.source.Pitch;
            }
            set
            {
                var transaction = commandManager.ThrowIfNoTransactionOpen();

                Pitch? oldPitch = null;
                transaction.Enqueue(new SimpleCommand(
                    () =>
                    {
                        oldPitch = this.source.Pitch;
                        this.source.Pitch = value;
                    },
                    () =>
                    {
                        if (oldPitch is null)
                        {
                            throw new Exception("No memento recorded.");
                        }

                        this.source.Pitch = oldPitch;
                    }));
            }
        }
        public bool Grace => source.Grace;

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        Pitch INote.Pitch => ((INote)source).Pitch;

        public void ApplyLayout(IMeasureElementLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IMeasureElementLayout? oldLayout = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    oldLayout = this.source.ReadLayout();
                    this.source.ApplyLayout(layout);
                },
                () =>
                {
                    if (oldLayout is null)
                    {
                        throw new Exception("No memento recorded.");
                    }

                    this.source.ApplyLayout(oldLayout);
                }));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IMeasureElementLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
