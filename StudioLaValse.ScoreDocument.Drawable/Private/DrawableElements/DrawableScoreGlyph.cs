using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements
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

    internal record TextMeasureKey(string Text, FontFamilyCore Font)
    {

    }

    internal record TextMeasureValue(XY Size, double FontSize)
    {

    }

    internal class TextMeasurerCache
    {
        private readonly int _capacity;
        private readonly Dictionary<TextMeasureKey, LinkedListNode<KeyValuePair<TextMeasureKey, TextMeasureValue>>> _cache;
        private readonly LinkedList<KeyValuePair<TextMeasureKey, TextMeasureValue>> _lruList;

        public TextMeasurerCache(int capacity)
        {
            _capacity = capacity;
            _cache = new Dictionary<TextMeasureKey, LinkedListNode<KeyValuePair<TextMeasureKey, TextMeasureValue>>>(capacity);
            _lruList = new LinkedList<KeyValuePair<TextMeasureKey, TextMeasureValue>>();
        }


        public TextMeasureValue? Get(TextMeasureKey key)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                _lruList.Remove(node);
                _lruList.AddLast(node);

                return node.Value.Value;
            }
            return null;
        }

        public void Add(TextMeasureKey key, TextMeasureValue size)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                _lruList.Remove(value);
            }
            else if (_cache.Count >= _capacity)
            {
                var firstNode = _lruList.First;
                if (firstNode != null)
                {
                    _cache.Remove(firstNode.Value.Key);
                    _lruList.RemoveFirst();
                }
            }

            var newNode = new KeyValuePair<TextMeasureKey, TextMeasureValue>(key, size);
            var node = new LinkedListNode<KeyValuePair<TextMeasureKey, TextMeasureValue>>(newNode);
            _lruList.AddLast(node);
            _cache[key] = node;
        }
    }

    internal sealed class DrawableScoreGlyph : DrawableText
    {
        private readonly Glyph glyph;

        public DrawableScoreGlyph(double locationX, double locationY, Glyph glyph, HorizontalTextOrigin horizontalTextOrigin, VerticalTextOrigin verticalTextOrigin, ColorARGB color) :
            base(locationX, locationY, glyph.StringValue, glyph.Points, color, horizontalTextOrigin, verticalTextOrigin, new FontFamilyCore(glyph.FontFamilyKey!, glyph.FontFamily))
        {
            this.glyph = glyph;
        }


        private double GetLeft(double knownWidth)
        {
            var left = OriginX;
            if (HorizontalAlignment == HorizontalTextOrigin.Left)
            {
                return left;
            }

            if (HorizontalAlignment == HorizontalTextOrigin.Center)
            {
                left -= knownWidth / 2;
                return left;
            }

            if (HorizontalAlignment == HorizontalTextOrigin.Right)
            {
                left -= knownWidth;
                return left;
            }

            throw new NotImplementedException(nameof(HorizontalAlignment));
        }

        private double GetTop(double knownHeight)
        {
            var top = OriginY;
            if (VerticalAlignment == VerticalTextOrigin.Top)
            {
                return top;
            }

            if (VerticalAlignment == VerticalTextOrigin.Center)
            {
                top -= knownHeight / 2;
                return top;
            }

            if (VerticalAlignment == VerticalTextOrigin.Bottom)
            {
                top -= knownHeight;
                return top;
            }

            throw new NotImplementedException(nameof(HorizontalAlignment));
        }

        public override BoundingBox GetBoundingBox()
        {
            if(!glyph.KnownWidth.HasValue || !glyph.KnownHeight.HasValue)
            {
                return base.GetBoundingBox();
            }

            var left = GetLeft(glyph.KnownWidth.Value);
            var top = GetTop(glyph.KnownHeight.Value);

            return new BoundingBox(left, left + glyph.KnownWidth.Value, top, top + glyph.KnownHeight.Value);
        }
    }
}
