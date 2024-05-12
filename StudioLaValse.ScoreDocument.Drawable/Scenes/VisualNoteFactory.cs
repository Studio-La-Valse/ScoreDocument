using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note factory.
    /// </summary>
    public class VisualNoteFactory : IVisualNoteFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        public VisualNoteFactory(ISelection<IUniqueScoreElement> selection)
        {
            this.selection = selection;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, bool offsetDots, Accidental? accidental, ColorARGB color)
        {
            var noteScale = note.ReadLayout().Scale;
            return new VisualNote(note, color, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, noteScale, offsetDots, accidental, selection);
        }
    }
}
