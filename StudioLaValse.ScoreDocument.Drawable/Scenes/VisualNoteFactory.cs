namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note factory.
    /// </summary>
    public class VisualNoteFactory : IVisualNoteFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualNoteFactory(ISelection<IUniqueScoreElement> selection, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.selection = selection;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double scale, bool offsetDots, Accidental? accidental, ColorARGB color)
        {
            return new VisualNote(note, color, canvasLeft, canvasTop, scale, offsetDots, accidental, selection, scoreLayoutDictionary);
        }
    }
}
