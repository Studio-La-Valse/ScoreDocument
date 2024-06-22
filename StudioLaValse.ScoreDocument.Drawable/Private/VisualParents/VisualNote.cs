using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualNote : BaseVisualNote
    {
        private readonly INoteReader note;
        private readonly double canvasTop;
        private readonly bool offsetDots;
        private readonly Accidental? accidental;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IScoreDocumentLayout scoreDocumentLayout;


        public INoteLayout NoteLayout => note.ReadLayout();
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

                return new DrawableScoreGlyph(XPosition, canvasTop, glyph, HorizontalTextOrigin.Center, VerticalTextOrigin.Center, scoreDocumentLayout.PageForegroundColor.FromPrimitive());
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
                        XPosition - (glyph.Width() * 2),
                        canvasTop,
                        glyph,
                        HorizontalTextOrigin.Center,
                        VerticalTextOrigin.Center,
                        scoreDocumentLayout.PageForegroundColor.FromPrimitive());
                }
                return null;
            }
        }

        public override bool OffsetDots => offsetDots;
        public override double XOffset => NoteLayout.XOffset;

        public VisualNote(INoteReader note,
                          double canvasLeft,
                          double canvasTop,
                          double lineSpacing,
                          double scoreScale,
                          double instrumentScale,
                          double noteScale,
                          bool offsetDots,
                          Accidental? accidental,
                          IGlyphLibrary glyphLibrary,
                          IScoreDocumentLayout scoreDocumentLayout,
                          ISelection<IUniqueScoreElement> selection,
                          IUnitToPixelConverter unitToPixelConverter) :
            base(note,
                 canvasLeft,
                 canvasTop,
                 lineSpacing,
                 scoreScale,
                 instrumentScale,
                 noteScale,
                 scoreDocumentLayout,
                 selection,
                 unitToPixelConverter)
        {
            this.note = note;
            this.canvasTop = canvasTop;
            this.offsetDots = offsetDots;
            this.accidental = accidental;
            this.glyphLibrary = glyphLibrary;
            this.scoreDocumentLayout = scoreDocumentLayout;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return AccidentalGlyph == null ? base.GetDrawableElements() : base.GetDrawableElements().Append(AccidentalGlyph);
        }
    }
}
