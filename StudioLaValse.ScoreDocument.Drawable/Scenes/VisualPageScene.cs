using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <inheritdoc/>
    public class VisualPageScene : IVisualPageScene
    {
        private readonly IVisualStaffSystemScene staffSystemContentFactory;

        /// <inheritdoc/>
        public VisualPageScene(IVisualStaffSystemScene staffSystemContentFactory)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IPage page, double canvasLeft, double canvasTop)
        {
            var visualPage = new VisualPage(page, canvasLeft, canvasTop, staffSystemContentFactory);
            return visualPage;
        }
    }
}
