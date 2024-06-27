using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class ChordLayout : IChordLayout
    {
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes;

        public abstract ValueTemplateProperty<double> _XOffset { get; }
        public abstract ValueTemplateProperty<double> _SpaceRight { get; }

        public double XOffset
        {
            get => _XOffset.Value;
            set => _XOffset.Value = value;
        }
        public double SpaceRight
        {
            get => _SpaceRight.Value;
            set => _SpaceRight.Value = value;
        }

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

        public void ApplyMemento(ChordLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _XOffset.Field = memento.XOffset;
            _SpaceRight.Field = memento.SpaceRight;
        }
        public void ApplyMemento(ChordLayoutModel? memento)
        {
            ApplyMemento(memento as ChordLayoutMembers);
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

    public class AuthorChordLayout : ChordLayout, ILayout<ChordLayoutMembers>
    {
        public override ValueTemplateProperty<double> _XOffset { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }

        public AuthorChordLayout(ChordStyleTemplate chordStyleTemplate, Dictionary<PowerOfTwo, BeamType> beamTypes) : base(beamTypes)
        {
            _XOffset = new ValueTemplateProperty<double>(() => 0);
            _SpaceRight = new ValueTemplateProperty<double>(() => chordStyleTemplate.SpaceRight);
        }


        public ChordLayoutMembers GetMemento()
        {
            return new ChordLayoutMembers()
            {
                XOffset = _XOffset.Field, 
                SpaceRight = _SpaceRight.Field
            };
        }
    }

    public class UserChordLayout : ChordLayout, ILayout<ChordLayoutModel>
    {
        public Guid Id { get; }
        public override ValueTemplateProperty<double> _XOffset { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }

        public UserChordLayout(AuthorChordLayout source, Guid id, Dictionary<PowerOfTwo, BeamType> beamTypes) : base(beamTypes)
        {
            Id = id;

            _XOffset = new ValueTemplateProperty<double>(() => source.XOffset);
            _SpaceRight = new ValueTemplateProperty<double>(() => source.SpaceRight);
        }

        public ChordLayoutModel GetMemento()
        {
            return new ChordLayoutModel()
            {
                Id = Id,
                XOffset = _XOffset.Field,
                SpaceRight = _SpaceRight.Field
            };
        }
    }
}