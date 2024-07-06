using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class ChordLayout : IChordLayout
    {
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes;

        public abstract ValueTemplateProperty<double> _XOffset { get; }
        public abstract ValueTemplateProperty<double> _SpaceRight { get; }

        public TemplateProperty<double> XOffset => _XOffset;
        public TemplateProperty<double> SpaceRight => _SpaceRight;


        protected ChordLayout(Dictionary<PowerOfTwo, BeamType> beamTypes)
        {
            this.beamTypes = beamTypes;
        }



        public BeamType? ReadBeamType(PowerOfTwo i)
        {
            return beamTypes.TryGetValue(i, out var value) ? value : null;
        }
        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
        {
            return beamTypes;
        }

        public void Restore()
        {
            _XOffset.Reset();
            _SpaceRight.Reset();
        }

        public void ApplyMemento(ChordLayoutMembers memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _XOffset.Field = memento.XOffset;
            _SpaceRight.Field = memento.SpaceRight;
        }


        public void ResetXOffset()
        {
            _XOffset.Reset();
        }

        public void ResetSpaceRight()
        {
            _SpaceRight.Reset();
        }
    }

    internal class AuthorChordLayout : ChordLayout
    {
        public override ValueTemplateProperty<double> _XOffset { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }
        public Dictionary<PowerOfTwo, BeamType> BeamTypes { get; }

        public AuthorChordLayout(ChordStyleTemplate chordStyleTemplate, Dictionary<PowerOfTwo, BeamType> beamTypes) : base(beamTypes)
        {
            _XOffset = new ValueTemplateProperty<double>(() => 0);
            _SpaceRight = new ValueTemplateProperty<double>(() => chordStyleTemplate.SpaceRight);
            BeamTypes = beamTypes;
        }
    }

    internal class UserChordLayout : ChordLayout
    {
        public Guid Id { get; }
        public override ValueTemplateProperty<double> _XOffset { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }

        public UserChordLayout(AuthorChordLayout source, Guid id) : base(source.BeamTypes)
        {
            Id = id;

            _XOffset = new ValueTemplateProperty<double>(() => source.XOffset);
            _SpaceRight = new ValueTemplateProperty<double>(() => source.SpaceRight);
        }
    }
}