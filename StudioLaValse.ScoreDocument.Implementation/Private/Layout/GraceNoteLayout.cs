using StudioLaValse.ScoreDocument.Models.Classes;
using StudioLaValse.ScoreDocument.Models.V1;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class GraceNoteLayout : IGraceNoteLayout
    {
        private readonly IGraceGroupLayout graceGroupLayout;

        public abstract ValueTemplateProperty<int> _StaffIndex { get; }
        public abstract ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public abstract ReferenceTemplateProperty<ColorARGBClass> _Color { get; }

        public ReadonlyTemplateProperty<double> Scale { get; }


        public TemplateProperty<AccidentalDisplay> ForceAccidental => _ForceAccidental;
        public TemplateProperty<int> StaffIndex => _StaffIndex;
        public TemplateProperty<ColorARGBClass> Color => _Color;


        public GraceNoteLayout(UserGraceGroupLayout graceGroupLayout)
        {
            this.graceGroupLayout = graceGroupLayout;

            Scale = new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupLayout.Scale);
        }


        public void ApplyMemento(GraceNoteLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _StaffIndex.Field = memento.StaffIndex;
            _ForceAccidental.Field = (AccidentalDisplay?)memento.ForceAccidental;
            _Color.Field = memento.Color;
        }
        public void ApplyMemento(GraceNoteLayoutModel? memento)
        {
            ApplyMemento(memento as GraceNoteLayoutMembers);
        }

        public void Restore()
        {
            _StaffIndex.Reset();
            _ForceAccidental.Reset();
            _Color.Reset();
        }
    }

    internal class AuthorGraceNoteLayout : GraceNoteLayout
    {
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ReferenceTemplateProperty<ColorARGBClass> _Color { get; }

        public AuthorGraceNoteLayout(UserGraceGroupLayout graceGroupLayout, PageStyleTemplate pageStyleTemplate) : base(graceGroupLayout)
        {
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => AccidentalDisplay.Default);
            _Color = new ReferenceTemplateProperty<ColorARGBClass>(() => pageStyleTemplate.ForegroundColor);
        }
    }

    internal class UserGraceNoteLayout : GraceNoteLayout
    {
        private readonly Guid guid;
        public Guid Guid => guid;

        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ReferenceTemplateProperty<ColorARGBClass> _Color { get; }

        public UserGraceNoteLayout(Guid guid, UserGraceGroupLayout graceGroupLayout, AuthorGraceNoteLayout authorGraceNoteLayout) : base(graceGroupLayout)
        {
            this.guid = guid;

            _StaffIndex = new ValueTemplateProperty<int>(() => authorGraceNoteLayout.StaffIndex);
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => authorGraceNoteLayout.ForceAccidental);
            _Color = new ReferenceTemplateProperty<ColorARGBClass>(() => authorGraceNoteLayout.Color);
        }
    }
}
