using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory for a visual instrument measure.
    /// </summary>
    public interface IVisualInstrumentMeasureFactory
    {
        /// <summary>
        /// Create a visual instrument measure.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="staffGroup"></param>
        /// <param name="canvasTop"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="width"></param>
        /// <param name="paddingLeft"></param>
        /// <param name="paddingRight"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, double lineSpacing, ColorARGB color);
    }
}
