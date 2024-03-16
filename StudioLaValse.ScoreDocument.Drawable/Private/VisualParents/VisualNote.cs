using StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualNote : BaseVisualNote
    {
        private readonly INoteReader note;
        private readonly ColorARGB color;
        private readonly double canvasTop;
        private readonly bool offsetDots;
        private readonly Accidental? accidental;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;


        public NoteLayout NoteLayout => scoreLayoutDictionary.NoteLayout(note);
        public override DrawableScoreGlyph Glyph
        {
            get
            {
                Glyph glyph = GlyphPrototype;
                return new DrawableScoreGlyph(XPosition, canvasTop, glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, color);
            }
        }
        public DrawableScoreGlyph? AccidentalGlyph
        {
            get
            {
                Glyph? glyph = accidental switch
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
                        XPosition - glyph.Width * 2,
                        canvasTop,
                        glyph,
                        HorizontalTextOrigin.Center,
                        VerticalTextOrigin.Center,
                        DisplayColor);
                }
                return null;
            }
        }
        public Glyph GlyphPrototype
        {
            get
            {
                Glyph glyph = DisplayDuration.Decimal switch
                {
                    1M => GlyphLibrary.NoteHeadWhole,
                    0.5M => GlyphLibrary.NoteHeadWhite,
                    _ => GlyphLibrary.NoteHeadBlack
                };

                glyph.Scale = Scale;

                return glyph;
            }
        }

        public override bool OffsetDots => offsetDots;
        public override double XOffset => NoteLayout.XOffset;

        public VisualNote(INoteReader note, ColorARGB color, double canvasLeft, double canvasTop, double scale, bool offsetDots, Accidental? accidental, ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary) :
            base(note, canvasLeft, canvasTop, scale, color, selection)
        {
            this.note = note;
            this.color = color;
            this.canvasTop = canvasTop;
            this.offsetDots = offsetDots;
            this.accidental = accidental;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return AccidentalGlyph == null ? base.GetDrawableElements() : base.GetDrawableElements().Append(AccidentalGlyph);
        }
    }
}
