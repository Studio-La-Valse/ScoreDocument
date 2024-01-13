using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualNoteFactory
    {
        BaseContentWrapper Build(INoteReader note, double canvasLeft, double canvasTop, double scale, ColorARGB color);
    }
}
