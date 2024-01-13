using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Drawable.Text;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Models;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.VisualParents
{
    public sealed class VisualNote : BaseVisualNote
    {
        private readonly INoteReader note;
        private readonly ColorARGB color;
        private readonly double canvasLeft;
        private readonly double canvasTop;


        public override int Line =>
            Clef.LineIndexAtPitch(Pitch);
        public override DrawableScoreGlyph Glyph
        {
            get
            {
                var glyph = GlyphPrototype;
                return new DrawableScoreGlyph(canvasLeft, canvasTop, glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, color);
            }
        }
        public DrawableScoreGlyph? AccidentalGlyph
        {
            get
            {
                var accidental = note.GetAccidental();
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
        public Clef Clef =>
            note.GetClef();
        public Pitch Pitch =>
            note.Pitch;
        public Glyph GlyphPrototype
        {
            get
            {
                var glyph = ActualDuration.Decimal switch
                {
                    1M => GlyphLibrary.NoteHeadWhole,
                    0.5M => GlyphLibrary.NoteHeadWhite,
                    _ => GlyphLibrary.NoteHeadBlack
                };

                glyph.Scale = Scale;

                return glyph;
            }
        }



        public VisualNote(INoteReader note, ColorARGB color, double canvasLeft, double canvasTop, double scale, ISelection<IUniqueScoreElement> selection) :
            base(note, canvasLeft, canvasTop, scale, color, selection)
        {
            this.note = note;
            this.color = color;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            if (AccidentalGlyph == null)
            {
                return base.GetDrawableElements();
            }

            return base.GetDrawableElements().Append(AccidentalGlyph);
        }
    }
}
