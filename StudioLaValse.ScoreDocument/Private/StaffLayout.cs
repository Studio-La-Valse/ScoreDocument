using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models.Classes;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffLayout : IStaffLayout
    {
        private readonly IScoreDocumentLayout scoreDocument;
        private readonly IInstrumentRibbonLayout instrumentRibbon;

        public ReadonlyTemplateProperty<double> DistanceToNext { get; }

        public ReadonlyTemplateProperty<double> VerticalStaffLineThickness => scoreDocument.VerticalStaffLineThickness;

        public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness => scoreDocument.HorizontalStaffLineThickness;

        public ReadonlyTemplateProperty<double> Scale => new ReadonlyTemplatePropertyFromFunc<double>(() => instrumentRibbon.Scale);

        public ReadonlyTemplateProperty<ColorARGBClass> Color => new ReadonlyTemplatePropertyFromFunc<ColorARGBClass>(() => scoreDocument.PageForegroundColor);

        public StaffLayout(ReadonlyTemplateProperty<double> distanceToNext, IScoreDocumentLayout scoreDocument, IInstrumentRibbonLayout instrumentRibbon)
        {
            DistanceToNext = distanceToNext;
            this.scoreDocument = scoreDocument;
            this.instrumentRibbon = instrumentRibbon;
        }
    }
}
