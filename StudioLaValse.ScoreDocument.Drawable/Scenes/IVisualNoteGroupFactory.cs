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
        /// <param name="canvasTopStaffGroup"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="allowedSpace"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        BaseContentWrapper Build(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double allowedSpace, ColorARGB color);
    }
}
