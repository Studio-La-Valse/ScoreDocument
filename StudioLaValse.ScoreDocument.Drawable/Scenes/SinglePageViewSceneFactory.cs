using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// An implementation of the visual score document factory, that visualizes a single page of the score document.
    /// </summary>
    public class SinglePageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly int pageIndex;
        private readonly IVisualPageFactory visualPageFactory;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="visualPageFactory"></param>
        public SinglePageViewSceneFactory(int pageIndex, IVisualPageFactory visualPageFactory)
        {
            this.pageIndex = pageIndex;
            this.visualPageFactory = visualPageFactory;
        }
        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocument scoreDocument)
        {
            var page = scoreDocument.ReadPages(Glyph.LineSpacingMm).ElementAt(pageIndex);
            return visualPageFactory.CreateContent(page, 0, 0);
        }
    }
}
