using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class ChordLayout
    {
        public abstract ValueTemplateProperty<double> _XOffset { get; }
        public abstract ValueTemplateProperty<double> _SpaceRight { get; }

        public double XOffset
        {
            get => _XOffset.Value;
            set => _XOffset.Value = value;
        }
        public double SpaceRight
        {
            get => _SpaceRight.Value;
            set => _SpaceRight.Value = value;
        }

        public void Restore()
        {
            _XOffset.Reset();
            _SpaceRight.Reset();
        }

        public void ApplyMemento(ChordLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _XOffset.Field = memento.XOffset;
            _SpaceRight.Field = memento.SpaceRight;
        }
        public void ApplyMemento(ChordLayoutModel? memento)
        {
            ApplyMemento(memento as ChordLayoutMembers);
        }
    }

    public class AuthorChordLayout : ChordLayout, IChordLayout, ILayout<ChordLayoutMembers>
    {
        public override ValueTemplateProperty<double> _XOffset { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }

        public AuthorChordLayout(ChordStyleTemplate chordStyleTemplate)
        {
            _XOffset = new ValueTemplateProperty<double>(() => 0);
            _SpaceRight = new ValueTemplateProperty<double>(() => chordStyleTemplate.SpaceRight);
        }

        public ChordLayoutMembers GetMemento()
        {
            return new ChordLayoutMembers()
            {
                XOffset = _XOffset.Field, 
                SpaceRight = _SpaceRight.Field
            };
        }
    }

    public class UserChordLayout : ChordLayout, IChordLayout, ILayout<ChordLayoutModel>
    {
        public Guid Id { get; }
        public override ValueTemplateProperty<double> _XOffset { get; }
        public override ValueTemplateProperty<double> _SpaceRight { get; }

        public UserChordLayout(AuthorChordLayout source, Guid id)
        {
            Id = id;

            _XOffset = new ValueTemplateProperty<double>(() => source.XOffset);
            _SpaceRight = new ValueTemplateProperty<double>(() => source.SpaceRight);
        }

        public ChordLayoutModel GetMemento()
        {
            return new ChordLayoutModel()
            {
                Id = Id,
                XOffset = _XOffset.Field,
                SpaceRight = _SpaceRight.Field
            };
        }
    }
}