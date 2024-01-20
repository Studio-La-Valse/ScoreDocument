namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default scene for visualizing a score document.
    /// </summary>
    public class VisualScoreDocumentScene : BaseVisualParent<IUniqueScoreElement>
    {
        private readonly IVisualScoreDocumentContentFactory sceneFactory;
        private readonly IScoreDocumentReader scoreDocumentReader;

        /// <summary>
        /// The default constructor. 
        /// </summary>
        /// <param name="sceneFactory"></param>
        /// <param name="scoreDocumentReader"></param>
        public VisualScoreDocumentScene(IVisualScoreDocumentContentFactory sceneFactory, IScoreDocumentReader scoreDocumentReader) : base(scoreDocumentReader)
        {
            this.sceneFactory = sceneFactory;
            this.scoreDocumentReader = scoreDocumentReader;
        }

        /// <inheritdoc/>
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return new List<BaseContentWrapper>()
            {
                sceneFactory.CreateContent(scoreDocumentReader)
            };
        }

        /// <inheritdoc/>
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>();
        }
    }
}
