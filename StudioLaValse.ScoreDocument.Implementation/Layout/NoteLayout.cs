using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.Models.Base;
using System;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class NoteLayout
    {
        public abstract ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }
        public abstract ValueTemplateProperty<int> _StaffIndex { get; }
        public abstract ValueTemplateProperty<double> _XOffset { get; }

        public AccidentalDisplay ForceAccidental
        {
            get => _ForceAccidental.Value;
            set => _ForceAccidental.Value = value;
        }
        public double Scale
        {
            get => _Scale.Value;
            set => _Scale.Value = value;
        }
        public int StaffIndex
        {
            get => _StaffIndex.Value;
            set => _StaffIndex.Value = value;
        }
        public double XOffset
        {
            get => _XOffset.Value;
            set => _XOffset.Value = value;
        }


        public void ApplyMemento(NoteLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _StaffIndex.Field = memento.StaffIndex;
            _XOffset.Field = memento.XOffset;
            _ForceAccidental.Field = memento.ForceAccidental.HasValue ? (AccidentalDisplay)memento.ForceAccidental : null;
            _Scale.Field = memento.Scale;
        }
        public void ApplyMemento(NoteLayoutModel? memento)
        {
            ApplyMemento(memento as NoteLayoutMembers);
        }

        public void Restore()
        {
            _StaffIndex.Reset();
            _XOffset.Reset();
            _ForceAccidental.Reset();
            _Scale.Reset();
        }
    }

    public class AuthorNoteLayout : NoteLayout, INoteLayout, ILayout<NoteLayoutMembers>
    {
        private readonly NoteStyleTemplate styleTemplate;

        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<double> _XOffset { get; }



        public AuthorNoteLayout(NoteStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;

            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => this.styleTemplate.AccidentalDisplay);
            _Scale = new ValueTemplateProperty<double>(() => this.styleTemplate.Scale);
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _XOffset = new ValueTemplateProperty<double>(() => 0);
        }


        public NoteLayoutMembers GetMemento()
        {
            return new NoteLayoutMembers()
            {
                ForceAccidental = _ForceAccidental.Field.HasValue ? (int)_ForceAccidental.Field : null,
                Scale = _Scale.Field,
                StaffIndex = _StaffIndex.Field,
                XOffset = _XOffset.Field
            };
        }
    }

    public class UserNoteLayout : NoteLayout, INoteLayout, ILayout<NoteLayoutModel>
    {
        public Guid Id { get; }
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<double> _XOffset { get; }


        public UserNoteLayout(Guid guid, AuthorNoteLayout primaryLayout)
        {
            Id = guid;

            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => primaryLayout.ForceAccidental);
            _Scale = new ValueTemplateProperty<double>(() => primaryLayout.Scale);
            _StaffIndex = new ValueTemplateProperty<int>(() => primaryLayout.StaffIndex);
            _XOffset = new ValueTemplateProperty<double>(() => primaryLayout.XOffset);
        }

        public NoteLayoutModel GetMemento()
        {
            return new NoteLayoutModel()
            {
                Id = Id,
                ForceAccidental = _ForceAccidental.Field.HasValue ? (int)_ForceAccidental.Field : null,
                Scale = _Scale.Field,
                StaffIndex = _StaffIndex.Field,
                XOffset = _XOffset.Field
            };
        }
    }
}