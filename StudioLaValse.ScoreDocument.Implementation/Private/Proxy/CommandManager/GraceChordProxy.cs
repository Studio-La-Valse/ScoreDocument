using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class GraceChordProxy : IGraceChord
    {
        private readonly GraceChord graceChord;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;


        public int Id => graceChord.Id;

        public int IndexInGroup => graceChord.IndexInGroup;

        public ReadonlyTemplateProperty<double> SpaceRight => Layout.SpaceRight;

        public IGraceChordLayout Layout => layoutSelector.GraceChordLayout(graceChord);


        public GraceChordProxy(GraceChord graceChord, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            this.graceChord = graceChord;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }


        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadNotes();
        }

        public BeamType? ReadBeamType(PowerOfTwo i)
        {
            graceChord.BeamTypes.TryGetValue(i, out var type);
            return type;
        }

        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
        {
            return graceChord.BeamTypes;
        }

        public IEnumerable<IGraceNote> ReadNotes()
        {
            return graceChord.EnumerateNotes().Select(n => n.Proxy(commandManager, notifyEntityChanged, layoutSelector));
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<GraceChord, GraceChordMemento>(graceChord, s => s.Clear()).ThenInvalidate(notifyEntityChanged, graceChord.HostMeasure);
            transaction.Enqueue(command);
        }

        public void Add(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<GraceChord, GraceChordMemento>(graceChord, s => s.Add(pitches)).ThenInvalidate(notifyEntityChanged, graceChord.HostMeasure);
            transaction.Enqueue(command);
        }

        public void Set(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<GraceChord, GraceChordMemento>(graceChord, s => s.Set(pitches)).ThenInvalidate(notifyEntityChanged, graceChord.HostMeasure);
            transaction.Enqueue(command);
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
