using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffGroup : BaseContentWrapper
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly ColorARGB color;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;
        private readonly double canvasTop;


        public IInstrumentRibbonReader Context => staffGroup.InstrumentRibbon;
        public InstrumentRibbonLayout ContextLayout => scoreLayoutDictionary.InstrumentRibbonLayout(Context);
        public StaffGroupLayout Layout => scoreLayoutDictionary.StaffGroupLayout(staffGroup);


        public DrawableText ID
        {
            get
            {
                var braceLeft = Brace?.TopLeftX ?? canvasLeft;
                var text = FirstMeasure.MeasureIndex == 0 ? ContextLayout.DisplayName : ContextLayout.AbbreviatedName;
                var height = staffGroup.CalculateHeight(scoreLayoutDictionary);
                var id = new DrawableText(braceLeft - 2, canvasTop + height / 2, text, 2, color, HorizontalTextOrigin.Right, VerticalTextOrigin.Center);

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

                var heightOfGroup = staffGroup.CalculateHeight(scoreLayoutDictionary);
                var knownHeightOfTheBrace = 4.8;
                var scale = heightOfGroup / knownHeightOfTheBrace;

                var glyph = GlyphLibrary.Brace;
                glyph.Scale = scale;

                return new DrawableScoreGlyph(canvasLeft - glyph.Width - 0.1, canvasTop, glyph, color);
            }
        }
        public IInstrumentMeasureReader FirstMeasure =>
            staffGroup.EnumerateMeasures().First();



        public VisualStaffGroup(IStaffGroupReader staffGroup, double canvasLeft, double canvasTop, double length, ColorARGB color, IScoreLayoutProvider scoreLayoutDictionary)
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
            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
            {
                var staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                var clef = FirstMeasure.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                var keySignature = FirstMeasure.KeySignature;
                var newStaff = new VisualStaff(
                    staff,
                    canvasLeft,
                    _canvasTop,
                    length,
                    clef,
                    keySignature,
                    color,
                    scoreLayoutDictionary);

                yield return newStaff;

                _canvasTop += staff.CalculateHeight(scoreLayoutDictionary);
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
