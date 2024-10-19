using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffGroup : BaseContentWrapper
    {
        private readonly IStaffGroup staffGroup;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly double canvasTop;


        public double InstrumentScale => 
            staffGroup.InstrumentRibbon.Scale;
        public IInstrumentRibbon Context =>
            staffGroup.InstrumentRibbon;
        public IInstrumentRibbon ContextLayout =>
            Context;
        public IStaffGroupLayout Layout =>
            staffGroup;
        public double Height =>
            staffGroup.CalculateHeight();
        public DrawableText ID
        {
            get
            {
                var knownHeightOfTheBrace = Glyph.Em;
                var scale = Height / knownHeightOfTheBrace;
                var heightOfBrace = knownHeightOfTheBrace * scale;
                var ratioApprox = 1 / 12d;
                var widthOfBrace = heightOfBrace * ratioApprox;
                var braceRight = canvasLeft;
                var distanceFromBrace = 1;
                var textRight = braceRight - widthOfBrace - distanceFromBrace;
                var text = FirstMeasure.MeasureIndex == 0 ? ContextLayout.DisplayName : ContextLayout.AbbreviatedName;
                var fontFamily = new FontFamilyCore("Arial");
                var id = new DrawableText(
                    originX: textRight, 
                    originY: canvasTop + (Height / 2), 
                    text: text,
                    fontSize: Glyph.Em / 2 * staffGroup.Scale, 
                    color: staffGroup.Color.Value.FromPrimitive(), 
                    horizontalAlignment: HorizontalTextOrigin.Right, 
                    verticalAlignment: VerticalTextOrigin.Center,
                    fontFamily: fontFamily);

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

                if (Layout.Visibility != Visibility.Visible)
                {
                    return null;
                }

                var knownHeightOfTheBrace = Glyph.Em;
                var scale = Height / knownHeightOfTheBrace;
                var glyph = glyphLibrary.Brace(scale);

                return new DrawableScoreGlyph(canvasLeft, canvasTop + Height, glyph, HorizontalTextOrigin.Right, VerticalTextOrigin.Center, staffGroup.Color.Value.FromPrimitive());
            }
        }
        public IInstrumentMeasure FirstMeasure =>
            staffGroup.EnumerateMeasures().First();


        public VisualStaffGroup(IStaffGroup staffGroup,
                                double canvasLeft,
                                double canvasTop,
                                double length,
                                IGlyphLibrary glyphLibrary)
        {
            this.staffGroup = staffGroup;
            this.canvasLeft = canvasLeft;
            this.length = length;
            this.glyphLibrary = glyphLibrary;
            this.canvasTop = canvasTop;
        }



        public IEnumerable<BaseContentWrapper> ConstructStaves()
        {
            if (Layout.Visibility != Visibility.Visible)
            {
                yield break;
            }

            foreach (var (staff, canvasTop) in staffGroup.EnumerateFromTop(this.canvasTop))
            {
                var clef = FirstMeasure.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var keySignature = FirstMeasure.KeySignature;
                var timeSignature = FirstMeasure.MeasureIndex == 0 ? FirstMeasure.TimeSignature : null;

                VisualStaff newStaff = new(
                    staff,
                    canvasLeft,
                    canvasTop,
                    length,
                    clef,
                    keySignature,
                    timeSignature,
                    glyphLibrary);

                yield return newStaff;
            }
        }




        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            if (ContextLayout.Visibility == Visibility.Hidden)
            {
                yield break;
            }

            if (ContextLayout.Visibility == Visibility.Collapsed)
            {
                yield return new DrawableLineHorizontal(canvasTop, canvasLeft, length, 0.1, staffGroup.Color.Value.FromPrimitive());
            }

            if (Brace != null)
            {
                yield return Brace;
            }

            yield return ID;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return ConstructStaves();
        }
    }
}
