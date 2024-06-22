using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <inheritdoc/>
    public class VisualPageFactory : IVisualPageFactory
    {
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        /// <inheritdoc/>
        public VisualPageFactory(IVisualStaffSystemFactory staffSystemContentFactory, IScoreDocumentLayout scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IPageReader page, double canvasLeft, double canvasTop)
        {
            var lineSpacing = Glyph.LineSpacingMm;
            var visualPage = new VisualPage(page, canvasLeft, canvasTop, lineSpacing, staffSystemContentFactory, scoreLayoutDictionary, unitToPixelConverter);
            return visualPage;
        }
    }
}
