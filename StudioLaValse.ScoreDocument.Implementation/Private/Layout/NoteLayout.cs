using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class NoteLayout : INoteLayout
    {
        public abstract ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public abstract ValueTemplateProperty<int> _StaffIndex { get; }
        public abstract ValueTemplateProperty<ColorARGB> _Color { get; }


        public ReadonlyTemplateProperty<double> Scale { get; }



        public TemplateProperty<AccidentalDisplay> ForceAccidental => _ForceAccidental;
        public TemplateProperty<int> StaffIndex => _StaffIndex;
        public TemplateProperty<ColorARGB> Color => _Color;


        protected NoteLayout(UserMeasureBlockLayout userMeasureBlockLayout)
        {
            Scale = new ReadonlyTemplatePropertyFromFunc<double>(() => userMeasureBlockLayout.Scale);
        }


        public void ApplyMemento(NoteLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _StaffIndex.Field = memento.StaffIndex;
            _ForceAccidental.Field = memento.ForceAccidental?.ConvertAccidental();
            _Color.Field = memento.Color?.Convert();
        }
        public void ApplyMemento(NoteLayoutModel? memento)
        {
            ApplyMemento(memento as NoteLayoutMembers);
        }

        public void Restore()
        {
            _StaffIndex.Reset();
            _ForceAccidental.Reset();
            _Color.Reset();
        }
    }

    internal class AuthorNoteLayout : NoteLayout
    {
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<ColorARGB> _Color { get; }

        public AuthorNoteLayout(PageStyleTemplate pageStyleTemplate, UserMeasureBlockLayout userMeasureBlockLayout) : base(userMeasureBlockLayout)
        {
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => AccidentalDisplay.Default);
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _Color = new ValueTemplateProperty<ColorARGB>(() => pageStyleTemplate.ForegroundColor);
        }
    }

    internal class UserNoteLayout : NoteLayout
    {
        public Guid Id { get; }

        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<ColorARGB> _Color { get; }


        public UserNoteLayout(Guid guid, AuthorNoteLayout primaryLayout, UserMeasureBlockLayout userMeasureBlockLayout) : base(userMeasureBlockLayout)
        {
            Id = guid;

            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => primaryLayout.ForceAccidental);
            _StaffIndex = new ValueTemplateProperty<int>(() => primaryLayout._StaffIndex);
            _Color = new ValueTemplateProperty<ColorARGB>(() => primaryLayout.Color);
        }
    }
}