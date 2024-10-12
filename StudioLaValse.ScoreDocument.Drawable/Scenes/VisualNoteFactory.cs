using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note factory.
    /// </summary>
    public class VisualNoteFactory : IVisualNoteFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="glyphLibrary"></param>
        public VisualNoteFactory(ISelection<IUniqueScoreElement> selection, IGlyphLibrary glyphLibrary)
        {
            this.selection = selection;
            this.glyphLibrary = glyphLibrary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Build(INote note, Clef clef, Accidental? accidental, double canvasLeft, double canvasTop)
        {
            var lineIndex = clef.LineIndexAtPitch(note.Pitch);
            var offsetDots = lineIndex % 2 == 0;

            return new VisualNote(note, canvasLeft, canvasTop, offsetDots, accidental, glyphLibrary, selection);
        }
    }
}
