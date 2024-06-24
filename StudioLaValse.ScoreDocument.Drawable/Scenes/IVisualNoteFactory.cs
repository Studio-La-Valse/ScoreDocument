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
        /// <param name="lineSpacing"></param>
        /// <param name="scoreScale"></param>
        /// <param name="instrumentScale"></param>
        /// <param name="offsetDots"></param>
        /// <param name="accidental"></param>
        /// <returns></returns>
        BaseContentWrapper Build(INote note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, bool offsetDots, Accidental? accidental);
    }
}
