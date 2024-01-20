using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualStaff : BaseContentWrapper
    {
        private readonly IStaffReader staff;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double length;
        private readonly Clef openingClef;
        private readonly KeySignature openingKeySignature;
        private readonly ColorARGB color;



        public static double KeySignatureGlyphSpacing { get; set; } = 0.9;
        public static double ClefSpacing { get; set; } = 5.5;
        public static double KeySignatureSpacing { get; set; } = 4.5;








        public VisualStaff(IStaffReader staff, double canvasLeft, double canvasTop, double length, Clef openingClef, KeySignature openingKeySignature, ColorARGB color)
        {
            this.staff = staff;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.length = length;
            this.openingClef = openingClef;
            this.openingKeySignature = openingKeySignature;
            this.color = color;
        }


        public IEnumerable<DrawableLineHorizontal> ConstructStaffLines()
        {
            var lines = new List<DrawableLineHorizontal>();

            for (int i = 0; i < 5; i++)
            {
                double heightOnPage = canvasTop + i * staff.ReadLayout().LineSpacing;

                lines.Add(new DrawableLineHorizontal(heightOnPage, canvasLeft, length, 0.1, color));
            }

            return lines;
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
            var glyphs = new List<DrawableScoreGlyph>();

            if (openingKeySignature.IndexInCircleOfFifths == 0)
            {
                return glyphs;
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

                var yPosition = staff.HeightFromLineIndex(canvasTop, line - 4);

                var glyph = new DrawableScoreGlyph(
                    xPosition,
                    yPosition,
                    _glyph,
                    color);

                glyphs.Add(glyph);
            }

            return glyphs;
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var list = new List<BaseDrawableElement>();

            foreach (var item in ConstructStaffLines())
            {
                list.Add(item);
            }

            foreach (var glyph in ConstructOpeningKeySignature())
            {
                list.Add(glyph);
            }

            list.Add(ConstructOpeningClef());

            return list;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            var elements = new List<BaseContentWrapper>();

            return elements;
        }
    }
}
