using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class NoteLayout : INoteLayout
    {
        public abstract ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }
        public abstract ValueTemplateProperty<int> _StaffIndex { get; }
        public abstract ValueTemplateProperty<double> _XOffset { get; }


        public TemplateProperty<AccidentalDisplay> ForceAccidental => _ForceAccidental;
        public TemplateProperty<double> Scale => _Scale;
        public TemplateProperty<int> StaffIndex => _StaffIndex;
        public TemplateProperty<double> XOffset => _XOffset;


        public void ApplyMemento(NoteLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _StaffIndex.Field = memento.StaffIndex;
            _XOffset.Field = memento.XOffset;
            _ForceAccidental.Field = memento.ForceAccidental?.ConvertAccidental();
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

    internal class AuthorNoteLayout : NoteLayout
    {
        public override ValueTemplateProperty<AccidentalDisplay> _ForceAccidental { get; }
        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<int> _StaffIndex { get; }
        public override ValueTemplateProperty<double> _XOffset { get; }



        public AuthorNoteLayout()
        {
            _ForceAccidental = new ValueTemplateProperty<AccidentalDisplay>(() => AccidentalDisplay.Default);
            _Scale = new ValueTemplateProperty<double>(() => 1);
            _StaffIndex = new ValueTemplateProperty<int>(() => 0);
            _XOffset = new ValueTemplateProperty<double>(() => 0);
        }
    }

    internal class UserNoteLayout : NoteLayout
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
    }
}