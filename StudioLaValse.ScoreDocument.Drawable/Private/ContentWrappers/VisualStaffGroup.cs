using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffGroup : BaseContentWrapper
    {
        private readonly IStaffGroup staffGroup;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double globalLineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly double canvasTop;


        public IInstrumentRibbon Context =>
            staffGroup.InstrumentRibbon;
        public IInstrumentRibbonLayout ContextLayout =>
            Context.ReadLayout();
        public IStaffGroupLayout Layout =>
            staffGroup.ReadLayout();
        public double Height =>
            unitToPixelConverter.UnitsToPixels(staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary));
        public DrawableText ID
        {
            get
            {
                var braceLeft = Brace?.TopLeftX ?? canvasLeft;
                var text = FirstMeasure.MeasureIndex == 0 ? ContextLayout.DisplayName : ContextLayout.AbbreviatedName;
                var id = new DrawableText(braceLeft - 2, canvasTop + (Height / 2), text, Glyph.Em /2 * scoreScale, scoreLayoutDictionary.PageForegroundColor.FromPrimitive(), HorizontalTextOrigin.Right, VerticalTextOrigin.Center);

                return id;
            }
        }
        public DrawableScoreGlyph? Brace
        {
            get
            {
                if (Layout.NumberOfStaves <= 1)
                {
                    return null;
                }

                if (ContextLayout.Collapsed)
                {
                    return null;
                }

                var knownHeightOfTheBrace = Glyph.Em;
                var scale = Height / knownHeightOfTheBrace;
                var glyph = glyphLibrary.Brace(scale);

                return new DrawableScoreGlyph(canvasLeft, canvasTop + Height, glyph, HorizontalTextOrigin.Right, VerticalTextOrigin.Center, scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
            }
        }
        public IInstrumentMeasure FirstMeasure =>
            staffGroup.EnumerateMeasures().First();


        public VisualStaffGroup(IStaffGroup staffGroup,
                                double canvasLeft,
                                double canvasTop,
                                double length,
                                double globalLineSpacing,
                                double scoreScale,
                                double instrumentScale,
                                IGlyphLibrary glyphLibrary,
                                IScoreDocumentLayout scoreLayoutDictionary,
                                IUnitToPixelConverter unitToPixelConverter)
        {
            this.staffGroup = staffGroup;
            this.canvasLeft = canvasLeft;
            this.length = length;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.glyphLibrary = glyphLibrary;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
            this.canvasTop = canvasTop;
        }



        public IEnumerable<VisualStaff> ConstructStaves()
        {
            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves())
            {
                var staffLayout = staff.ReadLayout();
                var clef = FirstMeasure.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var keySignature = FirstMeasure.ReadLayout().KeySignature;
                var timeSignature = FirstMeasure.MeasureIndex == 0 ? FirstMeasure.TimeSignature : null;
                VisualStaff newStaff = new(
                    staff,
                    canvasLeft,
                    _canvasTop,
                    length,
                    globalLineSpacing,
                    scoreScale,
                    instrumentScale,
                    scoreLayoutDictionary.HorizontalStaffLineThickness,
                    clef,
                    keySignature,
                    timeSignature,
                    glyphLibrary,
                    scoreLayoutDictionary,
                    unitToPixelConverter);

                yield return newStaff;

                _canvasTop += unitToPixelConverter.UnitsToPixels(staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale));
                _canvasTop += unitToPixelConverter.UnitsToPixels(staffLayout.DistanceToNext * scoreScale);
            }
        }




        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            if (ContextLayout.Collapsed)
            {
                yield return new DrawableLineHorizontal(canvasTop, canvasLeft, length, 0.1, scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
            }
            else
            {
                if (Brace != null)
                {
                    yield return Brace;
                }

                yield return ID;
            }
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return ConstructStaves();
        }
    }
}
