namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual instrument measure factory.
    /// </summary>
    public class VisualStaffGroupMeasureFactory : IVisualInstrumentMeasureFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IVisualNoteGroupFactory noteGroupFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="noteGroupFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualStaffGroupMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualNoteGroupFactory noteGroupFactory, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.selection = selection;
            this.noteGroupFactory = noteGroupFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, double lineSpacing, ColorARGB color)
        {
            var scoreLayout = scoreLayoutDictionary.DocumentLayout();
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = 1d;
            if (scoreLayout.InstrumentScales.TryGetValue(staffGroup.Instrument, out var value))
            {
                instrumentScale = value;
            }
            return new VisualStaffGroupMeasure(source, staffGroup, canvasTop, canvasLeft, width, paddingLeft, paddingRight, lineSpacing, scoreScale, instrumentScale, color, noteGroupFactory, selection, scoreLayoutDictionary);
        }
    }
}
