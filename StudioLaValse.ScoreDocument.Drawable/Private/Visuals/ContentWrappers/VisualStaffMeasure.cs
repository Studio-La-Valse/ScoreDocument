using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualStaffMeasure : BaseContentWrapper
    {
        private readonly Clef openingClef;
        private readonly KeySignature keySignature;
        private readonly TimeSignature? timeSignature;
        private readonly KeySignature? prepareNext;
        private readonly Clef? invalidatingNextClef;
        private readonly IEnumerable<ClefChange> clefChanges;
        private readonly bool openingMeasure;
        private readonly double canvasLeft;
        private readonly double width;
        private readonly double paddingLeft;
        private readonly double drawableWidth;
        private readonly double canvasTop;
        private readonly double lineSpacing;



        public double CanvasLeft =>
            canvasLeft;
        public double Width =>
            width;
        public ColorARGB ForegroundColor =>
            ColorARGB.Black;
        public KeySignature? NextMeasureKeySignature =>
            prepareNext;
        public IEnumerable<ClefChange> ClefChanges =>
            clefChanges;
        public IEnumerable<DrawableScoreGlyph> InvalidingNextKeySignature
        {
            get
            {
                if (NextMeasureKeySignature is null)
                {
                    yield break;
                }

                var _glyph = NextMeasureKeySignature.IndexInCircleOfFifths > 0 ?
                    GlyphLibrary.Sharp :
                    GlyphLibrary.Flat;

                var glyphWidth = _glyph.Width;

                var newLines = NextMeasureKeySignature.DefaultFlats ?
                    NextMeasureKeySignature.EnumerateFlatLines(openingClef) :
                    NextMeasureKeySignature.EnumerateSharpLines(openingClef);

                var naturalLines = newLines
                    .Where(line => !newLines.Contains(line));

                var numberOfAccidentals = newLines.Count() + naturalLines.Count();

                var xPosition = CanvasLeft + Width - (numberOfAccidentals + 1) * glyphWidth;

                foreach (var line in naturalLines)
                {
                    xPosition += glyphWidth * VisualStaff.KeySignatureGlyphSpacing;

                    var yPosition = HeightFromLineIndex(line - 4);

                    var glyph = new DrawableScoreGlyph(
                        xPosition,
                        yPosition,
                        GlyphLibrary.Natural,
                        color: ForegroundColor);

                    yield return glyph;
                }

                foreach (var line in newLines)
                {
                    xPosition += glyphWidth * VisualStaff.KeySignatureGlyphSpacing;

                    var yPosition = HeightFromLineIndex(line - 4);

                    var glyph = new DrawableScoreGlyph(
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
                var list = new List<DrawableScoreGlyph>();

                if (timeSignature is null)
                {
                    return list;
                }

                var flats = keySignature.DefaultFlats;
                var numberOfAccidentals = flats ?
                    keySignature.EnumerateFlats().Count() :
                    keySignature.EnumerateSharps().Count();
                var left = openingMeasure ?
                    CanvasLeft + numberOfAccidentals * VisualStaff.KeySignatureGlyphSpacing + VisualStaff.ClefSpacing :
                    CanvasLeft + 0.1;

                var topGlyph = timeSignature.Numinator switch
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

                var bottomGlyph = timeSignature.Denominator switch
                {
                    2 => GlyphLibrary.NumberTwo,
                    4 => GlyphLibrary.NumberFour,
                    8 => GlyphLibrary.NumberEight,
                    _ => throw new NotSupportedException()
                };

                list.Add(new DrawableScoreGlyph(left, HeightFromLineIndex(-6), topGlyph, ForegroundColor));

                list.Add(new DrawableScoreGlyph(left, HeightFromLineIndex(-2), bottomGlyph, ForegroundColor));

                return list;
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

                    var posX = XPositionFromParameter((double)clefchange.Position.Decimal) - GlyphLibrary.NoteHeadBlack.Width / 2 - 0.1;

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

                var posX = CanvasLeft + Width - 0.05 - glyph.Width;

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
            this.prepareNext = prepareNext;
            this.invalidatingNextClef = invalidatingNextClef;
            this.clefChanges = clefChanges;
            this.openingMeasure = openingMeasure;
            this.canvasLeft = canvasLeft;
            this.width = width;
            this.paddingLeft = paddingLeft;
            this.drawableWidth = drawableWidth;
            this.canvasTop = canvasTop;
            this.lineSpacing = lineSpacing;
        }




        public double XPositionFromParameter(double parameter) =>
            canvasLeft + paddingLeft + drawableWidth * parameter;
        public double HeightFromLineIndex(int line) =>
            canvasTop + line * (lineSpacing / 2);
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            var content = new List<BaseContentWrapper>();

            return content;
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var elements = new List<BaseDrawableElement>();

            foreach (var glyph in OpeningTimeSignature)
            {
                elements.Add(glyph);
            }

            if (InvalidatingNextClef != null)
            {
                elements.Add(InvalidatingNextClef);
            }

            elements.AddRange(InvalidingNextKeySignature);
            elements.AddRange(VisualClefChanges);
            return elements;
        }
    }
}
