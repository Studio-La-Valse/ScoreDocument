using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaff : BaseContentWrapper
    {
        private readonly IStaffReader staff;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double length;
        private readonly double globalLineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly double lineThickness;
        private readonly Clef openingClef;
        private readonly KeySignature openingKeySignature;
        private readonly TimeSignature? timeSignature;
        private readonly IScoreDocumentLayout scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        public static double SpaceUntilClef { get; } = 0.5;
        public static double ClefSpacing { get; } = 5.5;
        public static double KeySignatureGlyphSpacing { get; } = 0.9;
        public static double TimeSignatureSpacing { get; } = 4.0;
        public double Scale => scoreScale * instrumentScale;
        public double LineThickness => lineThickness * Scale;


        public VisualStaff(IStaffReader staff,
                           double canvasLeft,
                           double canvasTop,
                           double length,
                           double globalLineSpacing,
                           double scoreScale,
                           double instrumentScale,
                           double lineThickness,
                           Clef openingClef,
                           KeySignature openingKeySignature,
                           TimeSignature? timeSignature,
                           IScoreDocumentLayout scoreDocumentLayout,
                           IUnitToPixelConverter unitToPixelConverter)
        {
            this.staff = staff;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.length = length;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.lineThickness = lineThickness;
            this.openingClef = openingClef;
            this.openingKeySignature = openingKeySignature;
            this.timeSignature = timeSignature;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
        }


        public DrawableScoreGlyph ConstructOpeningClef(double canvasLeft, out double _canvasLeft)
        {
            var glyph = openingClef.ClefSpecies switch
            {
                ClefSpecies.G => GlyphLibrary.ClefG,
                ClefSpecies.F => GlyphLibrary.ClefF,
                ClefSpecies.C => GlyphLibrary.ClefC,
                ClefSpecies.Percussion => GlyphLibrary.ClefPercussion,
                _ => throw new NotImplementedException()
            };

            glyph.Scale = scoreScale * instrumentScale;

            var lineIndex = openingClef.ClefSpecies switch
            {
                ClefSpecies.G => 6,
                ClefSpecies.F => 2,
                _ => throw new NotImplementedException()
            };

            _canvasLeft = canvasLeft + (ClefSpacing * scoreScale);

            return new DrawableScoreGlyph(canvasLeft,
                                          canvasTop + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(lineIndex, globalLineSpacing, scoreScale, instrumentScale)),
                                          glyph,
                                          HorizontalTextOrigin.Left,
                                          VerticalTextOrigin.Center,
                                          scoreDocumentLayout.PageForegroundColor.FromPrimitive());
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
                GlyphLibrary.Flat :
                GlyphLibrary.Sharp;

            var glyphWidth = _glyph.Width;

            var flats = openingKeySignature.DefaultFlats;
            var accidentalLines = flats ?
                openingKeySignature.EnumerateFlatLines(openingClef) :
                openingKeySignature.EnumerateSharpLines(openingClef);


            foreach (var line in accidentalLines)
            {
                _canvasLeft += glyphWidth * KeySignatureGlyphSpacing * scoreScale;

                var yPosition = canvasTop + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(line, globalLineSpacing, scoreScale, instrumentScale));

                DrawableScoreGlyph glyph = new(_canvasLeft, yPosition, _glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, scoreDocumentLayout.PageForegroundColor.FromPrimitive());

                list.Add(glyph);
            }

            _canvasLeft += glyphWidth * KeySignatureGlyphSpacing * scoreScale;
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
                1 => GlyphLibrary.NumberOne,
                2 => GlyphLibrary.NumberTwo,
                3 => GlyphLibrary.NumberThree,
                4 => GlyphLibrary.NumberFour,
                5 => GlyphLibrary.NumberFive,
                6 => GlyphLibrary.NumberSix,
                7 => GlyphLibrary.NumberSeven,
                8 => GlyphLibrary.NumberEight,
                9 => GlyphLibrary.NumberNine,
                _ => throw new NotSupportedException()
            };
            topGlyph.Scale = scoreScale * instrumentScale;

            var bottomGlyph = timeSignature.Denominator.Value switch
            {
                2 => GlyphLibrary.NumberTwo,
                4 => GlyphLibrary.NumberFour,
                8 => GlyphLibrary.NumberEight,
                _ => throw new NotSupportedException()
            };
            bottomGlyph.Scale = scoreScale * instrumentScale;

            var list = new List<DrawableScoreGlyph>()
            {
                new DrawableScoreGlyph(canvasLeft, canvasTop + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(2, globalLineSpacing, scoreScale, instrumentScale)), topGlyph, HorizontalTextOrigin.Left, VerticalTextOrigin.Center, scoreDocumentLayout.PageForegroundColor.FromPrimitive()),
                new DrawableScoreGlyph(canvasLeft, canvasTop + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(6, globalLineSpacing, scoreScale, instrumentScale)), bottomGlyph, HorizontalTextOrigin.Left, VerticalTextOrigin.Center, scoreDocumentLayout.PageForegroundColor.FromPrimitive())
            };

            return list;
        }

        public IEnumerable<DrawableLineHorizontal> ConstructStaffLines()
        {
            var verticalLineThickness = scoreDocumentLayout.VerticalStaffLineThickness * Scale;
            var canvasLeft = this.canvasLeft - (verticalLineThickness / 2);
            var length = this.length + verticalLineThickness;

            for (var i = 0; i < 5; i++)
            {
                var heightOnPage = canvasTop + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(i * 2, globalLineSpacing, scoreScale, instrumentScale));

                yield return new DrawableLineHorizontal(heightOnPage, canvasLeft, length, LineThickness, scoreDocumentLayout.PageForegroundColor.FromPrimitive());
            }
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var canvasLeft = this.canvasLeft + (SpaceUntilClef * scoreScale);

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
