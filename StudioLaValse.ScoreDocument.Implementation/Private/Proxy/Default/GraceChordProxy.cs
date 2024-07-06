using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class GraceChordProxy : IGraceChord
    {
        private readonly GraceChord graceChord;
        private readonly ILayoutSelector layoutSelector;


        public int Id => graceChord.Id;

        public int IndexInGroup => graceChord.IndexInGroup;

        public ReadonlyTemplateProperty<double> SpaceRight => Layout.SpaceRight;

        public IGraceChordLayout Layout => layoutSelector.GraceChordLayout(graceChord);


        public GraceChordProxy(GraceChord graceChord, ILayoutSelector layoutSelector)
        {
            this.graceChord = graceChord;
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
            return graceChord.EnumerateNotes().Select(n => n.Proxy(layoutSelector));
        }

        public void Clear()
        {
            graceChord.Clear();
        }

        public void Add(params Pitch[] pitches)
        {
            graceChord.Add(pitches);
        }

        public void Set(params Pitch[] pitches)
        {
            graceChord.Set(pitches);
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
