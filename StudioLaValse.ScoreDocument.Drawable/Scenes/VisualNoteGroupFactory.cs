using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note group factory.
    /// </summary>
    public class VisualNoteGroupFactory : IVisualNoteGroupFactory
    {
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualBeamBuilder visualBeamBuilder;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="noteFactory"></param>
        /// <param name="restFactory"></param>
        public VisualNoteGroupFactory(IVisualNoteFactory noteFactory, IVisualRestFactory restFactory)
        {
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualBeamBuilder = new VisualBeamBuilder();
        }
        /// <inheritdoc/>
        public BaseContentWrapper Build(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double spacing, ColorARGB colorARGB)
        {
            return new VisualNoteGroup(noteGroup, staffGroup, canvasTopStaffGroup, canvasLeft, spacing, noteFactory, restFactory, visualBeamBuilder, colorARGB);
        }
    }
}
