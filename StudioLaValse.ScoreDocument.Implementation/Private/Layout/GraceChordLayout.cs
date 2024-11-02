using StudioLaValse.ScoreDocument.Models.V1;
using StudioLaValse.ScoreDocument.Models.Classes;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class GraceChordLayout : IGraceChordLayout
    {
        private readonly IGraceGroupLayout graceGroupLayout;
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes;
        private readonly MeasureBlockStyleTemplate chordStyleTemplate;

        public ReadonlyTemplateProperty<double> SpaceRight => new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupLayout.ChordSpacing.Value);

        public ReadonlyTemplateProperty<double> StemLineThickness => new ReadonlyTemplatePropertyFromFunc<double>(() => chordStyleTemplate.StemThickness * 0.5);



        public GraceChordLayout(IGraceGroupLayout graceGroupLayout, Dictionary<PowerOfTwo, BeamType> beamTypes, MeasureBlockStyleTemplate chordStyleTemplate)
        {
            this.graceGroupLayout = graceGroupLayout;
            this.beamTypes = beamTypes;
            this.chordStyleTemplate = chordStyleTemplate;
        }

        public BeamType? ReadBeamType(PowerOfTwo i)
        {
            return beamTypes.TryGetValue(i, out var value) ? value : null;
        }
        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
        {
            return beamTypes;
        }


        public void ApplyMemento(GraceChordLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }
        }
        public void ApplyMemento(GraceChordLayoutModel? memento)
        {
            ApplyMemento(memento as GraceChordLayoutMembers);
        }

        public void Restore()
        {

        }
    }

    internal class AuthorGraceChordLayout : GraceChordLayout
    {
        public Dictionary<PowerOfTwo, BeamType> BeamTypes { get; }

        public AuthorGraceChordLayout(IGraceGroupLayout graceGroupLayout, Dictionary<PowerOfTwo, BeamType> beamTypes, MeasureBlockStyleTemplate chordStyleTemplate) : base(graceGroupLayout, beamTypes, chordStyleTemplate)
        {
            BeamTypes = beamTypes;
        }
    }

    internal class UserGraceChordLayout : GraceChordLayout
    {
        private readonly Guid guid;
        public Guid Guid => guid;

        public UserGraceChordLayout(Guid guid, IGraceGroupLayout graceGroupLayout, Dictionary<PowerOfTwo, BeamType> beamTypes, MeasureBlockStyleTemplate chordStyleTemplate) : base(graceGroupLayout, beamTypes, chordStyleTemplate)
        {
            this.guid = guid;
        }
    }
}
