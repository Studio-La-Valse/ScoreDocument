using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class ScoreMeasureLayout
    {
        public abstract ValueTemplateProperty<KeySignature> _KeySignature { get; }
        public abstract ValueTemplateProperty<double> _PaddingLeft { get; }
        public abstract ValueTemplateProperty<double> _PaddingRight { get; }
        public abstract NullableTemplateProperty<double> _PaddingBottom { get; }

        public KeySignature KeySignature
        {
            get
            {
                return _KeySignature.Value;
            }
            set
            {
                _KeySignature.Value = value;
            }
        }
        public double PaddingLeft
        {
            get
            {
                return _PaddingLeft.Value;
            }
            set
            {
                _PaddingLeft.Value = value;
            }
        }
        public double PaddingRight
        {
            get
            {
                return _PaddingRight.Value;
            }
            set
            {
                _PaddingRight.Value = value;
            }
        }
        public double? PaddingBottom
        {
            get
            {
                return _PaddingBottom.Value;
            }
            set
            {
                _PaddingBottom.Value = value;
            }
        }


        public void Restore()
        {
            _PaddingLeft.Reset();
            _PaddingRight.Reset();
            _KeySignature.Reset();
            _PaddingBottom.Reset();
        }

        public void ApplyMemento(ScoreMeasureLayoutModel? memento)
        {
            ApplyMemento(memento as ScoreMeasureLayoutMembers);
        }
        public void ApplyMemento(ScoreMeasureLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _KeySignature.Field = memento.KeySignature?.Convert();
            _PaddingBottom.Field = memento.PaddingBottom;
            _PaddingLeft.Field = memento.PaddingLeft;
            _PaddingRight.Field = memento.PaddingRight;
        }
    }

    public class AuthorScoreMeasureLayout : ScoreMeasureLayout, IScoreMeasureLayout, ILayout<ScoreMeasureLayoutMembers>
    {
        public override ValueTemplateProperty<KeySignature> _KeySignature { get; }
        public override ValueTemplateProperty<double> _PaddingLeft { get; }
        public override ValueTemplateProperty<double> _PaddingRight { get; }
        public override NullableTemplateProperty<double> _PaddingBottom { get; }

        internal AuthorScoreMeasureLayout(ScoreMeasureStyleTemplate scoreMeasureStyleTemplate)
        {
            _KeySignature = new ValueTemplateProperty<KeySignature>(() => new KeySignature(new Step(0, 0), MajorOrMinor.Major));
            _PaddingLeft = new ValueTemplateProperty<double>(() => scoreMeasureStyleTemplate.PaddingLeft);
            _PaddingRight = new ValueTemplateProperty<double>(() => scoreMeasureStyleTemplate.PaddingRight);
            _PaddingBottom = new NullableTemplateProperty<double>(() => null);
        }

        public ScoreMeasureLayoutMembers GetMemento()
        {
            return new ScoreMeasureLayoutMembers()
            {
                KeySignature = _KeySignature.Field?.Convert(),
                PaddingBottom = _PaddingBottom.Field,
                PaddingLeft = _PaddingLeft.Field,
                PaddingRight = _PaddingRight.Field,
            };
        }
    }

    public class UserScoreMeasureLayout : ScoreMeasureLayout, IScoreMeasureLayout, ILayout<ScoreMeasureLayoutModel>
    {
        private readonly Guid id;

        public override ValueTemplateProperty<KeySignature> _KeySignature { get; }
        public override ValueTemplateProperty<double> _PaddingLeft { get; }
        public override ValueTemplateProperty<double> _PaddingRight { get; }
        public override NullableTemplateProperty<double> _PaddingBottom { get; }


        public Guid Id => id;



        public UserScoreMeasureLayout(Guid id, AuthorScoreMeasureLayout primaryScoreMeasureLayout)
        {
            this.id = id;

            _KeySignature = new ValueTemplateProperty<KeySignature>(() => primaryScoreMeasureLayout.KeySignature);
            _PaddingLeft = new ValueTemplateProperty<double>(() => primaryScoreMeasureLayout.PaddingLeft);
            _PaddingRight = new ValueTemplateProperty<double>(() => primaryScoreMeasureLayout.PaddingRight);
            _PaddingBottom = new NullableTemplateProperty<double>(() => primaryScoreMeasureLayout.PaddingBottom);
        }

        public ScoreMeasureLayoutModel GetMemento()
        {
            return new ScoreMeasureLayoutModel()
            {
                Id = Id,
                KeySignature = _KeySignature.Field?.Convert(),
                PaddingBottom = _PaddingBottom.Field,
                PaddingLeft = _PaddingLeft.Field,
                PaddingRight = _PaddingRight.Field,
            };
        }
    }
}