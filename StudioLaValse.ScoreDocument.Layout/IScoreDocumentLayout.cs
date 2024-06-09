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
        double ChordPositionFactor { get; }

        ColorARGB PageColor { get; }
        ColorARGB PageForegroundColor { get; }
        double PageMarginBottom { get; }
        double PageMarginLeft { get; }
        double PageMarginRight { get; }
        double PageMarginTop { get; }
        int PageHeight { get; }
        int PageWidth { get; }

        double StaffSystemPaddingBottom { get; }
        double StaffGroupPaddingBottom { get; }
        double StaffPaddingBottom { get; }
    }
}