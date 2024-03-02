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
        /// <param name="scale"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        BaseContentWrapper Build(IChordReader element, double canvasLeft, double canvasTop, double scale, ColorARGB color);
    }
}
