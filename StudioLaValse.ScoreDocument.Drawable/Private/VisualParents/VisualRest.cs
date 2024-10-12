using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualRest : BaseVisualNote
    {
        private readonly IChord chord;
        private readonly bool offsetDots;
        private readonly IGlyphLibrary glyphLibrary;

        public Glyph GlyphPrototype
        {
            get
            {
                var duration = 1M;

                var glyphs = new[]
                {
                    glyphLibrary.RestWhole(Scale),
                    glyphLibrary.RestHalf(Scale),
                    glyphLibrary.RestQuarter(Scale),
                    glyphLibrary.RestEighth(Scale),
                    glyphLibrary.RestSixteenth(Scale),
                    glyphLibrary.RestThirtySecond(Scale),
                };

                for (var i = 0; i < 6; i++)
                {
                    if (DisplayDuration.Decimal >= duration)
                    {
                        return glyphs[i];
                    }

                    duration /= 2;
                }

                return glyphs[3];
            }
        }
        public override DrawableScoreGlyph Glyph
        {
            get
            {
                var glyph = GlyphPrototype;

                return new DrawableScoreGlyph(
                    CanvasLeft,
                    CanvasTop,
                    glyph,
                    HorizontalTextOrigin.Center,
                    VerticalTextOrigin.Center,
                    chord.Color.Value.FromPrimitive());
            }
        }

        public override bool OffsetDots => offsetDots;
        public override double Scale => chord.Scale;
        public override ColorARGB Color => chord.Color.Value.FromPrimitive();

        public VisualRest(IChord chord,
                          double canvasLeft,
                          double canvasTop,
                          bool offsetDots,
                          IGlyphLibrary glyphLibrary,
                          ISelection<IUniqueScoreElement> selection) :
            base(chord,
                 canvasLeft,
                 canvasTop,
                 selection)
        {
            this.chord = chord;
            this.offsetDots = offsetDots;
            this.glyphLibrary = glyphLibrary;
        }
    }
}
