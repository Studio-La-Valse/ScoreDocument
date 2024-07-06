using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class ReadonlyTemplatePropertyFromValue<T> : ReadonlyTemplateProperty<T>
    {
        public override T Value { get; }

        public ReadonlyTemplatePropertyFromValue(T value)
        {
            Value = value;
        }

    }
}
