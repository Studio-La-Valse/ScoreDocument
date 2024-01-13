using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.VisualParents;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public class VisualRestFactory : IVisualRestFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;

        public VisualRestFactory(ISelection<IUniqueScoreElement> selection)
        {
            this.selection = selection;
        }

        public BaseContentWrapper Build(IChord note, double canvasLeft, double canvasTop, double scale, ColorARGB color)
        {
            return new VisualRest(note, canvasLeft, canvasTop, scale, color, selection);
        }
    }
}
