using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public class VisualScoreDocumentScene : BaseVisualParent<IUniqueScoreElement>
    {
        private readonly IVisualScoreDocumentContentFactory sceneFactory;
        private readonly IScoreDocumentReader scoreDocumentReader;

        public VisualScoreDocumentScene(IVisualScoreDocumentContentFactory sceneFactory, IScoreDocumentReader scoreDocumentReader) : base(scoreDocumentReader)
        {
            this.sceneFactory = sceneFactory;
            this.scoreDocumentReader = scoreDocumentReader;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return new List<BaseContentWrapper>()
            {
                sceneFactory.CreateContent(scoreDocumentReader)
            };
        }

        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>();
        }
    }
}
