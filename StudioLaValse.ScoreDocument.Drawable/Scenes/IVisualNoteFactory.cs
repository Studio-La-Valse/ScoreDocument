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
        /// <param name="clef"></param>
        /// <param name="accidental"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <returns></returns>
        BaseContentWrapper Build(INote note, Clef clef, Accidental? accidental, double canvasLeft, double canvasTop);
    }
}
