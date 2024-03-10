using StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaff : BaseContentWrapper
    {
        private readonly IStaffReader staff;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double length;
        private readonly double lineSpacing;
        private readonly Clef openingClef;
        private readonly KeySignature openingKeySignature;
        private readonly ColorARGB color;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;


        public StaffLayout Layout => scoreLayoutDictionary.StaffLayout(staff);
        public static double KeySignatureGlyphSpacing { get; set; } = 0.9;
        public static double ClefSpacing { get; set; } = 5.5;
        public static double KeySignatureSpacing { get; set; } = 4.5;








        public VisualStaff(IStaffReader staff, double canvasLeft, double canvasTop, double length, double lineSpacing, Clef openingClef, KeySignature openingKeySignature, ColorARGB color, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.staff = staff;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.length = length;
            this.lineSpacing = lineSpacing;
            this.openingClef = openingClef;
            this.openingKeySignature = openingKeySignature;
            this.color = color;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }


        public IEnumerable<DrawableLineHorizontal> ConstructStaffLines()
        {
            for (int i = 0; i < 5; i++)
            {
                double heightOnPage = canvasTop + (i * lineSpacing);

                yield return new DrawableLineHorizontal(heightOnPage, canvasLeft, length, 0.1, color);
            }
        }
        public DrawableScoreGlyph ConstructOpeningClef()
        {
            Glyph glyph = openingClef.ClefSpecies switch
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

            double xPosition = canvasLeft + 3.5;

            Glyph _glyph = openingKeySignature.IndexInCircleOfFifths > 0 ?
                GlyphLibrary.Sharp :
                GlyphLibrary.Flat;

            double glyphWidth = _glyph.Width;

            bool flats = openingKeySignature.DefaultFlats;
            IEnumerable<int> accidentalLines = flats ?
                openingKeySignature.EnumerateFlatLines(openingClef) :
                openingKeySignature.EnumerateSharpLines(openingClef);
            foreach (int line in accidentalLines)
            {
                xPosition += glyphWidth * KeySignatureGlyphSpacing;

                double yPosition = staff.HeightFromLineIndex(canvasTop, line - 4, lineSpacing);

                DrawableScoreGlyph glyph = new(
                    xPosition,
                    yPosition,
                    _glyph,
                    color);

                yield return glyph;
            }
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            foreach (DrawableLineHorizontal item in ConstructStaffLines())
            {
                yield return item;
            }

            foreach (DrawableScoreGlyph glyph in ConstructOpeningKeySignature())
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
