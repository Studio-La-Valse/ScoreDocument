namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory for a visual instrument measure.
    /// </summary>
    public interface IVisualInstrumentMeasureScene
    {
        /// <summary>
        /// Create a visual instrument measure.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="staffGroup"></param>
        /// <param name="positionDictionary"></param>
        /// <param name="canvasTop"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        BaseContentWrapper Create(IInstrumentMeasure source, IStaffGroup staffGroup, PositionDictionary positionDictionary, double canvasTop, double canvasLeft, double width);
    }
}
