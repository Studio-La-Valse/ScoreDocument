using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual rest factory.
    /// </summary>
    public class VisualRestFactory : IVisualRestFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        public VisualRestFactory(ISelection<IUniqueScoreElement> selection)
        {
            this.selection = selection;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Build(IChord note, double canvasLeft, double canvasTop, double scale, ColorARGB color)
        {
            return new VisualRest(note, canvasLeft, canvasTop, scale, color, selection);
        }
    }
}
