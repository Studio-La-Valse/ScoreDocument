using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Interfaces
{
    internal interface IVisualBeamBuilder
    {
        IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double scale, ColorARGB color);
    }
}