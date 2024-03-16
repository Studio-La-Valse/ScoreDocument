using StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffGroup : BaseContentWrapper
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly ColorARGB color;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly double canvasTop;


        public IInstrumentRibbonReader Context => staffGroup.InstrumentRibbon;
        public InstrumentRibbonLayout ContextLayout => scoreLayoutDictionary.InstrumentRibbonLayout(Context);
        public StaffGroupLayout Layout => scoreLayoutDictionary.StaffGroupLayout(staffGroup);


        public DrawableText ID
        {
            get
            {
                double braceLeft = Brace?.TopLeftX ?? canvasLeft;
                string text = FirstMeasure.MeasureIndex == 0 ? ContextLayout.DisplayName : ContextLayout.AbbreviatedName;
                double height = staffGroup.CalculateHeight(scoreLayoutDictionary);
                DrawableText id = new(braceLeft - 2, canvasTop + height / 2, text, 2, color, HorizontalTextOrigin.Right, VerticalTextOrigin.Center);

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

                double heightOfGroup = staffGroup.CalculateHeight(scoreLayoutDictionary);
                double knownHeightOfTheBrace = 4.8;
                double scale = heightOfGroup / knownHeightOfTheBrace;

                Glyph glyph = GlyphLibrary.Brace;
                glyph.Scale = scale;

                return new DrawableScoreGlyph(canvasLeft - glyph.Width - 0.1, canvasTop, glyph, color);
            }
        }
        public IInstrumentMeasureReader FirstMeasure =>
            staffGroup.EnumerateMeasures().First();



        public VisualStaffGroup(IStaffGroupReader staffGroup, double canvasLeft, double canvasTop, double length, ColorARGB color, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.staffGroup = staffGroup;
            this.canvasLeft = canvasLeft;
            this.length = length;
            this.color = color;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.canvasTop = canvasTop;
        }



        public IEnumerable<VisualStaff> ConstructStaves()
        {
            double _canvasTop = canvasTop;
            var lineSpacing = Layout.LineSpacing;
            foreach (IStaffReader staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
            {
                StaffLayout staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                Clef clef = FirstMeasure.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                KeySignature keySignature = FirstMeasure.KeySignature;
                VisualStaff newStaff = new(
                    staff,
                    canvasLeft,
                    _canvasTop,
                    length,
                    lineSpacing,
                    clef,
                    keySignature,
                    color,
                    scoreLayoutDictionary);

                yield return newStaff;

                _canvasTop += staff.CalculateHeight(lineSpacing);
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
