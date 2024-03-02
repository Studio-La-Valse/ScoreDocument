using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="selection"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, ISelection<IUniqueScoreElement> selection, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.selection = selection;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, ColorARGB color)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, systemMeasureFactory, color, selection, scoreLayoutDictionary);
        }
    }
}
