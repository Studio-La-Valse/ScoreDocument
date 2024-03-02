namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    //internal sealed class VisualScoreIndicator : BaseVisualParent<IUniqueScoreElement>
    //{
    //    private readonly IScoreIndicator indicator;
    //    private readonly IEnumerateVisualScoreMeasures enumerator;


    //    public double Parameter =>
    //        indicator.Parameter;


    //    public VisualScoreIndicator(IScoreIndicator element, IEnumerateVisualScoreMeasures baseVisualStrategy) : base(element)
    //    {
    //        indicator = element;
    //        enumerator = baseVisualStrategy;
    //    }



    //    public override IEnumerable<BaseContentWrapper> GetContentWrappers()
    //    {
    //        return new List<BaseContentWrapper>();
    //    }
    //    public override IEnumerable<BaseDrawableElement> GetDrawableElements()
    //    {
    //        var defaultList = new List<BaseDrawableElement>();

    //        var host = enumerator.EnumerateScoreMeasures().ElementAtOrDefault(indicator.MeasureIndex);
    //        if (!indicator.Visible || host is null)
    //        {
    //            return defaultList;
    //        }

    //        var bbox = host.BoundingBox();

    //        var position = MathUtils.Map((double)Parameter, 0, 1, bbox.MinPoint.X + host.PaddingLeft, bbox.MaxPoint.X - host.PaddingRight);

    //        defaultList.Add(new DrawableLineVertical(
    //            position,
    //            bbox.MinPoint.Y,
    //            bbox.Height,
    //            thickness: 0.5,
    //            color: new ColorARGB(128, 255, 0, 0)));

    //        defaultList.Add(new DrawableLineVertical(
    //            position,
    //            bbox.MinPoint.Y,
    //            bbox.Height,
    //            thickness: 2,
    //            color: new ColorARGB(30, 255, 0, 0)));

    //        defaultList.Add(new DrawableLineVertical(
    //            position,
    //            bbox.MinPoint.Y,
    //            bbox.Height,
    //            thickness: 10,
    //            color: new ColorARGB(15, 255, 0, 0)));

    //        return defaultList;
    //    }
    //}
}
