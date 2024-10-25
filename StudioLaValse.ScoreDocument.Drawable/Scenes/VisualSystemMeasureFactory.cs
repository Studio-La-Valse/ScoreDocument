namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual system measure factory.
    /// </summary>
    public class VisualSystemMeasureFactory : IVisualSystemMeasureFactory
    {
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="visualInstrumentMeasureFactory"></param>
        public VisualSystemMeasureFactory(IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory)
        {
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreMeasure scoreMeasure, IStaffSystem staffSystem, double canvasLeft, double canvasTop, double width)
        {
            return new VisualSystemMeasure(
                scoreMeasure,
                staffSystem,
                canvasLeft,
                canvasTop,
                width,
                visualInstrumentMeasureFactory);
        }
    }
}
