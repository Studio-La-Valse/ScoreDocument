namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual rest.
    /// </summary>
    public interface IVisualRestFactory
    {
        /// <summary>
        /// Create the visual rest.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="scoreScale"></param>
        /// <param name="instrumentScale"></param>
        /// <returns></returns>
        BaseContentWrapper Build(IChord element, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale);
    }
}
