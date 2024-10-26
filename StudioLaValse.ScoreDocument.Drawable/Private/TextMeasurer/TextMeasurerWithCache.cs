namespace StudioLaValse.ScoreDocument.Drawable.Private.TextMeasurer
{
    internal class TextMeasurerWithCache : IMeasureText
    {
        private readonly IMeasureText measurerBase;
        private readonly TextMeasurerCache cache;

        public TextMeasurerWithCache(IMeasureText measurerBase, TextMeasurerCache cache)
        {
            this.measurerBase = measurerBase;
            this.cache = cache;
        }

        public XY Measure(string text, FontFamilyCore fontFamily, double size)
        {
            var key = new TextMeasureKey(text, fontFamily);
            if (cache.Get(key) is TextMeasureValue value)
            {
                var factor = size / value.FontSize;
                var scaled = value.Size * factor;
                return scaled;
            }
            else
            {
                var resultSize = measurerBase.Measure(text, fontFamily, size);
                var newValue = new TextMeasureValue(resultSize, size);
                cache.Add(key, newValue);
                return resultSize;
            }
        }
    }
}
