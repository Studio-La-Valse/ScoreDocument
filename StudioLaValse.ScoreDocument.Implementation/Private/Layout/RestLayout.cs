using StudioLaValse.ScoreDocument.Models.Classes;
using StudioLaValse.ScoreDocument.Models.V1;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class RestLayout : IRestLayout
    {
        public abstract ReferenceTemplateProperty<ColorARGBClass> _Color { get; }
        public abstract ValueTemplateProperty<int> _StaffIndex { get; }
        public abstract ValueTemplateProperty<int> _Line { get; }


        public ReadonlyTemplateProperty<double> Scale { get; }


        public TemplateProperty<ColorARGBClass> Color => _Color;
        public TemplateProperty<int> StaffIndex => _StaffIndex;
        public TemplateProperty<int> Line => _Line;


        protected RestLayout(UserMeasureBlockLayout userMeasureBlockLayout)
        {
            Scale = new ReadonlyTemplatePropertyFromFunc<double>(() => userMeasureBlockLayout.Scale);
        }

        protected RestLayout(UserGraceGroupLayout userMeasureBlockLayout)
        {
            Scale = new ReadonlyTemplatePropertyFromFunc<double>(() => userMeasureBlockLayout.Scale);
        }


        public void Restore()
        {
            _Color.Reset();
            _StaffIndex.Reset();
            _Line.Reset();
        }

        public void ApplyMemento(RestLayoutMembers memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _Color.Field = memento.Color;
            _StaffIndex.Field = memento.StaffIndex;
            _Line.Field = memento.Line;
        }
    }

    internal class AuthorRestLayout : RestLayout
    {
        public override ReferenceTemplateProperty<ColorARGBClass> _Color { get; }

        public override ValueTemplateProperty<int> _StaffIndex { get; }

        public override ValueTemplateProperty<int> _Line { get; }

        public AuthorRestLayout(UserMeasureBlockLayout userMeasureBlockLayout, PageStyleTemplate pageStyleTemplate) : base(userMeasureBlockLayout)
        {
            _Color = new ReferenceTemplateProperty<ColorARGBClass>(() => pageStyleTemplate.ForegroundColor);
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _Line = new ValueTemplateProperty<int>(() => 4);
        }

        public AuthorRestLayout(UserGraceGroupLayout userMeasureBlockLayout, PageStyleTemplate pageStyleTemplate) : base(userMeasureBlockLayout)
        {
            _Color = new ReferenceTemplateProperty<ColorARGBClass>(() => pageStyleTemplate.ForegroundColor);
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _Line = new ValueTemplateProperty<int>(() => 4);
        }
    }

    internal class UserRestLayout : RestLayout
    {
        public Guid Id { get; }

        public override ReferenceTemplateProperty<ColorARGBClass> _Color { get; }

        public override ValueTemplateProperty<int> _StaffIndex { get; }

        public override ValueTemplateProperty<int> _Line { get; }

        public UserRestLayout(UserMeasureBlockLayout userMeasureBlockLayout, AuthorRestLayout source, Guid id) : base(userMeasureBlockLayout)
        {
            Id = id;

            _Color = new ReferenceTemplateProperty<ColorARGBClass>(() => source.Color);
            _StaffIndex = new ValueTemplateProperty<int>(() => source.StaffIndex);
            _Line = new ValueTemplateProperty<int>(() => source.Line);
        }

        public UserRestLayout(UserGraceGroupLayout userMeasureBlockLayout, AuthorRestLayout source, Guid id) : base(userMeasureBlockLayout)
        {
            Id = id;

            _Color = new ReferenceTemplateProperty<ColorARGBClass>(() => source.Color);
            _StaffIndex = new ValueTemplateProperty<int>(() => source.StaffIndex);
            _Line = new ValueTemplateProperty<int>(() => source.Line);
        }
    }
}