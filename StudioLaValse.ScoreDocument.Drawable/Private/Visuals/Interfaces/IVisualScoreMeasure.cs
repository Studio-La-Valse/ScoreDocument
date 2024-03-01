using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Interfaces
{
    internal interface IVisualScoreMeasure
    {
        BoundingBox BoundingBox();
        IUniqueScoreElement AssociatedElement { get; }
        double PaddingRight { get; }
        double PaddingLeft { get; }
    }
}
