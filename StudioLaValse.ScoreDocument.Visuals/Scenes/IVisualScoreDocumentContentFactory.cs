using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualScoreDocumentContentFactory
    {
        BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument);
    }
}
