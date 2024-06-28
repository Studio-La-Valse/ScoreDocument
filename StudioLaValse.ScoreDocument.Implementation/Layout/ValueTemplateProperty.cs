﻿namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    /// <summary> 
    /// Defines a template property that has a field and a value. When the value is assigned a value, this value will instead be assigned to the backing field. If the field has a value assigned, this value will be used. If not, the value will be retrieved by the provided value getter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueTemplateProperty<T> : TemplateProperty<T> where T : struct
    {
        private readonly Func<T> getDefaultValue;

        /// <summary>
        /// The backing field for this property. If it is not set, the template value will be used.
        /// </summary>
        internal T? Field { get; set; } = null;

        /// <summary>
        /// The value of the property.
        /// </summary>
        public override T Value
        {
            get
            {
                if (Field.HasValue)
                {
                    return Field.Value;
                }

                return getDefaultValue();
            }
            set
            {
                Field = value;
            }
        }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="getDefaultValue"></param>
        public ValueTemplateProperty(Func<T> getDefaultValue)
        {
            this.getDefaultValue = getDefaultValue;
        }

        /// <summary>
        /// Reset the backing field value to its default value.
        /// </summary>
        public override void Reset()
        {
            Field = null;
        }
    }
}