using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualStaff : BaseContentWrapper
    {
        private readonly IStaff staff;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double length;
        private readonly Clef openingClef;
        private readonly KeySignature openingKeySignature;
        private readonly ColorARGB color;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;


        public StaffLayout Layout => scoreLayoutDictionary.GetOrDefault(staff);
        public static double KeySignatureGlyphSpacing { get; set; } = 0.9;
        public static double ClefSpacing { get; set; } = 5.5;
        public static double KeySignatureSpacing { get; set; } = 4.5;








        public VisualStaff(IStaff staff, double canvasLeft, double canvasTop, double length, Clef openingClef, KeySignature openingKeySignature, ColorARGB color, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.staff = staff;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.length = length;
            this.openingClef = openingClef;
            this.openingKeySignature = openingKeySignature;
            this.color = color;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }


        public IEnumerable<DrawableLineHorizontal> ConstructStaffLines()
        {
            for (int i = 0; i < 5; i++)
            {
                double heightOnPage = canvasTop + i * Layout.LineSpacing;

                yield return (new DrawableLineHorizontal(heightOnPage, canvasLeft, length, 0.1, color));
            }
        }
        public DrawableScoreGlyph ConstructOpeningClef()
        {
            var glyph = openingClef.ClefSpecies switch
            {
                ClefSpecies.G => GlyphLibrary.ClefG,
                ClefSpecies.F => GlyphLibrary.ClefF,
                ClefSpecies.C => GlyphLibrary.ClefC,
                ClefSpecies.Percussion => GlyphLibrary.ClefPercussion,
                _ => throw new NotImplementedException()
            };

            return new DrawableScoreGlyph(
                canvasLeft + 0.5,
                canvasTop,
                glyph,
                color);
        }
        public IEnumerable<DrawableScoreGlyph> ConstructOpeningKeySignature()
        {
            if (openingKeySignature.IndexInCircleOfFifths == 0)
            {
                yield break;
            }

            var xPosition = canvasLeft + 3.5;

            var _glyph = openingKeySignature.IndexInCircleOfFifths > 0 ?
                GlyphLibrary.Sharp :
                GlyphLibrary.Flat;

            var glyphWidth = _glyph.Width;

            var flats = openingKeySignature.DefaultFlats;
            var accidentalLines = flats ?
                openingKeySignature.EnumerateFlatLines(openingClef) :
                openingKeySignature.EnumerateSharpLines(openingClef);
            foreach (var line in accidentalLines)
            {
                xPosition += glyphWidth * KeySignatureGlyphSpacing;

                var yPosition = staff.HeightFromLineIndex(canvasTop, line - 4, scoreLayoutDictionary);

                var glyph = new DrawableScoreGlyph(
                    xPosition,
                    yPosition,
                    _glyph,
                    color);

                yield return glyph;
            }
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            foreach (var item in ConstructStaffLines())
            {
                yield return (item);
            }

            foreach (var glyph in ConstructOpeningKeySignature())
            {
                yield return glyph;
            }

            yield return ConstructOpeningClef();
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
    }
}
