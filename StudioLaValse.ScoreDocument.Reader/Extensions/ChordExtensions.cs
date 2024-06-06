﻿using System.Diagnostics.CodeAnalysis;

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


        public static Dictionary<Position, double> PositionsOnly(this Dictionary<Position, (double, double)> dictionary)
        {
            return dictionary.ToDictionary(e => e.Key, e => e.Value.Item1, dictionary.Comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<Position, (double, double)> EnumeratePositions(this IScoreMeasureReader scoreMeasureReader)
        {
            var comparer = new PositionComparer();
            var positions = new Dictionary<Position, (double, double)>(comparer);
            foreach (var instrumentMeasure in scoreMeasureReader.ReadMeasures())
            {
                if (instrumentMeasure.ReadLayout().Collapsed)
                {
                    continue;
                }
                var left = 0d;
                foreach (var positionGroup in instrumentMeasure.ReadChords().GroupBy(e => e.Position, comparer))
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
                        var max = Math.Max(spaceRight, positionFromParamer.Item2);
                        positions[position] = (left, max);
                        left += max;
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
        /// 
        /// </summary>
        public static double ApproximateWidth(this IScoreMeasureReader scoreMeasure)
        {
            var last = scoreMeasure.EnumeratePositions().LastOrDefault().Value;

            return last.Item1 + last.Item2;
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
