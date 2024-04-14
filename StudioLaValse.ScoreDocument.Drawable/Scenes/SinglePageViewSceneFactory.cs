namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// An implementation of the visual score document factory, that visualizes a single page of the score document.
    /// </summary>
    public class SinglePageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly int pageIndex;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="staffSystemContentFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public SinglePageViewSceneFactory(int pageIndex, IVisualStaffSystemFactory staffSystemContentFactory, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.pageIndex = pageIndex;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            var page = scoreDocument.GeneratePages().ElementAt(pageIndex);
            var lineSpacing = GlyphLibrary.LineSpacing;
            var visualPage = new VisualPage(page, 0, 0, lineSpacing, staffSystemContentFactory, scoreLayoutDictionary);
            return visualPage;
        }
    }
}
