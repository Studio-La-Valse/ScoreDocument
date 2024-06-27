using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extensions to the <see cref="IScoreMeasure"/> interface.
    /// </summary>
    public static class ScoreMeasureExtensions
    {
        /// <summary>
        /// Approximates the required width of a score measure by enumerating all unique positions and accounting for the required space for each of them. 
        /// Takes into account any styling like padding or margins.
        /// </summary>
        public static double ApproximateWidth(this IScoreMeasure scoreMeasure, double scoreScale)
        {
            var (position, spaceRight) = scoreMeasure.EnumeratePositions(scoreScale).LastOrDefault().Value;
            var measurePadding = scoreMeasure.PaddingLeft * scoreScale + scoreMeasure.PaddingRight * scoreScale;
            return position + spaceRight + measurePadding;
        }

        /// <summary>
        /// Create a dictionary of unique positions in the score measure.
        /// </summary>
        /// <param name="scoreMeasureReader"></param>
        /// <param name="scoreScale"></param>
        /// <returns></returns>
        public static Dictionary<Position, (double position, double spaceRight)> EnumeratePositions(this IScoreMeasure scoreMeasureReader, double scoreScale)
        {
            var comparer = new PositionComparer();
            var positions = new Dictionary<Position, (double position, double spaceRight)>(comparer);
            foreach (var instrumentMeasure in scoreMeasureReader.ReadMeasures())
            {
                if (instrumentMeasure.Collapsed ?? false)
                {
                    continue;
                }
                var left = 0d;
                foreach (var positionGroup in instrumentMeasure.ReadChords().OrderBy(e => e.Position.Decimal).GroupBy(e => e.Position, comparer))
                {
                    var spaceRight = positionGroup.Max(e => e.SpaceRight * scoreScale);
                    var graceSpace = positionGroup.Max(e =>
                    {
                        var graceGroup = e.ReadGraceGroup();
                        var space = 0d;
                        if (graceGroup is null || !graceGroup.OccupySpace)
                        {
                            return space;
                        }
                        space = graceGroup.ReadChords().Count() * (graceGroup.ChordSpacing * scoreScale * graceGroup.Scale);
                        return space;
                    });
                    left += graceSpace;
                    var position = positionGroup.First().Position;

                    if (positions.TryGetValue(position, out var positionFromParamer))
                    {
                        var maxSpaceRight = Math.Max(spaceRight, positionFromParamer.spaceRight);
                        var maxLeft = Math.Max(left, positionFromParamer.position);
                        positions[position] = (maxLeft, maxSpaceRight);
                        left += maxSpaceRight;
                    }
                    else
                    {
                        positions.Add(position, (left, spaceRight));
                        left += spaceRight;
                    }
                }
            }
            return positions;
        }
    }


    file class PositionComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position? x, Position? y)
        {
            if (x == null || y == null)
            {
                throw new InvalidOperationException();
            }

            return x.Decimal == y.Decimal;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            return obj.Decimal.GetHashCode();
        }
    }
}
