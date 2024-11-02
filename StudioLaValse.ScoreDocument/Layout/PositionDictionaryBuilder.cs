using StudioLaValse.ScoreDocument.Extensions.Private;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// The default implementation of the position dictionary builder.
    /// Builds a dictionary of canvas positions for elements in a score measure.
    /// </summary>
    public class PositionDictionaryBuilder : IPositionDictionaryBuilder
    {
        private readonly IEqualityComparer<Position> equalityComparer;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="equalityComparer"></param>
        public PositionDictionaryBuilder(IEqualityComparer<Position> equalityComparer)
        {
            this.equalityComparer = equalityComparer;
        }

        /// <summary>
        /// Bulid the position dictionary.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <returns></returns>
        public PositionDictionary Build(IScoreMeasure scoreMeasure)
        {
            var scoreScale = scoreMeasure.Scale;

            var positions = new PositionDictionarySource(equalityComparer);
            foreach (var instrumentMeasure in scoreMeasure.ReadMeasures())
            {
                var left = 0d;
                var orderedChords = instrumentMeasure.ReadChords().OrderBy(e => e.Position.Decimal);
                foreach (var positionGroup in orderedChords.GroupBy(e => e.Position, equalityComparer))
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
                    var positionRecord = new PositionInMeasure(left, spaceRight);

                    if (positions.TryGetValue(position, out var positionFromParamer))
                    {
                        var maxSpaceRight = Math.Max(spaceRight, positionFromParamer.SpaceRight);
                        var maxLeft = Math.Max(left, positionFromParamer.Position);
                        positions.Override(position, positionRecord);
                        left += maxSpaceRight;
                    }
                    else
                    {
                        positions.Add(position, positionRecord);
                        left += spaceRight;
                    }
                }
            }
            return positions.AsReadOnly();
        }
    }
}
