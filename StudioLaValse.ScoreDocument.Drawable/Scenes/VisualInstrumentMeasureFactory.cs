using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual instrument measure factory.
    /// </summary>
    public class VisualInstrumentMeasureFactory : IVisualInstrumentMeasureFactory
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
        public VisualInstrumentMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualNoteGroupFactory noteGroupFactory, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.selection = selection;
            this.noteGroupFactory = noteGroupFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, double lineSpacing, ColorARGB color)
        {
            var scoreLayout = scoreLayoutDictionary;
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = scoreLayout.GetInstrumentScale(staffGroup.InstrumentRibbon);
            return new VisualStaffGroupMeasure(source, staffGroup, canvasTop, canvasLeft, width, paddingLeft, paddingRight, lineSpacing, scoreScale, instrumentScale, color, noteGroupFactory, selection, scoreLayoutDictionary);
        }
    }
}
