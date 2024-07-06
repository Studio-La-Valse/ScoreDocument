namespace StudioLaValse.ScoreDocument.Implementation.Private.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static Dictionary<TKey, TValue> DeepCopy<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : IEquatable<TKey> where TValue : struct
        {
            var dict = new Dictionary<TKey, TValue>();
            foreach (var kv in dictionary)
            {
                dict[kv.Key] = kv.Value;
            }
            return dict;
        }
    }
}
