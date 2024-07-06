using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class TemplatePropertyFromValue<T> : TemplateProperty<T>
    {
        private readonly T value;

        public override T Value
        {
            get => value;
            set => throw new NotImplementedException();
        }

        public override bool FieldIsSet => false;

        public TemplatePropertyFromValue(T value)
        {
            this.value = value;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
