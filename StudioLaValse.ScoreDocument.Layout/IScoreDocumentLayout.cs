using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreDocumentLayout
    {
        string GlyphFamily { get; }

        double FirstSystemIndent { get; }
        double HorizontalStaffLineThickness { get; }
        double Scale { get; }
        double StemLineThickness { get; }
        double VerticalStaffLineThickness { get; }

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