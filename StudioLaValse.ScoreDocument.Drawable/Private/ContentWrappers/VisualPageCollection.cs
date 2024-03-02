namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualPageCollection : BaseContentWrapper
    {
        private readonly IEnumerable<BaseContentWrapper> visualPages;



        public VisualPageCollection(IEnumerable<BaseContentWrapper> visualPages)
        {
            this.visualPages = visualPages;
        }




        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(visualPages.Select(p => p.BoundingBox()));
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return visualPages;
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>();
        }
    }
}
