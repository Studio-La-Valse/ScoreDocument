using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    /// <summary>
    /// A note mirror, to specify stem alignment. 
    /// </summary>
    internal enum NoteMirror
    {
        /// <summary>
        /// Don't mirror flip the note.
        /// </summary>
        NoMirror,
        /// <summary>
        /// Move the note from the right side of the stem to the left.
        /// </summary>
        Left,
        /// <summary>
        /// Move the note from the left side of the stem to the right.
        /// </summary>
        Right
    }
    internal sealed class VisualNote : BaseVisualNote
    {
        private readonly INote note;
        private readonly double canvasTop;
        private readonly bool offsetDots;
        private readonly Accidental? accidental;
        private readonly IGlyphLibrary glyphLibrary;


        public override DrawableScoreGlyph Glyph
        {
            get
            {
                var glyph = DisplayDuration.PowerOfTwo.Value switch
                {
                    1 => glyphLibrary.NoteHeadWhole(Scale),
                    2 => glyphLibrary.NoteHeadWhite(Scale),
                    _ => glyphLibrary.NoteHeadBlack(Scale)
                };
                var canvasLeft = CanvasLeft;

                return new DrawableScoreGlyph(canvasLeft, canvasTop, glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, note.Color.Value.FromPrimitive());
            }
        }
        public DrawableScoreGlyph? AccidentalGlyph
        {
            get
            {
                var glyph = accidental switch
                {
                    Accidental.DoubleFlat => glyphLibrary.DoubleFlat(Scale),
                    Accidental.Flat => glyphLibrary.Flat(Scale),
                    Accidental.Natural => glyphLibrary.Natural(Scale),
                    Accidental.Sharp => glyphLibrary.Sharp(Scale),
                    Accidental.DoubleSharp => glyphLibrary.DoubleSharp(Scale),
                    null => null,
                    _ => throw new NotImplementedException(),
                };

                if (glyph is not null)
                {
                    return new DrawableScoreGlyph(
                        CanvasLeft - (glyph.Width() * 2),
                        canvasTop,
                        glyph,
                        HorizontalTextOrigin.Center,
                        VerticalTextOrigin.Center,
                        note.Color.Value.FromPrimitive());
                }
                return null;
            }
        }

        public override bool OffsetDots => offsetDots;
        public override double Scale => note.Scale;
        public override ColorARGB Color => note.Color.Value.FromPrimitive();

        public VisualNote(INote note,
                          double canvasLeft,
                          double canvasTop,
                          bool offsetDots,
                          Accidental? accidental,
                          IGlyphLibrary glyphLibrary,
                          ISelection<IUniqueScoreElement> selection) :
            base(note,
                 canvasLeft,
                 canvasTop,
                 selection)
        {
            this.note = note;
            this.canvasTop = canvasTop;
            this.offsetDots = offsetDots;
            this.accidental = accidental;
            this.glyphLibrary = glyphLibrary;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return AccidentalGlyph == null ? base.GetDrawableElements() : base.GetDrawableElements().Append(AccidentalGlyph);
        }
    }
}
