using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class ChordProxy : IChord
    {
        private readonly Chord source;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;


        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        public IChordLayout Layout => layoutSelector.ChordLayout(source);

        public TemplateProperty<double> SpaceRight => Layout.SpaceRight.WithRerender(notifyEntityChanged, source.HostMeasure, commandManager);

        public ReadonlyTemplateProperty<double> StemLineThickness => Layout.StemLineThickness;

        public IRestLayout RestLayout => layoutSelector.RestLayout(source);

        public ReadonlyTemplateProperty<double> Scale => RestLayout.Scale;

        public TemplateProperty<ColorARGB> Color => RestLayout.Color.WithRerender(notifyEntityChanged, source, commandManager);

        public TemplateProperty<int> StaffIndex => RestLayout.StaffIndex.WithRerender(notifyEntityChanged, source.HostMeasure, commandManager);

        public TemplateProperty<int> Line => RestLayout.Line.WithRerender(notifyEntityChanged, source.HostMeasure, commandManager);

        public ChordProxy(Chord source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            this.source = source;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }



        public void Add(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<Chord, ChordMemento>(source, s => s.Add(pitches)).ThenInvalidate(notifyEntityChanged, source.HostMeasure);
            transaction.Enqueue(command);
        }

        public void Set(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<Chord, ChordMemento>(source, s => s.Set(pitches)).ThenInvalidate(notifyEntityChanged, source.HostMeasure);
            transaction.Enqueue(command);
        }

        public void Grace(RythmicDuration rythmicDuration, params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<Chord, ChordMemento>(source, s => s.ApplyGrace(rythmicDuration, pitches)).ThenInvalidate(notifyEntityChanged, source.HostMeasure);
            transaction.Enqueue(command);
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<Chord, ChordMemento>(source, s => s.Clear()).ThenInvalidate(notifyEntityChanged, source.HostMeasure);
            transaction.Enqueue(command);
        }

        public IEnumerable<INote> ReadNotes()
        {
            return source.EnumerateNotesCore().Select(n => n.Proxy(commandManager, notifyEntityChanged, layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadNotes();
        }

        public IGraceGroup? ReadGraceGroup()
        {
            return source.GraceGroup?.Proxy(commandManager, notifyEntityChanged, layoutSelector);
        }

        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
        {
            return Layout.ReadBeamTypes();
        }

        public BeamType? ReadBeamType(PowerOfTwo i)
        {
            return Layout.ReadBeamType(i);
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
