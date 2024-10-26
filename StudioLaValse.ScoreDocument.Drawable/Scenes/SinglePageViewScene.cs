using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// An implementation of the visual score document factory, that visualizes a single page of the score document.
    /// </summary>
    public class SinglePageViewScene : IVisualScoreDocumentScene
    {
        private readonly int pageIndex;
        private readonly IVisualPageScene visualPageFactory;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="visualPageFactory"></param>
        public SinglePageViewScene(int pageIndex, IVisualPageScene visualPageFactory)
        {
            this.pageIndex = pageIndex;
            this.visualPageFactory = visualPageFactory;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Create(IScoreDocument scoreDocument)
        {
            var page = scoreDocument.ReadPages().ElementAt(pageIndex);
            return visualPageFactory.Create(page, 0, 0);
        }
    }
}
