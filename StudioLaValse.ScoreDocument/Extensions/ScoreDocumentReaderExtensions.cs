﻿using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Private;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extensions for a score document.
    /// </summary>
    public static class ScoreDocumentReaderExtensions
    {
        /// <summary>
        /// Read the pages of a score document.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <returns></returns>
        public static IEnumerable<IPage> ReadPages(this IScoreDocument scoreDocument)
        {
            var scoreScale = scoreDocument.Scale;
            var lineSpacing = Glyph.LineSpacingMm;
            var currentpage = new Page(0, scoreDocument);
            currentpage.StaffSystems.Clear();
            var currentSystem = new StaffSystem(scoreDocument);
            currentpage.StaffSystems.Add(currentSystem);

            var pageLayout = currentpage.Layout;
            var pageWidth = pageLayout.PageWidth;
            var pageHeight = pageLayout.PageHeight;
            var pageMarginBottom = pageLayout.MarginBottom;

            var systemIndex = 1;
            var pageIndex = 1;
            var currentSystemCanvasTop = pageLayout.MarginTop.Value;

            foreach (var measure in scoreDocument.ReadScoreMeasures())
            {
                currentSystem.ScoreMeasures.Add(measure);

                var currentSystemLength = currentSystem.ScoreMeasures.Select(m => m.ApproximateWidth(scoreScale)).Sum();
                var currentAvailableWidth = pageWidth - pageLayout.MarginLeft - pageLayout.MarginRight;

                // Need to add a new system.
                if (currentSystemLength > currentAvailableWidth && currentSystem.ScoreMeasures.Any())
                {
                    var previousSystemHeight = currentSystem.CalculateHeight(lineSpacing, scoreDocument);
                    var previousSystemMarginBottom = currentSystem.ReadLayout().PaddingBottom * scoreScale;
                    currentSystem = new StaffSystem(scoreDocument);
                    currentSystemCanvasTop += previousSystemHeight + previousSystemMarginBottom;

                    var currentSystemCanvasBottom = currentSystemCanvasTop + currentSystem.CalculateHeight(lineSpacing, scoreDocument);
                    var currentLowestAllowedPoint = pageHeight - pageMarginBottom;

                    // Need to add a new page.
                    if (currentSystemCanvasBottom > currentLowestAllowedPoint)
                    {
                        yield return currentpage;
                        currentpage = new Page(pageIndex, scoreDocument);
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

            if (!currentpage.StaffSystems.LastOrDefault()?.EnumerateMeasures().Any() ?? false)
            {
                currentpage.StaffSystems.RemoveAt(currentpage.StaffSystems.Count - 1);
            }

            if (currentpage.StaffSystems.Any(s => s.EnumerateMeasures().Any()))
            {
                yield return currentpage;
            }
        }
    }
}
