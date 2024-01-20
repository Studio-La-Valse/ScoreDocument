using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    internal interface IVisualBeamBuilder
    {
        IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double scale, ColorARGB color);
    }
}