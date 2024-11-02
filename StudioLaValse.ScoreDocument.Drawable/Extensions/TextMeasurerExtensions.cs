using StudioLaValse.ScoreDocument.Drawable.Private.TextMeasurer;

namespace StudioLaValse.ScoreDocument.Drawable.Extensions
{
    /// <summary>
    /// Text measurer extensions
    /// </summary>
    public static class TextMeasurerExtensions
    {
        /// <summary>
        /// Use cache for a maximum of <paramref name="maxSize"/> measurements.
        /// </summary>
        /// <param name="measureText"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static IMeasureText UseCache(this IMeasureText measureText, int maxSize)
        {
            var cache = new TextMeasurerCache(maxSize);
            var measurer = new TextMeasurerWithCache(measureText, cache);
            return measurer;
        }
    }
}
