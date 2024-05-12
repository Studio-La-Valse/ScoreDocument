using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffGroup : BaseContentWrapper
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double globalLineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly ColorARGB color;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly double canvasTop;


        public IInstrumentRibbonReader Context =>
            staffGroup.InstrumentRibbon;
        public IInstrumentRibbonLayout ContextLayout =>
            Context.ReadLayout();
        public IStaffGroupLayout Layout =>
            staffGroup.ReadLayout();
        public DrawableText ID
        {
            get
            {
                var braceLeft = Brace?.TopLeftX ?? canvasLeft;
                var text = FirstMeasure.MeasureIndex == 0 ? ContextLayout.DisplayName : ContextLayout.AbbreviatedName;
                var height = staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary);
                DrawableText id = new(braceLeft - 2, canvasTop + (height / 2), text, 2 * scoreScale, color, HorizontalTextOrigin.Right, VerticalTextOrigin.Center);

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

                var heightOfGroup = staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary);
                double knownHeightOfTheBrace = 6;
                var scale = heightOfGroup / knownHeightOfTheBrace;

                var glyph = GlyphLibrary.Brace;
                glyph.Scale = scale;

                return new DrawableScoreGlyph(canvasLeft, canvasTop + staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), glyph, HorizontalTextOrigin.Right, VerticalTextOrigin.Center, color);
            }
        }
        public IInstrumentMeasureReader FirstMeasure =>
            staffGroup.EnumerateMeasures().First();


        public VisualStaffGroup(IStaffGroupReader staffGroup, double canvasLeft, double canvasTop, double length, double globalLineSpacing, double scoreScale, double instrumentScale, ColorARGB color, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.staffGroup = staffGroup;
            this.canvasLeft = canvasLeft;
            this.length = length;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.color = color;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.canvasTop = canvasTop;
        }



        public IEnumerable<VisualStaff> ConstructStaves()
        {
            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
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
                    color,
                    scoreLayoutDictionary);

                yield return newStaff;

                _canvasTop += staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale);
                _canvasTop += staffLayout.DistanceToNext;
            }
        }




        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            if (ContextLayout.Collapsed)
            {
                yield return new DrawableLineHorizontal(canvasTop, canvasLeft, length, 0.1, color);
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
