namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for a visual note.
    /// </summary>
    public interface IVisualNoteFactory
    {
        /// <summary>
        /// Build the visual note.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <param name="scale"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double scale, ColorARGB color);
    }
}
