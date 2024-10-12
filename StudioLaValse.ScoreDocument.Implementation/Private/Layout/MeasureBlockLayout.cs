using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{

    internal abstract class MeasureBlockLayout : IMeasureBlockLayout
    {
        public abstract ValueTemplateProperty<double> _StemLength { get; }
        public abstract ValueTemplateProperty<double> _BeamAngle { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }
        public abstract ValueTemplateProperty<StemDirection> _StemDirection { get; }

        public TemplateProperty<StemDirection> StemDirection => _StemDirection;
        public TemplateProperty<double> StemLength => _StemLength;
        public TemplateProperty<double> BeamAngle => _BeamAngle;


        public ReadonlyTemplateProperty<double> BeamThickness { get; }
        public ReadonlyTemplateProperty<double> BeamSpacing { get; }
        public TemplateProperty<double> Scale { get; }


        protected MeasureBlockLayout(MeasureBlockStyleTemplate measureBlockStyleTemplate, UserInstrumentRibbonLayout instrumentRibbonLayout)
        {
            BeamThickness = new ReadonlyTemplatePropertyFromFunc<double>(() => measureBlockStyleTemplate.BeamThickness);
            BeamSpacing = new ReadonlyTemplatePropertyFromFunc<double>(() => measureBlockStyleTemplate.BeamSpacing);

            double defaultScaleGetter() => _Scale.Value;
            double parentScaleGetter() => instrumentRibbonLayout.Scale.Value;
            double scaleAccumulator(double first, double second) => first * second;
            Scale = new AccumulativeValueTemplateProperty<double>(defaultScaleGetter, parentScaleGetter, scaleAccumulator);
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
            _StemDirection.Field = memento.StemDirection?.ConvertStemDirection();
        }
        public void ApplyMemento(MeasureBlockLayoutModel? memento)
        {
            ApplyMemento(memento as MeasureBlockLayoutMembers);
        }
    }

    internal class AuthorMeasureBlockLayout : MeasureBlockLayout
    {
        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }
        public override ValueTemplateProperty<double> _Scale { get; }

        public AuthorMeasureBlockLayout(MeasureBlockStyleTemplate styleTemplate, int voice, UserInstrumentRibbonLayout instrumentRibbonLayout) : base(styleTemplate, instrumentRibbonLayout)
        {
            _StemLength = new ValueTemplateProperty<double>(() => styleTemplate.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => styleTemplate.BeamAngle);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => voice % 2 == 0 ? StudioLaValse.ScoreDocument.Layout.StemDirection.Up : StudioLaValse.ScoreDocument.Layout.StemDirection.Down);
            _Scale = new ValueTemplateProperty<double>(() => styleTemplate.Scale);
        }

        public MeasureBlockLayoutMembers GetMemento()
        {
            return new MeasureBlockLayoutMembers()
            {
                StemLength = _StemLength.Field,
                BeamAngle = _BeamAngle.Field,
                StemDirection = _StemDirection.Field?.ConvertStemDirection(),
                Scale = _Scale.Field,
            };
        }
    }

    internal class UserMeasureBlockLayout : MeasureBlockLayout
    {
        public Guid Id { get; }

        public override ValueTemplateProperty<double> _StemLength { get; }
        public override ValueTemplateProperty<double> _BeamAngle { get; }
        public override ValueTemplateProperty<StemDirection> _StemDirection { get; }
        public override ValueTemplateProperty<double> _Scale { get; }

        public UserMeasureBlockLayout(Guid id, AuthorMeasureBlockLayout blockLayout, MeasureBlockStyleTemplate styleTemplate, UserInstrumentRibbonLayout instrumentRibbonLayout) : base(styleTemplate, instrumentRibbonLayout)
        {
            Id = id;

            _StemLength = new ValueTemplateProperty<double>(() => blockLayout.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => blockLayout.BeamAngle);
            _StemDirection = new ValueTemplateProperty<StemDirection>(() => blockLayout.StemDirection);
            _Scale = new ValueTemplateProperty<double>(() => blockLayout._Scale);
        }
    }
}
