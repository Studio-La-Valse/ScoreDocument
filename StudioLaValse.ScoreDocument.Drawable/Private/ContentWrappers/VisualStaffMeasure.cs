using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffMeasure : BaseContentWrapper
    {
        private readonly IStaffReader staff;
        private readonly Clef openingClef;
        private readonly Clef? invalidatingNextClef;
        private readonly double paddingLeft;
        private readonly double drawableWidth;
        private readonly double canvasTop;
        private readonly double globalLineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        public double CanvasLeft { get; }
        public double Width { get; }
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
                    GlyphLibrary.Sharp :
                    GlyphLibrary.Flat;

                var glyphWidth = _glyph.Width;

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
                        GlyphLibrary.Natural,
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

                    var glyph = clefchange.Clef.ClefSpecies switch
                    {
                        ClefSpecies.C => GlyphLibrary.ClefC,
                        ClefSpecies.F => GlyphLibrary.ClefF,
                        ClefSpecies.G => GlyphLibrary.ClefG,
                        _ => throw new NotSupportedException()
                    };
                    glyph.Scale = scoreScale * instrumentScale;

                    var posX = XPositionFromParameter((double)clefchange.Position.Decimal) - (GlyphLibrary.NoteHeadBlack.Width / 2) - 0.1;

                    glyph.Scale = 0.8;

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

                var glyph = invalidatingNextClef.ClefSpecies switch
                {
                    ClefSpecies.C => GlyphLibrary.ClefC,
                    ClefSpecies.F => GlyphLibrary.ClefF,
                    ClefSpecies.G => GlyphLibrary.ClefG,
                    _ => throw new NotSupportedException()
                };
                glyph.Scale = scoreScale * instrumentScale;

                var posX = CanvasLeft + Width - 0.05 - glyph.Width;


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
            IEnumerable<ClefChange> clefChanges,
            double canvasLeft,
            double width,
            double paddingLeft,
            double drawableWidth,
            double canvasTop,
            double globalLineSpacing,
            double scoreScale,
            double instrumentScale,
            IUnitToPixelConverter unitToPixelConverter)
        {
            this.staff = staff;
            this.openingClef = openingClef;
            this.invalidatingNextClef = invalidatingNextClef;
            this.paddingLeft = paddingLeft;
            this.drawableWidth = drawableWidth;
            this.canvasTop = canvasTop;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.unitToPixelConverter = unitToPixelConverter;

            NextMeasureKeySignature = prepareNext;
            ClefChanges = clefChanges;
            CanvasLeft = canvasLeft;
            Width = width;
        }




        public double XPositionFromParameter(double parameter)
        {
            return CanvasLeft + paddingLeft + (drawableWidth * parameter);
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
