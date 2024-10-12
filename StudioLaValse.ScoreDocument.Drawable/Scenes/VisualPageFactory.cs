using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <inheritdoc/>
    public class VisualPageFactory : IVisualPageFactory
    {
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;

        /// <inheritdoc/>
        public VisualPageFactory(IVisualStaffSystemFactory staffSystemContentFactory)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IPage page, double canvasLeft, double canvasTop)
        {
            var visualPage = new VisualPage(page, canvasLeft, canvasTop, staffSystemContentFactory);
            return visualPage;
        }
    }
}
