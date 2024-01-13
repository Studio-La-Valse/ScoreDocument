using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualBeamBuilder
    {
        IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double scale, ColorARGB color);
    }
}