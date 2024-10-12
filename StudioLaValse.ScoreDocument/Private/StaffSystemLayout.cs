using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffSystemLayout : IStaffSystemLayout
    {
        private readonly IScoreDocumentLayout scoreDocument;

        public ReadonlyTemplateProperty<double> PaddingBottom { get; }

        public ReadonlyTemplateProperty<double> VerticalStaffLineThickness => scoreDocument.VerticalStaffLineThickness;

        public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness => scoreDocument.HorizontalStaffLineThickness;

        public ReadonlyTemplateProperty<double> Scale => scoreDocument.Scale;

        public ReadonlyTemplateProperty<ColorARGB> Color => scoreDocument.PageForegroundColor;

        public StaffSystemLayout(ReadonlyTemplateProperty<double> paddingBottom, IScoreDocumentLayout scoreDocument)
        {
            PaddingBottom = paddingBottom;
            this.scoreDocument = scoreDocument;
        }
    }
}
