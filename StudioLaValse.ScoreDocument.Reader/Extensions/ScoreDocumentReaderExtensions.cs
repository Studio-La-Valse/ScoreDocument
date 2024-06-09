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



}
