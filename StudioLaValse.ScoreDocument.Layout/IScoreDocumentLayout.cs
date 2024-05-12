using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreDocumentLayout
    {
        double FirstSystemIndent { get; }
        double HorizontalStaffLineThickness { get; }
        double Scale { get; }
        double StemLineThickness { get; }
        double VerticalStaffLineThickness { get; }
        
        ColorARGB PageColor { get; }
        ColorARGB ForegroundColor { get; }

        double GetInstrumentScale(IInstrumentRibbon instrumentRibbon);
    }
}