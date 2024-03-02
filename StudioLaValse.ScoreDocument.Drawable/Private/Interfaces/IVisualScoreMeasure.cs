namespace StudioLaValse.ScoreDocument.Drawable.Private.Interfaces
{
    internal interface IVisualScoreMeasure
    {
        BoundingBox BoundingBox();
        IUniqueScoreElement AssociatedElement { get; }
        double PaddingRight { get; }
        double PaddingLeft { get; }
    }
}
