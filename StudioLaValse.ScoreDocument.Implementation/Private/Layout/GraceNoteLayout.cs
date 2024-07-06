using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class GraceNoteLayout : IGraceNoteLayout
    {
        private readonly IGraceGroupLayout graceGroupLayout;

        public abstract ValueTemplateProperty<int> _StaffIndex { get; }
        public abstract ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }


        public TemplateProperty<AccidentalDisplay> ForceAccidental => _ForceAccidental;
        public ReadonlyTemplateProperty<double> Scale => new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupLayout.Scale);
        public TemplateProperty<int> StaffIndex => _StaffIndex;
        public ReadonlyTemplateProperty<double> XOffset => new ReadonlyTemplatePropertyFromFunc<double>(() => 0);


        public GraceNoteLayout(IGraceGroupLayout graceGroupLayout)
        {
            this.graceGroupLayout = graceGroupLayout;
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
        }
        public void ApplyMemento(GraceNoteLayoutModel? memento)
        {
            ApplyMemento(memento as GraceNoteLayoutMembers);
        }

        public void Restore()
        {
            _StaffIndex.Reset();
            _ForceAccidental.Reset();
        }

        public void ResetAccidental()
        {
            _ForceAccidental.Reset();
        }

        public void ResetStaffIndex()
        {
            _StaffIndex.Reset();
        }
    }

    internal class AuthorGraceNoteLayout : GraceNoteLayout
    {
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }


        public AuthorGraceNoteLayout(IGraceGroupLayout graceGroupLayout) : base(graceGroupLayout)
        {
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => AccidentalDisplay.Default);
        }
    }

    internal class UserGraceNoteLayout : GraceNoteLayout
    {
        private readonly Guid guid;

        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public Guid Guid => guid;

        public UserGraceNoteLayout(Guid guid, IGraceGroupLayout graceGroupLayout, AuthorGraceNoteLayout authorGraceNoteLayout) : base(graceGroupLayout)
        {
            this.guid = guid;

            _StaffIndex = new ValueTemplateProperty<int>(() => authorGraceNoteLayout.StaffIndex);
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => authorGraceNoteLayout.ForceAccidental);
        }
    }
}
