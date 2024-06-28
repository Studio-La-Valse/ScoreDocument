namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    /// <summary> 
    /// Defines a template property that has a field and a value. When the value is assigned a value, this value will instead be assigned to the backing field. If the field has a value assigned, this value will be used. If not, the value will be retrieved by the provided value getter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReadonlyTemplatePropertyFromFunc<T> : ReadonlyTemplateProperty<T> 
    {
        private readonly Func<T> getDefaultValue;

        /// <summary>
        /// The value of the property.
        /// </summary>
        public override T Value
        {
            get
            {
                return getDefaultValue();
            }
        }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="getDefaultValue"></param>
        public ReadonlyTemplatePropertyFromFunc(Func<T> getDefaultValue)
        {
            this.getDefaultValue = getDefaultValue;
        }
    }
}