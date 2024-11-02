using StudioLaValse.ScoreDocument.Core.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Extensions
{
    /// <summary>
    /// Chord extensions.
    /// </summary>
    public static class ChordExtensions
    {
        /// <summary>
        /// Remap the generated dictionary to canvas space.
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasRight"></param>
        /// <returns></returns>
        public static Dictionary<Position, (double, double)> Remap(this Dictionary<Position, (double, double)> positions, double canvasLeft, double canvasRight)
        {
            if (positions.Count == 0)
            {
                return positions; 
            }
            var originalMin = positions.Min(e => e.Value.Item1);
            var originalMax = positions.Max(e => e.Value.Item1 + e.Value.Item2);
            var originalMinSpace = positions.Min(e => e.Value.Item2);
            var originalMaxSpace = positions.Max(e => e.Value.Item2);
            var originalWidth = originalMax - originalMin;
            var newWidth = canvasRight - canvasLeft;
            foreach (var kv in positions)
            {
                var position = kv.Value.Item1.Map(originalMin, originalMax, canvasLeft, canvasRight);
                var spaceRight = originalMinSpace == originalMaxSpace ?
                    newWidth / originalWidth * originalMinSpace :
                    kv.Value.Item2.Map(originalMinSpace, originalMaxSpace, originalWidth, newWidth);
                positions[kv.Key] = (position, spaceRight);
            }

            return positions;
        }

        /// <summary>
        /// Discard the space right values from the dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="positionSpace"></param>
        /// <returns></returns>
        public static Dictionary<Position, double> PositionsOnly(this Dictionary<Position, (double position, double spaceRight)> dictionary, out double positionSpace)
        {
            positionSpace = dictionary.FirstOrDefault().Value.spaceRight;
            return dictionary.OrderBy(e => e.Key.Decimal).ToDictionary(e => e.Key, e => e.Value.position, dictionary.Comparer);
        }
    }
}
