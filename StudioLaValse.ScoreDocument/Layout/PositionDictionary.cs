using System.Diagnostics.CodeAnalysis;
using StudioLaValse.ScoreDocument.Extensions.Private;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A dictionary containing the positions of notes.
    /// </summary>
    public class PositionDictionary
    {
        private readonly PositionDictionarySource positionDictionary;

        /// <summary>
        /// Get the position in the measure.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public PositionInMeasure this[Position position] => positionDictionary[position];

        internal PositionDictionary(PositionDictionarySource positionDictionary)
        {
            this.positionDictionary = positionDictionary;
        }

        /// <summary>
        /// Get the position and space right.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public PositionInMeasure Get(Position position)
        {
            return positionDictionary.Get(position);
        }

        /// <summary>
        /// Read all positions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<Position, PositionInMeasure>> ReadAll()
        {
            return positionDictionary.ReadAll();
        }
        /// <summary>
        /// Try to get the value from the dictionary.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="positionInMeasure"></param>
        /// <returns></returns>
        public bool TryGetValue(Position position, [NotNullWhen(true)] out PositionInMeasure? positionInMeasure)
        {
            return positionDictionary.TryGetValue(position, out positionInMeasure);
        }
        /// <summary>
        /// Read the first position in the measure.
        /// </summary>
        /// <returns></returns>
        public PositionInMeasure GetFirst()
        {
            return positionDictionary.GetFirst();
        }
        /// <summary>
        /// Read the last position in the measure.
        /// </summary>
        /// <returns></returns>
        public PositionInMeasure GetLast()
        {
            return positionDictionary.GetLast();
        }
        /// <summary>
        /// Remap the positions.
        /// </summary>
        /// <param name="canvasLeft"></param>
        /// <param name="measureWidth"></param>
        /// <returns></returns>
        public PositionDictionary Remap(double canvasLeft, double measureWidth)
        {
            return positionDictionary.Remap(canvasLeft, measureWidth).AsReadOnly();
        }
    }
}
