using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Models.Base;
using System;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class GraceNoteLayout : INoteLayout
    {
        private readonly IGraceGroupLayout graceGroupLayout;
        private readonly NoteStyleTemplate noteStyleTemplate;

        protected abstract ValueTemplateProperty<int> _StaffIndex { get; }
        protected abstract ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }

        
        public double Scale => graceGroupLayout.Scale;
        public double XOffset => 0;


        public int StaffIndex
        {
            get => _StaffIndex.Value;
            set => _StaffIndex.Value = value;
        }
        public AccidentalDisplay ForceAccidental
        {
            get => _ForceAccidental.Value;
            set => _ForceAccidental.Value = value;
        }



        public GraceNoteLayout(IGraceGroupLayout graceGroupLayout, NoteStyleTemplate noteStyleTemplate )
        {
            this.graceGroupLayout = graceGroupLayout;
            this.noteStyleTemplate = noteStyleTemplate;
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
    }

    public class AuthorGraceNoteLayout : GraceNoteLayout, ILayout<GraceNoteLayoutMembers>
    {
        protected override ValueTemplateProperty<int> _StaffIndex { get; }
        protected override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }


        public AuthorGraceNoteLayout(IGraceGroupLayout graceGroupLayout, NoteStyleTemplate noteStyleTemplate) : base(graceGroupLayout, noteStyleTemplate)
        {
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => noteStyleTemplate.AccidentalDisplay); 
        }

        public GraceNoteLayoutMembers GetMemento()
        {
            return new GraceNoteLayoutMembers()
            {
                ForceAccidental = (int?)_ForceAccidental.Field,
                StaffIndex = _StaffIndex.Field,
            };
        }
    }

    public class UserGraceNoteLayout : GraceNoteLayout, ILayout<GraceNoteLayoutModel>
    {
        private readonly Guid guid;

        protected override ValueTemplateProperty<int> _StaffIndex { get; }
        protected override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }

        public UserGraceNoteLayout(Guid guid, IGraceGroupLayout graceGroupLayout, NoteStyleTemplate noteStyleTemplate, AuthorGraceNoteLayout authorGraceNoteLayout) : base(graceGroupLayout, noteStyleTemplate)
        {
            this.guid = guid;

            _StaffIndex = new ValueTemplateProperty<int>(() => authorGraceNoteLayout.StaffIndex);
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => authorGraceNoteLayout.ForceAccidental);
        }

        public GraceNoteLayoutModel GetMemento()
        {
            return new GraceNoteLayoutModel()
            {
                Id = guid,
                ForceAccidental = (int?)_ForceAccidental.Field,
                StaffIndex = _StaffIndex.Field,
            };
        }
    }
}
