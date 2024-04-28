using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual note group.
    /// </summary>
    public interface IVisualNoteGroupFactory
    {
        /// <summary>
        /// Build the visual note group.
        /// </summary>
        /// <param name="noteGroup"></param>
        /// <param name="staffGroup"></param>
        /// <param name="instrumentMeasure"></param>
        /// <param name="canvasTopStaffGroup"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="allowedSpace"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="colorARGB"></param>
        /// <returns></returns>
        BaseContentWrapper Build(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, IInstrumentMeasureReader instrumentMeasure, double canvasTopStaffGroup, double canvasLeft, double allowedSpace, double lineSpacing, ColorARGB colorARGB);
    }
}
