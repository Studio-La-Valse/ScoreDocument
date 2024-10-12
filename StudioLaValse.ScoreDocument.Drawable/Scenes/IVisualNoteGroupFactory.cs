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
        /// <returns></returns>
        BaseContentWrapper Build(IMeasureBlock noteGroup, IStaffGroup staffGroup, IInstrumentMeasure instrumentMeasure, IReadOnlyDictionary<Position, double> positionDictionary, double canvasTopStaffGroup);
    }
}
