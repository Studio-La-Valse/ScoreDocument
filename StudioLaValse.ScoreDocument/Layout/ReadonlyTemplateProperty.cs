namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A property that has a readonly value that is provided by a template.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ReadonlyTemplateProperty<T>
    {
        /// <summary>
        /// Get the value.
        /// </summary>
        public abstract T Value { get; }

        /// <summary>
        /// Cast a template property to a value.
        /// </summary>
        /// <param name="property"></param>
        public static implicit operator T(ReadonlyTemplateProperty<T> property)
        {
            return property.Value;
        }
    }
}