using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Drawable.Interfaces
{
    public interface IVisualScoreMeasure
    {
        BoundingBox BoundingBox();
        IUniqueScoreElement AssociatedElement { get; }
        double PaddingRight { get; }
        double PaddingLeft { get; }
    }
}
