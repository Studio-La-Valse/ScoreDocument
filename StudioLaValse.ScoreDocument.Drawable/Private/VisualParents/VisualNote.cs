using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualNote : BaseVisualNote
    {
        private readonly INoteReader note;
        private readonly ColorARGB color;
        private readonly double canvasTop;
        private readonly bool offsetDots;
        private readonly Accidental? accidental;
        private readonly ISelection<IUniqueScoreElement> selection;


        public INoteLayout NoteLayout => note.ReadLayout();
        public override DrawableScoreGlyph Glyph
        {
            get
            {
                var glyph = DisplayDuration.PowerOfTwo.Value switch
                {
                    1 => GlyphLibrary.NoteHeadWhole,
                    2 => GlyphLibrary.NoteHeadWhite,
                    _ => GlyphLibrary.NoteHeadBlack
                };
                glyph.Scale = Scale;

                return new DrawableScoreGlyph(XPosition, canvasTop, glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, color);
            }
        }
        public DrawableScoreGlyph? AccidentalGlyph
        {
            get
            {
                var glyph = accidental switch
                {
                    Accidental.DoubleFlat => GlyphLibrary.DoubleFlat,
                    Accidental.Flat => GlyphLibrary.Flat,
                    Accidental.Natural => GlyphLibrary.Natural,
                    Accidental.Sharp => GlyphLibrary.Sharp,
                    Accidental.DoubleSharp => GlyphLibrary.DoubleSharp,
                    null => null,
                    _ => throw new NotImplementedException(),
                };

                if (glyph is not null)
                {
                    glyph.Scale = Scale;
                    return new DrawableScoreGlyph(
                        XPosition - (glyph.Width * 2),
                        canvasTop,
                        glyph,
                        HorizontalTextOrigin.Center,
                        VerticalTextOrigin.Center,
                        DisplayColor);
                }
                return null;
            }
        }

        public override bool OffsetDots => offsetDots;
        public override double XOffset => NoteLayout.XOffset;

        public VisualNote(INoteReader note, ColorARGB color, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, double noteScale, bool offsetDots, Accidental? accidental, ISelection<IUniqueScoreElement> selection) :
            base(note, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, noteScale, color, selection)
        {
            this.note = note;
            this.color = color;
            this.canvasTop = canvasTop;
            this.offsetDots = offsetDots;
            this.accidental = accidental;
            this.selection = selection;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return AccidentalGlyph == null ? base.GetDrawableElements() : base.GetDrawableElements().Append(AccidentalGlyph);
        }
    }
}
