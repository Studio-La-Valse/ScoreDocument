﻿namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default scene for visualizing a score document.
    /// </summary>
    public class VisualScoreDocumentScene : BaseVisualParent<IUniqueScoreElement>
    {
        private readonly IVisualScoreDocumentContentFactory sceneFactory;
        private readonly IScoreDocument scoreDocumentReader;

        /// <summary>
        /// The default constructor. 
        /// </summary>
        /// <param name="sceneFactory"></param>
        /// <param name="scoreDocumentReader"></param>
        public VisualScoreDocumentScene(IVisualScoreDocumentContentFactory sceneFactory, IScoreDocument scoreDocumentReader) : base(scoreDocumentReader)
        {
            this.sceneFactory = sceneFactory;
            this.scoreDocumentReader = scoreDocumentReader;
        }

        /// <inheritdoc/>
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield return sceneFactory.CreateContent(scoreDocumentReader);
        }

        /// <inheritdoc/>
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield break;
        }
    }
}
