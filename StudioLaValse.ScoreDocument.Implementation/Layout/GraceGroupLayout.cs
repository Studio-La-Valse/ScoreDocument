using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class GraceGroupLayout
    {
        public abstract ValueTemplateProperty<bool> _OccupySpace { get; }
        public abstract ValueTemplateProperty<double> _ChordSpacing { get; }
        public abstract ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }
        public abstract ValueTemplateProperty<double> _StemLength { get; }
        public abstract ValueTemplateProperty<double> _BeamAngle { get; }
        public abstract ValueTemplateProperty<double> _BeamThickness { get; }
        public abstract ValueTemplateProperty<double> _BeamSpacing { get; }
        public abstract ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public bool OccupySpace
        {
            get => _OccupySpace.Value;
            set => _OccupySpace.Value = value;
        }
        public double ChordSpacing
        {
            get => _ChordSpacing.Value;
            set => _ChordSpacing.Value = value;
        }
        [AllowNull]
        public RythmicDuration ChordDuration
        {
            get => _ChordDuration.Value;
            set => _ChordDuration.Value = value;
        }
        public double Scale
        {
            get => _Scale.Value;
        }
        public double StemLength
        {
            get => _StemLength.Value;
            set => _StemLength.Value = value;
        }
        public double BeamAngle
        {
            get => _BeamAngle.Value;
            set => _BeamAngle.Value = value;
        }
        public StemDirection StemDirection
        {
            get => _StemDirection.Value;
            set => _StemDirection.Value = value;
        }

        public double BeamThickness
        {
            get => _BeamThickness.Value;
        }
        public double BeamSpacing
        {
            get => _BeamSpacing.Value;
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
            _BeamThickness.Reset();
            _BeamSpacing.Reset();
            _StemDirection.Reset();
        }
    }

    public class AuthorGraceGroupLayout : GraceGroupLayout, IGraceGroupLayout, ILayout<GraceGroupLayoutMembers>
    {
        public override ValueTemplateProperty<bool> _OccupySpace { get; }
        public override ValueTemplateProperty<double> _ChordSpacing { get; }
        public override ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<double> _BeamThickness { get; }
        public override ValueTemplateProperty<double> _BeamSpacing { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public AuthorGraceGroupLayout(GraceGroupStyleTemplate graceGroupStyleTemplate, int voice)
        {
            _OccupySpace = new ValueTemplateProperty<bool>(() => graceGroupStyleTemplate.OccupySpace);
            _ChordSpacing = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.ChordSpaceRight);
            _ChordDuration = new ReferenceTemplateProperty<RythmicDuration>(() => new(graceGroupStyleTemplate.ChordDuration));
            _Scale = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.Scale);
            _StemLength = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamAngle);
            _BeamThickness = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamThickness);
            _BeamSpacing = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamSpacing);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => voice % 2 == 0 ? StemDirection.Up : StemDirection.Down);
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

    public class UserGraceGroupLayout : GraceGroupLayout, IGraceGroupLayout
    {
        private readonly Guid guid;


        public override ValueTemplateProperty<bool> _OccupySpace { get; }
        public override ValueTemplateProperty<double> _ChordSpacing { get; }
        public override ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<double> _BeamThickness { get; }
        public override ValueTemplateProperty<double> _BeamSpacing { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public UserGraceGroupLayout(AuthorGraceGroupLayout authorGraceGroupLayout, Guid guid)
        {
            this.guid = guid;

            _OccupySpace = new ValueTemplateProperty<bool>(() => authorGraceGroupLayout.OccupySpace);
            _ChordSpacing = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.ChordSpacing);
            _ChordDuration = new ReferenceTemplateProperty<RythmicDuration>(() => authorGraceGroupLayout.ChordDuration);
            _Scale = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.Scale);
            _StemLength = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamAngle);
            _BeamThickness = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamThickness);
            _BeamSpacing = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamSpacing);
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
