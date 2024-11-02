using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Core.Extensions;
using StudioLaValse.ScoreDocument.Extensions.Private;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extensions to the <see cref="IScoreMeasure"/> interface.
    /// </summary>
    public static partial class ScoreMeasureExtensions
    {
        /// <summary>
        /// <param name="scoreMeasure"></param>
        /// Approximates the required width of a score measure by enumerating all unique positions and accounting for the required space for each of them. 
        /// Takes into account any styling like padding or margins.
        /// <param name="positionDictionaryBuilder"></param>
        /// </summary>
        public static double ApproximateWidth(this IScoreMeasure scoreMeasure, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            if (!scoreMeasure.ReadMeasures().Any(m => m.ReadChords().Any()))
            {
                return 50;
            }
            var scoreScale = scoreMeasure.Scale;
            var rightOfMeasure = scoreMeasure.EnumeratePositions(positionDictionaryBuilder).GetLast().GetRight();
            var measurePadding = scoreMeasure.PaddingLeft * scoreScale + scoreMeasure.PaddingRight * scoreScale;
            return rightOfMeasure + measurePadding;
        }

        /// <summary>
        /// Interpolate a position in a score measure, assuming a canvasleft for the score measure.
        /// Intended for positions of clef changes for example. Does not work at all for grace positions.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <param name="position"></param>
        /// <param name="measureStart"></param>
        /// <param name="positionDictionaryBuilder"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static double InterpolatePosition(this IScoreMeasure scoreMeasure, Position position, double measureStart, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            var positionDictionary = scoreMeasure.EnumeratePositions(positionDictionaryBuilder);
            if (positionDictionary.TryGetValue(position, out var positionInMeasure))
            {
                return positionInMeasure.Position;
            }

            var orderedPositions = positionDictionary.ReadAll().OrderBy(e => e.Key.Decimal).ToList();
            if (orderedPositions.Count == 0)
            {
                throw new InvalidOperationException("The source dictionary does not contain any items, so the position cannot be interpolated.");
            }

            KeyValuePair<Position, PositionInMeasure>? positionLeft = null;
            KeyValuePair<Position, PositionInMeasure>? positionRight = null;
            for (var i = 0; i < orderedPositions.Count; i++)
            {
                if (orderedPositions[i].Key.Decimal < position.Decimal)
                {
                    positionLeft = orderedPositions[i];
                }
                else
                {
                    positionRight = orderedPositions[i];
                    break;
                }
            }

            if (positionLeft == null)
            {
                var firstPositionAvailable = orderedPositions[0].Value.Position;
                var _positionInMeasure = new PositionInMeasure(measureStart, firstPositionAvailable);
                var _position = Position.Start();
                positionLeft = new KeyValuePair<Position, PositionInMeasure>(_position, _positionInMeasure);
            }

            if (positionRight == null)
            {
                var farRight = orderedPositions[^1].Value.GetRight();
                var _positionInMeasure = new PositionInMeasure(farRight, 0);
                var _position = scoreMeasure.TimeSignature;
                positionRight = new KeyValuePair<Position, PositionInMeasure>(position, _positionInMeasure);
            }

            var param = (double)position.Decimal.Map(0, 1, positionLeft.Value.Key.Decimal, positionRight.Value.Key.Decimal);
            var resultPosition = param.Map(0, 1, positionLeft.Value.Value.Position, positionRight.Value.Value.Position);
            return resultPosition;
        }

        /// <summary>
        /// Create a dictionary of unique positions in the score measure.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <param name="positionDictionaryBuilder"></param>
        /// <returns></returns>
        public static PositionDictionary EnumeratePositions(this IScoreMeasure scoreMeasure, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            var equalityComparer = new PositionComparer();
            var builder = positionDictionaryBuilder ?? new PositionDictionaryBuilder(equalityComparer);
            var dict = builder.Build(scoreMeasure);
            return dict;
        }

        /// <summary>
        /// Enumerate positions in a measure for a grace group.
        /// </summary>
        /// <param name="graceGroup"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static PositionDictionary EnumeratePositions(this IGraceGroup graceGroup, double target)
        {
            var equalityComparer = new PositionComparer();
            var dictionary = new PositionDictionarySource(equalityComparer);
            var layout = graceGroup;
            var position = graceGroup.Target;
            foreach (var chord in graceGroup.ReadChords().OrderBy(e => e.IndexInGroup).Reverse())
            {
                var chordSpaceRight = layout.ChordSpacing * layout.Scale;
                target -= chordSpaceRight;
                position -= layout.ChordDuration;
                var positionInMeasure = new PositionInMeasure(target, chordSpaceRight);
                dictionary.Add(position, positionInMeasure);
            }
            return dictionary.AsReadOnly();
        }
    }
}
