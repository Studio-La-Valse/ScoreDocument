using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class MeasureBlockLayout : IMeasureBlockLayout
    {
        public abstract ValueTemplateProperty<double> _StemLength { get; }
        public abstract ValueTemplateProperty<double> _BeamAngle { get; }
        public abstract ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public TemplateProperty<StemDirection> StemDirection => _StemDirection;

        public TemplateProperty<double> StemLength => _StemLength;

        public TemplateProperty<double> BeamAngle => _BeamAngle;

        public ReadonlyTemplateProperty<double> BeamThickness { get; }

        public ReadonlyTemplateProperty<double> BeamSpacing { get; }


        protected MeasureBlockLayout(MeasureBlockStyleTemplate measureBlockStyleTemplate)
        {
            BeamThickness = new ReadonlyTemplatePropertyFromFunc<double>(() => measureBlockStyleTemplate.BeamThickness);
            BeamSpacing = new ReadonlyTemplatePropertyFromFunc<double>(() => measureBlockStyleTemplate.BeamSpacing);
        }


        public void Restore()
        {
            _StemLength.Reset();
            _BeamAngle.Reset();
            _StemDirection.Reset();
        }

        public void ApplyMemento(MeasureBlockLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _StemLength.Field = memento.StemLength;
            _BeamAngle.Field = memento.BeamAngle;
            _StemDirection.Field = memento.StemDirection;
        }
        public void ApplyMemento(MeasureBlockLayoutModel? memento)
        {
            ApplyMemento(memento as MeasureBlockLayoutMembers);
        }
    }

    public class AuthorMeasureBlockLayout : MeasureBlockLayout, ILayout<MeasureBlockLayoutMembers>
    {
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }


        public AuthorMeasureBlockLayout(MeasureBlockStyleTemplate styleTemplate, int voice) : base(styleTemplate)
        {
            _StemLength = new ValueTemplateProperty<double>(() => styleTemplate.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => styleTemplate.BeamAngle);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => voice % 2 == 0 ? ScoreDocument.Layout.StemDirection.Up : ScoreDocument.Layout.StemDirection.Down);
        }

        public MeasureBlockLayoutMembers GetMemento()
        {
            return new MeasureBlockLayoutMembers()
            {
                StemLength = _StemLength.Field,
                BeamAngle = _BeamAngle.Field,
                StemDirection = _StemDirection.Field,
            };
        }
    }

    public class UserMeasureBlockLayout : MeasureBlockLayout, IMeasureBlockLayout, ILayout<MeasureBlockLayoutModel>
    {
        public Guid Id { get; }

        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }


        public UserMeasureBlockLayout(Guid id, AuthorMeasureBlockLayout blockLayout, MeasureBlockStyleTemplate styleTemplate) : base(styleTemplate)
        {
            Id = id;

            _StemLength = new ValueTemplateProperty<double>(() => blockLayout.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => blockLayout.BeamAngle);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => blockLayout.StemDirection);
        }

        public MeasureBlockLayoutModel GetMemento()
        {
            return new MeasureBlockLayoutModel()
            {
                Id = Id,
                StemLength = _StemLength.Field,
                BeamAngle = _BeamAngle.Field,
                StemDirection = _StemDirection.Field,
            };
        }
    }
}
