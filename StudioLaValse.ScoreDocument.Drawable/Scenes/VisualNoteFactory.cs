using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;

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
        public BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double scale, ColorARGB color)
        {
            return new VisualNote(note, color, canvasLeft, canvasTop, scale, selection);
        }
    }
}
