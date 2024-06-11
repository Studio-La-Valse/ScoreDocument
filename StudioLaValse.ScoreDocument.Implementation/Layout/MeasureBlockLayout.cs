using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class MeasureBlockLayout
    {
        public abstract ValueTemplateProperty<double> _StemLength { get; }
        public abstract ValueTemplateProperty<double> _BeamAngle { get; }
        public abstract ValueTemplateProperty<double> _BeamThickness { get; }
        public abstract ValueTemplateProperty<double> _BeamSpacing { get; }
        public abstract ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public double StemLength { get => _StemLength.Value; set => _StemLength.Value = value; }
        public double BeamAngle { get => _BeamAngle.Value; set => _BeamAngle.Value = value; }
        public StemDirection StemDirection { get => _StemDirection.Value; set => _StemDirection.Value = value; }

        public double BeamThickness { get => _BeamThickness.Value; }
        public double BeamSpacing { get => _BeamSpacing.Value; }
        public void Restore()
        {
            _StemLength.Reset();
            _BeamAngle.Reset();
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
        }
        public void ApplyMemento(MeasureBlockLayoutModel? memento)
        {
            ApplyMemento(memento as MeasureBlockLayoutMembers);
        }
    }

    public class AuthorMeasureBlockLayout : MeasureBlockLayout, IMeasureBlockLayout, ILayout<MeasureBlockLayoutMembers>
    {
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<double> _BeamThickness { get; }
        public override ValueTemplateProperty<double> _BeamSpacing { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }


        public AuthorMeasureBlockLayout(MeasureBlockStyleTemplate styleTemplate, int voice)
        {
            _StemLength = new ValueTemplateProperty<double>(() => styleTemplate.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => styleTemplate.BeamAngle);
            _BeamThickness = new ValueTemplateProperty<double>(() => styleTemplate.BeamThickness);
            _BeamSpacing = new ValueTemplateProperty<double>(() => styleTemplate.BeamSpacing);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => voice % 2 == 0 ? StemDirection.Up : StemDirection.Down);
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
        public override ValueTemplateProperty<double> _BeamThickness { get; }
        public override ValueTemplateProperty<double> _BeamSpacing { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }


        public UserMeasureBlockLayout(Guid id, AuthorMeasureBlockLayout blockLayout)
        {
            Id = id;

            _StemLength = new ValueTemplateProperty<double>(() => blockLayout.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => blockLayout.BeamAngle);
            _BeamThickness = new ValueTemplateProperty<double>(() => blockLayout.BeamThickness);
            _BeamSpacing = new ValueTemplateProperty<double>(() => blockLayout.BeamSpacing);
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
