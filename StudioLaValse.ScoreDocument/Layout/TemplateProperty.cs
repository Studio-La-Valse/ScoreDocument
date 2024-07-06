using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A property that has its value provided by a template.
    /// Value can be reset so that when the value is retrieved, the templated value is returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TemplateProperty<T>
    {
        /// <summary>
        /// A boolean value showing if the backing field has been set.
        /// </summary>
        public abstract bool FieldIsSet { get; }

        /// <summary>
        /// Get or set the value.
        /// </summary>
        public abstract T Value { get; set; }

        /// <summary>
        /// Reset the value to its unset value.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Cast a template property to a value.
        /// </summary>
        /// <param name="property"></param>
        public static implicit operator T(TemplateProperty<T> property)
        {
            return property.Value;
        }
    }
}