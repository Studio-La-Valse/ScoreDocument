using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class TemplatePropertyFromReadonlyTemplateProperty<T> : TemplateProperty<T>
    {
        private readonly ReadonlyTemplateProperty<T> readonlyTemplateProperty;

        public override T Value 
        {
            get => readonlyTemplateProperty.Value;
            set => throw new NotImplementedException(); 
        }

        public override bool FieldIsSet => false;

        public TemplatePropertyFromReadonlyTemplateProperty(ReadonlyTemplateProperty<T> readonlyTemplateProperty)
        {
            this.readonlyTemplateProperty = readonlyTemplateProperty;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
