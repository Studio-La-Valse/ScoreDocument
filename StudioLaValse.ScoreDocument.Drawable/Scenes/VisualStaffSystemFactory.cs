using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <param name="unitToPixelConverter"></param>
        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, IScoreDocumentLayout scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, double lineSpacing)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, lineSpacing, systemMeasureFactory, scoreLayoutDictionary, unitToPixelConverter);
        }
    }
}
