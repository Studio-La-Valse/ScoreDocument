using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual system measure factory.
    /// </summary>
    public class VisualSystemMeasureFactory : IVisualSystemMeasureFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="visualInstrumentMeasureFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualSystemMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.selection = selection;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreMeasureReader scoreMeasure, IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double width, bool firstMeasure, ColorARGB colorARGB)
        {
            return new VisualSystemMeasure(scoreMeasure, staffSystem, canvasLeft, canvasTop, width, firstMeasure, colorARGB, selection, visualInstrumentMeasureFactory, scoreLayoutDictionary);
        }
    }
}
