using StudioLaValse.ScoreDocument.Primitives;

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
        private readonly IUnitToPixelConverter unitToPixelConverter;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="noteGroupFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <param name="unitToPixelConverter"></param>
        public VisualInstrumentMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualNoteGroupFactory noteGroupFactory, IScoreDocumentLayout scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter)
        {
            this.selection = selection;
            this.noteGroupFactory = noteGroupFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, IReadOnlyDictionary<Position, double> positions, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, double lineSpacing, double positionSpace)
        {
            var scoreLayout = scoreLayoutDictionary;
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;
            return new VisualStaffGroupMeasure(
                source,
                staffGroup,
                positions,
                canvasTop,
                canvasLeft,
                width,
                paddingLeft,
                paddingRight,
                lineSpacing,
                positionSpace,
                scoreScale,
                instrumentScale,
                noteGroupFactory,
                selection,
                scoreLayoutDictionary,
                unitToPixelConverter);
        }
    }
}
