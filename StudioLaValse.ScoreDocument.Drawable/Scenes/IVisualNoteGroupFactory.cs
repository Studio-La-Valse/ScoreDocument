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
        /// <param name="positionDictionary"></param>
        /// <param name="canvasTopStaffGroup"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="positionSpace"></param>
        /// <returns></returns>
        BaseContentWrapper Build(IMeasureBlockReader noteGroup,
                                 IStaffGroupReader staffGroup,
                                 IInstrumentMeasureReader instrumentMeasure,
                                 IReadOnlyDictionary<Position, double> positionDictionary,
                                 double canvasTopStaffGroup,
                                 double lineSpacing,
                                 double positionSpace);
    }
}
