using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// An implementation of the visual score document factory, that visualizes a single page of the score document.
    /// </summary>
    public class SinglePageViewScene : IVisualScoreDocumentScene
    {
        private readonly int pageIndex;
        private readonly IVisualPageScene visualPageFactory;
        private readonly IPositionDictionaryBuilder positionDictionaryBuilder;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="visualPageFactory"></param>
        /// <param name="positionDictionaryBuilder"></param>
        public SinglePageViewScene(int pageIndex, IVisualPageScene visualPageFactory, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            this.pageIndex = pageIndex;
            this.visualPageFactory = visualPageFactory;
            this.positionDictionaryBuilder = positionDictionaryBuilder;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Create(IScoreDocument scoreDocument)
        {
            var page = scoreDocument.ReadPages(positionDictionaryBuilder).ElementAt(pageIndex);
            return visualPageFactory.Create(page, 0, 0);
        }
    }
}
