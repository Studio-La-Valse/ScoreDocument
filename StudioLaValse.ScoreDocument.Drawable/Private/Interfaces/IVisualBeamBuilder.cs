namespace StudioLaValse.ScoreDocument.Drawable.Private.Interfaces
{
    internal interface IVisualBeamBuilder
    {
        IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamThickness, double beamSpacing, double scale, double positionSpace);
    }
}