using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual score measure.
    /// </summary>
    public interface IVisualSystemMeasureFactory
    {
        /// <summary>
        /// Create the visual score measure.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <param name="staffSystem"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <param name="width"></param>
        /// <param name="firstMeasure"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IScoreMeasureReader scoreMeasure, IStaffSystem staffSystem, double canvasLeft, double canvasTop, double width, bool firstMeasure, ColorARGB color);
    }
}
