using StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStaffMeasure : BaseContentWrapper
    {
        private readonly Clef openingClef;
        private readonly KeySignature keySignature;
        private readonly TimeSignature? timeSignature;
        private readonly Clef? invalidatingNextClef;
        private readonly bool openingMeasure;
        private readonly double paddingLeft;
        private readonly double drawableWidth;
        private readonly double canvasTop;
        private readonly double lineSpacing;



        public double CanvasLeft { get; }
        public double Width { get; }
        public ColorARGB ForegroundColor =>
            ColorARGB.Black;
        public KeySignature? NextMeasureKeySignature { get; }
        public IEnumerable<ClefChange> ClefChanges { get; }
        public IEnumerable<DrawableScoreGlyph> InvalidingNextKeySignature
        {
            get
            {
                if (NextMeasureKeySignature is null)
                {
                    yield break;
                }

                Glyph _glyph = NextMeasureKeySignature.Value.IndexInCircleOfFifths > 0 ?
                    GlyphLibrary.Sharp :
                    GlyphLibrary.Flat;

                double glyphWidth = _glyph.Width;

                IEnumerable<int> newLines = NextMeasureKeySignature.Value.DefaultFlats ?
                    NextMeasureKeySignature.Value.EnumerateFlatLines(openingClef) :
                    NextMeasureKeySignature.Value.EnumerateSharpLines(openingClef);

                IEnumerable<int> naturalLines = newLines
                    .Where(line => !newLines.Contains(line));

                int numberOfAccidentals = newLines.Count() + naturalLines.Count();

                double xPosition = CanvasLeft + Width - (numberOfAccidentals + 1) * glyphWidth;

                foreach (int line in naturalLines)
                {
                    xPosition += glyphWidth * VisualStaff.KeySignatureGlyphSpacing;

                    double yPosition = HeightFromLineIndex(line - 4);

                    DrawableScoreGlyph glyph = new(
                        xPosition,
                        yPosition,
                        GlyphLibrary.Natural,
                        color: ForegroundColor);

                    yield return glyph;
                }

                foreach (int line in newLines)
                {
                    xPosition += glyphWidth * VisualStaff.KeySignatureGlyphSpacing;

                    double yPosition = HeightFromLineIndex(line - 4);

                    DrawableScoreGlyph glyph = new(
                        xPosition,
                        yPosition,
                        _glyph,
                        color: ForegroundColor);

                    yield return glyph;
                }
            }
        }
        public IEnumerable<DrawableScoreGlyph> OpeningTimeSignature
        {
            get
            {
                if (timeSignature is null)
                {
                    yield break;
                }

                bool flats = keySignature.DefaultFlats;
                int numberOfAccidentals = flats ?
                    keySignature.EnumerateFlats().Count() :
                    keySignature.EnumerateSharps().Count();
                double left = openingMeasure ?
                    CanvasLeft + numberOfAccidentals * VisualStaff.KeySignatureGlyphSpacing + VisualStaff.ClefSpacing :
                    CanvasLeft + 0.1;

                Glyph topGlyph = timeSignature.Numerator switch
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

                Glyph bottomGlyph = timeSignature.Denominator.Value switch
                {
                    2 => GlyphLibrary.NumberTwo,
                    4 => GlyphLibrary.NumberFour,
                    8 => GlyphLibrary.NumberEight,
                    _ => throw new NotSupportedException()
                };

                yield return new DrawableScoreGlyph(left, HeightFromLineIndex(-6), topGlyph, ForegroundColor);

                yield return new DrawableScoreGlyph(left, HeightFromLineIndex(-2), bottomGlyph, ForegroundColor);
            }
        }
        public IEnumerable<DrawableScoreGlyph> VisualClefChanges
        {
            get
            {
                foreach (ClefChange clefchange in ClefChanges)
                {
                    if (clefchange.Position.Decimal == 0)
                    {
                        continue;
                    }

                    Glyph glyph = clefchange.Clef.ClefSpecies switch
                    {
                        ClefSpecies.C => GlyphLibrary.ClefC,
                        ClefSpecies.F => GlyphLibrary.ClefF,
                        ClefSpecies.G => GlyphLibrary.ClefG,
                        _ => throw new NotSupportedException()
                    };

                    double posX = XPositionFromParameter((double)clefchange.Position.Decimal) - GlyphLibrary.NoteHeadBlack.Width / 2 - 0.1;

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

                Glyph glyph = invalidatingNextClef.ClefSpecies switch
                {
                    ClefSpecies.C => GlyphLibrary.ClefC,
                    ClefSpecies.F => GlyphLibrary.ClefF,
                    ClefSpecies.G => GlyphLibrary.ClefG,
                    _ => throw new NotSupportedException()
                };

                double posX = CanvasLeft + Width - 0.05 - glyph.Width;

                glyph.Scale = 0.8;

                return new DrawableScoreGlyph(
                    posX,
                    HeightFromLineIndex(0),
                    glyph,
                    ForegroundColor);
            }
        }



        public VisualStaffMeasure(
            Clef openingClef,
            KeySignature keySignature,
            TimeSignature? timeSignature,
            KeySignature? prepareNext,
            Clef? invalidatingNextClef,
            IEnumerable<ClefChange> clefChanges,
            bool openingMeasure,
            double canvasLeft,
            double width,
            double paddingLeft,
            double drawableWidth,
            double canvasTop,
            double lineSpacing)
        {
            this.keySignature = keySignature;
            this.openingClef = openingClef;
            this.timeSignature = timeSignature;
            NextMeasureKeySignature = prepareNext;
            this.invalidatingNextClef = invalidatingNextClef;
            ClefChanges = clefChanges;
            this.openingMeasure = openingMeasure;
            CanvasLeft = canvasLeft;
            Width = width;
            this.paddingLeft = paddingLeft;
            this.drawableWidth = drawableWidth;
            this.canvasTop = canvasTop;
            this.lineSpacing = lineSpacing;
        }




        public double XPositionFromParameter(double parameter)
        {
            return CanvasLeft + paddingLeft + drawableWidth * parameter;
        }

        public double HeightFromLineIndex(int line)
        {
            return canvasTop + line * (lineSpacing / 2);
        }

        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            List<BaseContentWrapper> content = [];

            return content;
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            foreach (DrawableScoreGlyph glyph in OpeningTimeSignature)
            {
                yield return glyph;
            }

            if (InvalidatingNextClef != null)
            {
                yield return InvalidatingNextClef;
            }

            foreach (DrawableScoreGlyph element in InvalidingNextKeySignature)
            {
                yield return element;
            }

            foreach (DrawableScoreGlyph element in VisualClefChanges)
            {
                yield return element;
            }
        }
    }
}
