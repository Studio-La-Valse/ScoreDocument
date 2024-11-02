using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <inheritdoc/>
    public class VisualPageScene : IVisualPageScene
    {
        private readonly IVisualStaffSystemScene staffSystemContentFactory;
        private readonly IPositionDictionaryBuilder positionDictionaryBuilder;

        /// <inheritdoc/>
        public VisualPageScene(IVisualStaffSystemScene staffSystemContentFactory, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.positionDictionaryBuilder = positionDictionaryBuilder;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IPage page, double canvasLeft, double canvasTop)
        {
            var visualPage = new VisualPage(page, canvasLeft, canvasTop, staffSystemContentFactory, positionDictionaryBuilder);
            return visualPage;
        }
    }
}
