using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class GraceGroupLayout : IGraceGroupLayout
    {
        public abstract ValueTemplateProperty<bool> _OccupySpace { get; }
        public abstract ValueTemplateProperty<double> _ChordSpacing { get; }
        public abstract ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }
        public abstract ValueTemplateProperty<double> _StemLength { get; }
        public abstract ValueTemplateProperty<double> _BeamAngle { get; }
        public abstract ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public TemplateProperty<bool> OccupySpace => _OccupySpace;

        public TemplateProperty<double> ChordSpacing => _ChordSpacing;

        public TemplateProperty<RythmicDuration> ChordDuration => _ChordDuration;

        public TemplateProperty<double> Scale => _Scale;

        public TemplateProperty<StemDirection> StemDirection => _StemDirection;

        public TemplateProperty<double> StemLength => _StemLength;

        public TemplateProperty<double> BeamAngle => _BeamAngle;

        public TemplateProperty<RythmicDuration> BlockDuration => _ChordDuration;

        public ReadonlyTemplateProperty<double> BeamThickness { get; }

        public ReadonlyTemplateProperty<double> BeamSpacing { get; }


        protected GraceGroupLayout(GraceGroupStyleTemplate graceGroupStyleTemplate)
        {
            BeamThickness = new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupStyleTemplate.BeamThickness);
            BeamSpacing = new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupStyleTemplate.BeamSpacing);
        }


        public void ApplyMemento(GraceGroupLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _OccupySpace.Field = memento.OccupySpace;
            _ChordDuration.Field = memento.ChordDuration?.Convert();
            _ChordSpacing.Field = memento.ChordSpacing;
            _BeamAngle.Field = memento.BeamAngle;
            _StemLength.Field = memento.StemLength;
            _StemDirection.Field = memento.StemDirection;
        }
        public void ApplyMemento(GraceGroupLayoutModel? memento)
        {
            ApplyMemento(memento as GraceGroupLayoutMembers);
        }


        public void Restore()
        {
            _OccupySpace.Reset();
            _ChordSpacing.Reset();
            _ChordDuration.Reset();
            _Scale.Reset();
            _StemLength.Reset();
            _BeamAngle.Reset();
            _StemDirection.Reset();
        }
    }

    public class AuthorGraceGroupLayout : GraceGroupLayout, ILayout<GraceGroupLayoutMembers>
    {
        public override ValueTemplateProperty<bool> _OccupySpace { get; }
        public override ValueTemplateProperty<double> _ChordSpacing { get; }
        public override ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public AuthorGraceGroupLayout(GraceGroupStyleTemplate graceGroupStyleTemplate, int voice) : base(graceGroupStyleTemplate)
        {
            _OccupySpace = new ValueTemplateProperty<bool>(() => graceGroupStyleTemplate.OccupySpace);
            _ChordSpacing = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.ChordSpaceRight);
            _ChordDuration = new ReferenceTemplateProperty<RythmicDuration>(() => new(graceGroupStyleTemplate.ChordDuration));
            _Scale = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.Scale);
            _StemLength = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamAngle);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => voice % 2 == 0 ? ScoreDocument.Layout.StemDirection.Up : ScoreDocument.Layout.StemDirection.Down);
        }

        public GraceGroupLayoutMembers GetMemento()
        {
            return new GraceGroupLayoutMembers()
            {
                BeamAngle = _BeamAngle.Field,
                ChordDuration = _ChordDuration.Field?.Convert(),
                ChordSpacing = _ChordSpacing.Field,
                OccupySpace = _OccupySpace.Field,
                StemLength = _StemLength.Field,
                StemDirection = _StemDirection.Field
            };
        }
    }

    public class UserGraceGroupLayout : GraceGroupLayout
    {
        private readonly Guid guid;


        public override ValueTemplateProperty<bool> _OccupySpace { get; }
        public override ValueTemplateProperty<double> _ChordSpacing { get; }
        public override ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }


        public UserGraceGroupLayout(AuthorGraceGroupLayout authorGraceGroupLayout, Guid guid, GraceGroupStyleTemplate graceGroupStyleTemplate) : base(graceGroupStyleTemplate)
        {
            this.guid = guid;

            _OccupySpace = new ValueTemplateProperty<bool>(() => authorGraceGroupLayout.OccupySpace);
            _ChordSpacing = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.ChordSpacing);
            _ChordDuration = new ReferenceTemplateProperty<RythmicDuration>(() => authorGraceGroupLayout.ChordDuration);
            _Scale = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.Scale);
            _StemLength = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamAngle);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => authorGraceGroupLayout.StemDirection);
        }

        public GraceGroupLayoutModel GetMemento()
        {
            return new GraceGroupLayoutModel()
            {
                Id = guid,
                BeamAngle = _BeamAngle.Field,
                ChordDuration = _ChordDuration.Field?.Convert(),
                ChordSpacing = _ChordSpacing.Field,
                OccupySpace = _OccupySpace.Field,
                StemLength = _StemLength.Field,
                StemDirection = _StemDirection.Field,
            };
        }
    }
}
