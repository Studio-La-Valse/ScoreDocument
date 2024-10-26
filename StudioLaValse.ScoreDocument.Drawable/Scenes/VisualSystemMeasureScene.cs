namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual system measure factory.
    /// </summary>
    public class VisualSystemMeasureScene : IVisualSystemMeasureScene
    {
        private readonly IVisualInstrumentMeasureScene visualInstrumentMeasureFactory;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="visualInstrumentMeasureFactory"></param>
        public VisualSystemMeasureScene(IVisualInstrumentMeasureScene visualInstrumentMeasureFactory)
        {
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IScoreMeasure scoreMeasure, IStaffSystem staffSystem, double canvasLeft, double canvasTop, double width)
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
