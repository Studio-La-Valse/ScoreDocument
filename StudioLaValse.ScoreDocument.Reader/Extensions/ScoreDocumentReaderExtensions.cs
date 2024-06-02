using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader.Private;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader.Extensions
{
    public static class ScoreDocumentReaderExtensions
    {
        public static IEnumerable<IPageReader> ReadPages(this IScoreDocumentReader scoreDocument)
        {
            var scoreDocumentLayout = scoreDocument.ReadLayout();

            var currentpage = new Page(0, scoreDocumentLayout);
            currentpage.StaffSystems.Clear();
            var currentSystem = new StaffSystem(scoreDocument);
            currentpage.StaffSystems.Add(currentSystem);

            var pageLayout = currentpage.Layout;
            var pageWidth = pageLayout.PageWidth;
            var pageHeight = pageLayout.PageHeight;
            var pageMarginBottom = pageLayout.MarginBottom;

            var systemIndex = 1;
            var pageIndex = 1;
            var currentSystemCanvasTop = pageLayout.MarginTop;
            var lineSpacing = 1.2;

            foreach (var measure in scoreDocument.ReadScoreMeasures())
            {
                currentSystem.ScoreMeasures.Add(measure);

                var currentSystemLength = currentSystem.ScoreMeasures.Select(m => m.ApproximateWidth()).Sum();
                var currentAvailableWidth = pageWidth - pageLayout.MarginLeft - pageLayout.MarginRight;
                // Need to add a new system.
                if (currentSystemLength > currentAvailableWidth && currentSystem.ScoreMeasures.Any())
                {
                    var previousSystemHeight = currentSystem.CalculateHeight(lineSpacing, scoreDocumentLayout);
                    var previousSystemMarginBottom = currentSystem.ReadLayout().PaddingBottom;
                    currentSystem = new StaffSystem(scoreDocument);
                    currentSystemCanvasTop += previousSystemHeight + previousSystemMarginBottom;

                    var currentSystemCanvasBottom = currentSystemCanvasTop + currentSystem.CalculateHeight(lineSpacing, scoreDocumentLayout);
                    var currentLowestAllowedPoint = pageHeight - pageMarginBottom;
                    // Need to add a new page.
                    if (currentSystemCanvasBottom > currentLowestAllowedPoint)
                    {
                        yield return currentpage;
                        currentpage = new Page(pageIndex, scoreDocumentLayout);
                        pageLayout = currentpage.Layout;
                        pageWidth = pageLayout.PageWidth;
                        pageHeight = pageLayout.PageHeight;
                        pageMarginBottom = pageLayout.MarginBottom;

                        currentSystemCanvasTop = pageLayout.MarginTop;
                        pageIndex++;
                    }

                    currentpage.StaffSystems.Add(currentSystem);
                    systemIndex++;
                }
            }

            if ((!currentpage.StaffSystems.LastOrDefault()?.EnumerateMeasures().Any()) ?? false)
            {
                currentpage.StaffSystems.RemoveAt(currentpage.StaffSystems.Count -1);
            }

            if (currentpage.StaffSystems.Any(s => s.EnumerateMeasures().Any()))
            {
                yield return currentpage;
            }
        }
    }


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
        public static void Remap(this Dictionary<Position, double> positions, double canvasLeft, double canvasRight)
        {
            var originalMin = positions.Min(e => e.Value);
            var originalMax = positions.Max(e => e.Value);

            foreach (var kv in positions)
            {
                var remappedValue = kv.Value.Map(originalMin, originalMax, canvasLeft, canvasRight);
                positions[kv.Key] = remappedValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Remap(this Dictionary<Position, double> positions, double canvasLeft, double canvasRight, TimeSignature timeSignature, double factor)
        {
            var originalMin = positions.Min(e => e.Value);
            var originalMax = positions.Max(e => e.Value);

            foreach (var kv in positions)
            {
                var remappedValue = kv.Value.Map(originalMin, originalMax, canvasLeft, canvasRight);

                var parameter = (double)(kv.Key.Decimal / timeSignature.Decimal);

                var positionFromParamer = parameter.Map(0, 1, canvasLeft, canvasRight);

                var finalValue = factor.Map(0, 1, remappedValue, positionFromParamer);

                positions[kv.Key] = finalValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<Position, double> EnumeratePositions(this IScoreMeasureReader scoreMeasureReader)
        {
            var comparer = new PositionComparer();
            var positions = new Dictionary<Position, double>(comparer);
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
                    var position = positionGroup.First().Position;

                    positions.Add(position, left);
                    left += spaceRight;
                }
            }
            return positions;
        }
        /// <summary>
        /// 
        /// </summary>
        public static double ApproximateWidth(this IScoreMeasureReader scoreMeasure)
        {
            var width = scoreMeasure.EnumeratePositions().Last().Value;
            return width;
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
