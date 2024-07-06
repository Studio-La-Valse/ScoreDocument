global using StudioLaValse.CommandManager;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class ChordProxy : IChord
    {
        private readonly Chord source;
        private readonly ILayoutSelector layoutSelector;

        public IChordLayout Layout => layoutSelector.ChordLayout(source);

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public Guid Guid => source.Guid;

        public int Id => source.Id;

        public TemplateProperty<double> XOffset => Layout.XOffset;

        public TemplateProperty<double> SpaceRight => Layout.SpaceRight;


        public ChordProxy(Chord source, ILayoutSelector layoutSelector)
        {
            this.source = source;
            this.layoutSelector = layoutSelector;
        }



        public void Add(params Pitch[] pitches)
        {
            source.Add(pitches);
        }

        public void Set(params Pitch[] pitches)
        {
            source.Set(pitches);
        }
        public void Grace(RythmicDuration rythmicDuration, params Pitch[] pitches)
        {
            source.ApplyGrace(rythmicDuration, pitches);
        }
        public void Clear()
        {
            source.Clear();
        }

        public IEnumerable<INote> ReadNotes()
        {
            return source.EnumerateNotesCore().Select(n => n.Proxy(layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadNotes();
        }

        public IGraceGroup? ReadGraceGroup()
        {
            return source.GraceGroup?.Proxy(layoutSelector);
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
