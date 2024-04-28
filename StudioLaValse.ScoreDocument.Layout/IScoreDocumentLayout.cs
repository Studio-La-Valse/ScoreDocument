using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout.Templates;

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

        double GetInstrumentScale(Instrument instrument);
    }
}