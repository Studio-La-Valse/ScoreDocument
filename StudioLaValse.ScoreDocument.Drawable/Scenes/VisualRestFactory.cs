using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual rest factory.
    /// </summary>
    public class VisualRestFactory : IVisualRestFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocumentLayout scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="scoreDocumentLayout"></param>
        /// <param name="unitToPixelConverter"></param>
        public VisualRestFactory(ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreDocumentLayout,IUnitToPixelConverter unitToPixelConverter)
        {
            this.selection = selection;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Build(IChordReader note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale)
        {
            return new VisualRest(note, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, scoreDocumentLayout, selection, unitToPixelConverter);
        }
    }
}
