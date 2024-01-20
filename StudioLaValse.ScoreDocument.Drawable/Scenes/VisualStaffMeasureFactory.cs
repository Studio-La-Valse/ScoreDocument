using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual instrument measure factory.
    /// </summary>
    public class VisualStaffMeasureFactory : IVisualInstrumentMeasureFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IVisualNoteGroupFactory noteGroupFactory;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="noteGroupFactory"></param>
        public VisualStaffMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualNoteGroupFactory noteGroupFactory)
        {
            this.selection = selection;
            this.noteGroupFactory = noteGroupFactory;
        }
        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, ColorARGB color)
        {
            return new VisualStaffGroupMeasure(source, staffGroup, canvasTop, canvasLeft, width, paddingLeft, paddingRight, color, noteGroupFactory, selection);
        }
    }
}
