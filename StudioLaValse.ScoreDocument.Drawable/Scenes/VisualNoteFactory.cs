using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.VisualParents;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public class VisualNoteFactory : IVisualNoteFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;

        public VisualNoteFactory(ISelection<IUniqueScoreElement> selection)
        {
            this.selection = selection;
        }

        public BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double scale, ColorARGB color)
        {
            return new VisualNote(note, color, canvasLeft, canvasTop, scale, selection);
        }
    }
}
