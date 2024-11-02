namespace StudioLaValse.ScoreDocument.Drawable.Private.TextMeasurer
{
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
}
