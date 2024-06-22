using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffMeasure : BaseContentWrapper
    {
        private readonly IStaffReader staff;
        private readonly Clef openingClef;
        private readonly Clef? invalidatingNextClef;
        private readonly IReadOnlyDictionary<Position, double> positionPositions;
        private readonly double canvasTop;
        private readonly double globalLineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        public double CanvasLeft { get; }
        public double Width { get; }
        public double Scale => scoreScale * instrumentScale;
        public KeySignature? NextMeasureKeySignature { get; }
        public IEnumerable<ClefChange> ClefChanges { get; }


        public ColorARGB ForegroundColor =>
            ColorARGB.Black;
        public IEnumerable<DrawableScoreGlyph> InvalidingNextKeySignature
        {
            get
            {
                if (NextMeasureKeySignature is null)
                {
                    yield break;
                }

                var _glyph = NextMeasureKeySignature.Value.IndexInCircleOfFifths > 0 ?
                    glyphLibrary.Sharp(Scale) :
                    glyphLibrary.Flat(Scale);

                var glyphWidth = _glyph.Width();

                var newLines = NextMeasureKeySignature.Value.DefaultFlats ?
                    NextMeasureKeySignature.Value.EnumerateFlatLines(openingClef) :
                    NextMeasureKeySignature.Value.EnumerateSharpLines(openingClef);

                var naturalLines = newLines
                    .Where(line => !newLines.Contains(line));

                var numberOfAccidentals = newLines.Count() + naturalLines.Count();

                var xPosition = CanvasLeft + Width - ((numberOfAccidentals + 1) * glyphWidth);

                foreach (var line in naturalLines)
                {
                    xPosition += glyphWidth * VisualStaff.KeySignatureGlyphSpacing;

                    var yPosition = HeightFromLineIndex(line);

                    DrawableScoreGlyph glyph = new(
                        xPosition,
                        yPosition,
                        glyphLibrary.Natural(Scale),
                        HorizontalTextOrigin.Center,
                        VerticalTextOrigin.Center,
                        color: ForegroundColor);

                    yield return glyph;
                }

                foreach (var line in newLines)
                {
                    xPosition += glyphWidth * VisualStaff.KeySignatureGlyphSpacing;

                    var yPosition = HeightFromLineIndex(line - 4);

                    DrawableScoreGlyph glyph = new(
                        xPosition,
                        yPosition,
                        _glyph,
                        HorizontalTextOrigin.Center,
                        VerticalTextOrigin.Center,
                        color: ForegroundColor);

                    yield return glyph;
                }
            }
        }
        public IEnumerable<DrawableScoreGlyph> VisualClefChanges
        {
            get
            {
                foreach (var clefchange in ClefChanges)
                {
                    if (clefchange.Position.Decimal == 0)
                    {
                        continue;
                    }

                    var scale = scoreScale * instrumentScale * 0.8;
                    var glyph = clefchange.Clef.ClefSpecies switch
                    {
                        ClefSpecies.C => glyphLibrary.ClefC(scale),
                        ClefSpecies.F => glyphLibrary.ClefF(scale),
                        ClefSpecies.G => glyphLibrary.ClefG(scale),
                        _ => throw new NotSupportedException()
                    };

                    var posX = positionPositions[clefchange.Position] - 0.1;

                    yield return new DrawableScoreGlyph(
                        posX,
                        HeightFromLineIndex(4),
                        glyph,
                        HorizontalTextOrigin.Right,
                        VerticalTextOrigin.Center,
                        ForegroundColor);
                };
            }
        }
        public DrawableScoreGlyph? InvalidatingNextClef
        {
            get
            {
                if (invalidatingNextClef is null)
                {
                    return null;
                }

                var scale = scoreScale * instrumentScale;
                var glyph = invalidatingNextClef.ClefSpecies switch
                {
                    ClefSpecies.C => glyphLibrary.ClefC(scale),
                    ClefSpecies.F => glyphLibrary.ClefF(scale),
                    ClefSpecies.G => glyphLibrary.ClefG(scale),
                    _ => throw new NotSupportedException()
                };

                var posX = CanvasLeft + Width - 0.05 - glyph.Width();


                return new DrawableScoreGlyph(
                    posX,
                    HeightFromLineIndex(4),
                    glyph,
                    HorizontalTextOrigin.Center,
                    VerticalTextOrigin.Center,
                    ForegroundColor);
            }
        }



        public VisualStaffMeasure(
            IStaffReader staff,
            Clef openingClef,
            KeySignature? prepareNext,
            Clef? invalidatingNextClef,
            ClefChange[] clefChanges,
            IReadOnlyDictionary<Position, double> positionPositions,
            double canvasLeft,
            double width,
            double canvasTop,
            double globalLineSpacing,
            double scoreScale,
            double instrumentScale,
            IGlyphLibrary glyphLibrary,
            IUnitToPixelConverter unitToPixelConverter)
        {
            this.staff = staff;
            this.openingClef = openingClef;
            this.invalidatingNextClef = invalidatingNextClef;
            this.canvasTop = canvasTop;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.glyphLibrary = glyphLibrary;
            this.unitToPixelConverter = unitToPixelConverter;
            this.positionPositions = positionPositions;

            NextMeasureKeySignature = prepareNext;
            ClefChanges = clefChanges;
            CanvasLeft = canvasLeft;
            Width = width;
        }



        public double HeightFromLineIndex(int line)
        {
            return canvasTop + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(line, globalLineSpacing, scoreScale, instrumentScale));
        }

        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            if (InvalidatingNextClef != null)
            {
                yield return InvalidatingNextClef;
            }

            foreach (var element in InvalidingNextKeySignature)
            {
                yield return element;
            }

            foreach (var element in VisualClefChanges)
            {
                yield return element;
            }
        }
    }
}
