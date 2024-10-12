using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaff : BaseContentWrapper
    {
        private readonly IStaff staff;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double length;
        private readonly Clef openingClef;
        private readonly KeySignature openingKeySignature;
        private readonly TimeSignature? timeSignature;
        private readonly IGlyphLibrary glyphLibrary;

        public static double SpaceUntilClef { get; } = 1;
        public static double ClefSpacing { get; } = 6;
        public static double KeySignatureGlyphSpacing { get; } = 1.8;
        public static double TimeSignatureSpacing { get; } = 8;
        public double Scale => staff.Scale;
        public double HorizontalLineThickness => staff.HorizontalStaffLineThickness * Scale;
        public double VerticalLineThickness => staff.VerticalStaffLineThickness * Scale;

        public VisualStaff(IStaff staff,
                           double canvasLeft,
                           double canvasTop,
                           double length,
                           Clef openingClef,
                           KeySignature openingKeySignature,
                           TimeSignature? timeSignature,
                           IGlyphLibrary glyphLibrary)
        {
            this.staff = staff;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.length = length;
            this.openingClef = openingClef;
            this.openingKeySignature = openingKeySignature;
            this.timeSignature = timeSignature;
            this.glyphLibrary = glyphLibrary;
        }


        public DrawableScoreGlyph ConstructOpeningClef(double canvasLeft, out double _canvasLeft)
        {
            var glyph = openingClef.ClefSpecies switch
            {
                ClefSpecies.G => glyphLibrary.ClefG(Scale),
                ClefSpecies.F => glyphLibrary.ClefF(Scale),
                ClefSpecies.C => glyphLibrary.ClefC(Scale),
                ClefSpecies.Percussion => glyphLibrary.ClefPercussion(Scale),
                _ => throw new NotImplementedException()
            };

            var lineIndex = openingClef.ClefSpecies switch
            {
                ClefSpecies.G => 6,
                ClefSpecies.F => 2,
                _ => throw new NotImplementedException()
            };

            _canvasLeft = canvasLeft + (ClefSpacing * Scale);

            return new DrawableScoreGlyph(canvasLeft,
                                          canvasTop + staff.DistanceFromTop(lineIndex),
                                          glyph,
                                          HorizontalTextOrigin.Left,
                                          VerticalTextOrigin.Center,
                                          staff.Color.Value.FromPrimitive());
        }
        public IEnumerable<DrawableScoreGlyph> ConstructOpeningKeySignature(double canvasLeft, out double _canvasLeft)
        {
            _canvasLeft = canvasLeft;
            var list = new List<DrawableScoreGlyph>();
            if (openingKeySignature.IndexInCircleOfFifths == 0)
            {
                return list;
            }

            var _glyph = openingKeySignature.DefaultFlats ?
                glyphLibrary.Flat(Scale) :
                glyphLibrary.Sharp(Scale);

            var glyphWidth = _glyph.Width();

            var flats = openingKeySignature.DefaultFlats;
            var accidentalLines = flats ?
                openingKeySignature.EnumerateFlatLines(openingClef) :
                openingKeySignature.EnumerateSharpLines(openingClef);


            foreach (var line in accidentalLines)
            {
                _canvasLeft += glyphWidth * KeySignatureGlyphSpacing * staff.Scale;

                var yPosition = canvasTop + staff.DistanceFromTop(line);

                DrawableScoreGlyph glyph = new(_canvasLeft, yPosition, _glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, staff.Color.Value.FromPrimitive());

                list.Add(glyph);
            }

            _canvasLeft += glyphWidth * KeySignatureGlyphSpacing * staff.Scale;
            return list;
        }
        public IEnumerable<DrawableScoreGlyph> ConstructOpeningTimeSignature(double canvasLeft)
        {
            if (timeSignature is null)
            {
                return new List<DrawableScoreGlyph>();
            }

            var topGlyph = timeSignature.Numerator switch
            {
                1 => glyphLibrary.NumberOne(Scale),
                2 => glyphLibrary.NumberTwo(Scale),
                3 => glyphLibrary.NumberThree(Scale),
                4 => glyphLibrary.NumberFour(Scale),
                5 => glyphLibrary.NumberFive(Scale),
                6 => glyphLibrary.NumberSix(Scale),
                7 => glyphLibrary.NumberSeven(Scale),
                8 => glyphLibrary.NumberEight(Scale),
                9 => glyphLibrary.NumberNine(Scale),
                _ => throw new NotSupportedException()
            };

            var bottomGlyph = timeSignature.Denominator.Value switch
            {
                2 => glyphLibrary.NumberTwo(Scale),
                4 => glyphLibrary.NumberFour(Scale),
                8 => glyphLibrary.NumberEight(Scale),
                _ => throw new NotSupportedException()
            };

            var list = new List<DrawableScoreGlyph>()
            {
                new DrawableScoreGlyph(canvasLeft, canvasTop + staff.DistanceFromTop(2), topGlyph, HorizontalTextOrigin.Left, VerticalTextOrigin.Center, staff.Color.Value.FromPrimitive()),
                new DrawableScoreGlyph(canvasLeft, canvasTop + staff.DistanceFromTop(6), bottomGlyph, HorizontalTextOrigin.Left, VerticalTextOrigin.Center, staff.Color.Value.FromPrimitive())
            };

            return list;
        }

        public IEnumerable<DrawableLineHorizontal> ConstructStaffLines()
        {
            var verticalLineThickness = staff.VerticalStaffLineThickness * Scale;
            var canvasLeft = this.canvasLeft - (verticalLineThickness / 2);
            var length = this.length + verticalLineThickness;

            for (var i = 0; i < 5; i++)
            {
                var heightOnPage = canvasTop + staff.DistanceFromTop(i * 2);

                yield return new DrawableLineHorizontal(heightOnPage, canvasLeft, length, HorizontalLineThickness, staff.Color.Value.FromPrimitive());
            }
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var canvasLeft = this.canvasLeft + (SpaceUntilClef * staff.Scale);

            yield return ConstructOpeningClef(canvasLeft, out canvasLeft);

            foreach (var glyph in ConstructOpeningKeySignature(canvasLeft, out canvasLeft))
            {
                yield return glyph;
            }

            foreach (var glyph in ConstructOpeningTimeSignature(canvasLeft))
            {
                yield return glyph;
            }

            foreach (var item in ConstructStaffLines())
            {
                yield return item;
            }
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
    }
}
