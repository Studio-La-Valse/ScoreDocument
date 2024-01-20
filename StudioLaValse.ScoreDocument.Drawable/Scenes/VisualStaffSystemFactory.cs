using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly ISelection<IUniqueScoreElement> selection;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="selection"></param>
        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, ISelection<IUniqueScoreElement> selection)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.selection = selection;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, ColorARGB color)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, systemMeasureFactory, color, selection);
        }
    }
}
