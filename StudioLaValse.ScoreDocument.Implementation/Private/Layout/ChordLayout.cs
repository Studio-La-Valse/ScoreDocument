using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class ChordLayout : IChordLayout
    {
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes;
        private readonly MeasureBlockStyleTemplate measureBlockStyleTemplate;

        public abstract ValueTemplateProperty<double> _SpaceRight { get; }

        public TemplateProperty<double> SpaceRight => _SpaceRight;
        public ReadonlyTemplateProperty<double> StemLineThickness => new ReadonlyTemplatePropertyFromFunc<double>(() => measureBlockStyleTemplate.StemThickness);

        protected ChordLayout(Dictionary<PowerOfTwo, BeamType> beamTypes, MeasureBlockStyleTemplate chordStyleTemplate)
        {
            this.beamTypes = beamTypes;
            this.measureBlockStyleTemplate = chordStyleTemplate;
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
            _SpaceRight.Reset();
        }

        public void ApplyMemento(ChordLayoutMembers memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _SpaceRight.Field = memento.SpaceRight;
        }
    }

    internal class AuthorChordLayout : ChordLayout
    {
        public override ValueTemplateProperty<double> _SpaceRight { get; }
        public Dictionary<PowerOfTwo, BeamType> BeamTypes { get; }

        public AuthorChordLayout(MeasureBlockStyleTemplate measureBlockStyleTemplate, ChordStyleTemplate chordStyleTemplate, Dictionary<PowerOfTwo, BeamType> beamTypes) : base(beamTypes, measureBlockStyleTemplate)
        {
            _SpaceRight = new ValueTemplateProperty<double>(() => chordStyleTemplate.SpaceRight);
            BeamTypes = beamTypes;
        }
    }

    internal class UserChordLayout : ChordLayout
    {
        public Guid Id { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }

        public UserChordLayout(AuthorChordLayout source, Guid id, MeasureBlockStyleTemplate measureBlockStyleTemplate) : base(source.BeamTypes, measureBlockStyleTemplate)
        {
            Id = id;

            _SpaceRight = new ValueTemplateProperty<double>(() => source.SpaceRight);
        }
    }
}