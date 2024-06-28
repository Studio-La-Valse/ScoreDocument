using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class ReadonlyTemplatePropertyFromFunc<T> : ReadonlyTemplateProperty<T>
    {
        private readonly Func<T> value;

        public override T Value => value();

        public ReadonlyTemplatePropertyFromFunc(Func<T> value)
        {
            this.value = value;
        }
    }
}
