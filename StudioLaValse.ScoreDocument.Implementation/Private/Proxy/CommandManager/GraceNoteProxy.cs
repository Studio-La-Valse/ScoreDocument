using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class GraceNoteProxy : IGraceNote
    {
        private readonly GraceNote graceNote;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;


        public IGraceNoteLayout Layout => layoutSelector.GraceNoteLayout(graceNote);

        public int Id => graceNote.Id;

        public ReadonlyTemplateProperty<double> Scale => Layout.Scale;

        public ReadonlyTemplateProperty<double> XOffset => Layout.XOffset;

        public Pitch Pitch
        {
            get => graceNote.Pitch;
            set
            {
                var transaction = commandManager.ThrowIfNoTransactionOpen();
                var command = new MementoCommand<GraceNote, GraceNoteMemento>(graceNote, s => s.Pitch = value).ThenInvalidate(notifyEntityChanged, graceNote.InstrumentMeasure.HostMeasure.HostDocument);
                transaction.Enqueue(command);
            }
        }

        public TemplateProperty<AccidentalDisplay> ForceAccidental => Layout.ForceAccidental.WithRerender(notifyEntityChanged, graceNote.InstrumentMeasure, commandManager);

        public TemplateProperty<int> StaffIndex => Layout.StaffIndex.WithRerender(notifyEntityChanged, graceNote.InstrumentMeasure, commandManager);

        public TemplateProperty<ColorARGB> Color => Layout.Color;

        public GraceNoteProxy(GraceNote graceNote, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            this.graceNote = graceNote;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }


        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            yield break;
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }
    }
}
