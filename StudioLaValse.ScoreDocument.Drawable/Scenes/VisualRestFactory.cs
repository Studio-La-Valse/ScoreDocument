using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual rest factory.
    /// </summary>
    public class VisualRestFactory : IVisualRestFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocument scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="scoreDocumentLayout"></param>
        /// <param name="unitToPixelConverter"></param>
        /// <param name="glyphLibrary"></param>
        public VisualRestFactory(ISelection<IUniqueScoreElement> selection, IScoreDocument scoreDocumentLayout,IUnitToPixelConverter unitToPixelConverter, IGlyphLibrary glyphLibrary)
        {
            this.selection = selection;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
            this.glyphLibrary = glyphLibrary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Build(IChord note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale)
        {
            return new VisualRest(note, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, glyphLibrary, scoreDocumentLayout, selection, unitToPixelConverter);
        }
    }
}
