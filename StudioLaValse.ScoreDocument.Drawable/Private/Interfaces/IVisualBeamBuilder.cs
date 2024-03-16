using StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Interfaces
{
    internal interface IVisualBeamBuilder
    {
        IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double scale, ColorARGB color);
    }
}