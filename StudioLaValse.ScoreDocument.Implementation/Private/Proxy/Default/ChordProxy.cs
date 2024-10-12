global using StudioLaValse.CommandManager;
using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class ChordProxy : IChord
    {
        private readonly Chord source;
        private readonly ILayoutSelector layoutSelector;


        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        public IChordLayout Layout => layoutSelector.ChordLayout(source);

        public TemplateProperty<double> SpaceRight => Layout.SpaceRight;

        public ReadonlyTemplateProperty<double> StemLineThickness => Layout.StemLineThickness;

        public IRestLayout RestLayout => layoutSelector.RestLayout(source);

        public TemplateProperty<ColorARGB> Color => RestLayout.Color;

        public ReadonlyTemplateProperty<double> Scale => RestLayout.Scale;

        public TemplateProperty<int> StaffIndex => RestLayout.StaffIndex;

        public TemplateProperty<int> Line => RestLayout.StaffIndex;


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
