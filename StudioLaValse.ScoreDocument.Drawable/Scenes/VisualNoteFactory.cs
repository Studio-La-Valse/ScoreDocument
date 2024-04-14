namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note factory.
    /// </summary>
    public class VisualNoteFactory : IVisualNoteFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualNoteFactory(ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.selection = selection;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, bool offsetDots, Accidental? accidental, ColorARGB color)
        {
            var noteScale = scoreLayoutDictionary.NoteLayout(note).Scale;
            return new VisualNote(note, color, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, noteScale, offsetDots, accidental, selection, scoreLayoutDictionary);
        }
    }
}
