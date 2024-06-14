using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual system measure factory.
    /// </summary>
    public class VisualSystemMeasureFactory : IVisualSystemMeasureFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="visualInstrumentMeasureFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <param name="unitToPixelConverter"></param>
        public VisualSystemMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, IScoreDocumentLayout scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter)
        {
            this.selection = selection;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreMeasureReader scoreMeasure, IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double width, double lineSpacing)
        {
            return new VisualSystemMeasure(
                scoreMeasure,
                staffSystem,
                canvasLeft,
                canvasTop,
                width,
                lineSpacing,
                selection,
                visualInstrumentMeasureFactory,
                scoreLayoutDictionary,
                unitToPixelConverter);
        }
    }
}
