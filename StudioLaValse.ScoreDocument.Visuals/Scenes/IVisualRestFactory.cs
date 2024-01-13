using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualRestFactory
    {
        BaseContentWrapper Build(IChord element, double canvasLeft, double canvasTop, double scale, ColorARGB color);
    }
}
