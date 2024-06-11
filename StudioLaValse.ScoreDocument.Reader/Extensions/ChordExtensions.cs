using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ChordExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static double Map(this double value, double minStart, double maxStart, double minEnd, double maxEnd)
        {
            var fraction = maxStart - minStart;

            return fraction == 0 ? throw new ArgumentOutOfRangeException() : minEnd + ((maxEnd - minEnd) * ((value - minStart) / fraction));
        }


        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<Position, (double, double)> Remap(this Dictionary<Position, (double, double)> positions, double canvasLeft, double canvasRight)
        {
            var originalMin = positions.Min(e => e.Value.Item1);
            var originalMax = positions.Max(e => e.Value.Item1 + e.Value.Item2);

            foreach (var kv in positions)
            {
                var position = kv.Value.Item1.Map(originalMin, originalMax, canvasLeft, canvasRight);
                var spaceRight = kv.Value.Item2.Map(originalMin, originalMax, canvasLeft, canvasRight);
                positions[kv.Key] = (position, spaceRight);
            }

            return positions;
        }


        public static Dictionary<Position, double> PositionsOnly(this Dictionary<Position, (double position, double spaceRight)> dictionary)
        {
            return dictionary.OrderBy(e => e.Key.Decimal).ToDictionary(e => e.Key, e => e.Value.position, dictionary.Comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<Position, (double position, double spaceRight)> EnumeratePositions(this IScoreMeasureReader scoreMeasureReader)
        {
            var comparer = new PositionComparer();
            var positions = new Dictionary<Position, (double position, double spaceRight)>(comparer);
            foreach (var instrumentMeasure in scoreMeasureReader.ReadMeasures())
            {
                if (instrumentMeasure.ReadLayout().Collapsed)
                {
                    continue;
                }
                var left = 0d;
                foreach (var positionGroup in instrumentMeasure.ReadChords().OrderBy(e => e.Position.Decimal).GroupBy(e => e.Position, comparer))
                {
                    var spaceRight = positionGroup.Max(e => e.ReadLayout().SpaceRight);
                    var graceSpace = positionGroup.Max(e =>
                    {
                        var graceGroup = e.ReadGraceGroup();
                        var space = 0d;
                        if (graceGroup is null || !graceGroup.ReadLayout().OccupySpace)
                        {
                            return space;
                        }
                        space = graceGroup.ReadChords().Count() * (graceGroup.ReadLayout().ChordSpacing * graceGroup.ReadLayout().Scale);
                        return space;
                    });
                    left += graceSpace;
                    var position = positionGroup.First().Position;

                    if(positions.TryGetValue(position, out var positionFromParamer))
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
        /// <summary>
        /// Approximates the required width of a score measure by enumerating all unique positions and accounting for the required space for each of them. 
        /// Takes into account any styling like padding or margins.
        /// </summary>
        public static double ApproximateWidth(this IScoreMeasureReader scoreMeasure)
        {
            var (position, spaceRight) = scoreMeasure.EnumeratePositions().LastOrDefault().Value;
            var measureLayout = scoreMeasure.ReadLayout();
            var measurePadding = measureLayout.PaddingLeft + measureLayout.PaddingRight;
            return position + spaceRight + measurePadding;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<IChordReader> ReadChords(this IInstrumentMeasureReader instrumentMeasureReader)
        {
            foreach (var voice in instrumentMeasureReader.ReadVoices())
            {
                foreach (var chord in instrumentMeasureReader.ReadChords(voice))
                {
                    yield return chord;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<INoteReader> ReadNotes(this IInstrumentMeasureReader instrumentMeasureReader)
        {
            foreach (var chord in instrumentMeasureReader.ReadChords())
            {
                foreach(var note in chord.ReadNotes())
                {
                    yield return note;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<IChordReader> ReadChords(this IInstrumentMeasureReader instrumentMeasureReader, int voice)
        {
            foreach (var chord in instrumentMeasureReader.ReadBlockChainAt(voice).ReadBlocks().SelectMany(e => e.ReadChords()))
            {
                yield return chord;
            }
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
