using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <inheritdoc/>
    public class VisualPageFactory : IVisualPageFactory
    {
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly IScoreDocument scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        /// <inheritdoc/>
        public VisualPageFactory(IVisualStaffSystemFactory staffSystemContentFactory, IScoreDocument scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IPage page, double canvasLeft, double canvasTop)
        {
            var lineSpacing = Glyph.LineSpacingMm;
            var visualPage = new VisualPage(page, canvasLeft, canvasTop, lineSpacing, staffSystemContentFactory, scoreLayoutDictionary, unitToPixelConverter);
            return visualPage;
        }
    }
}
