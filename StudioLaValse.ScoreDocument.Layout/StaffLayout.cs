using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff layout.
    /// </summary>
    public class StaffLayout : ILayoutElement<StaffLayout>
    {
        private readonly StaffStyleTemplate styleTemplate;
        private readonly TemplateProperty<double> distanceToNext;

        public double DistanceToNext { get => distanceToNext.Value; set => distanceToNext.Value = value; }



        public StaffLayout(StaffStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
            distanceToNext = new TemplateProperty<double>(() => styleTemplate.DistanceToNext);
        }




        /// <inheritdoc/>
        public StaffLayout Copy()
        {
            var copy = new StaffLayout(styleTemplate);
            copy.distanceToNext.Field = distanceToNext.Field;
            return copy;
        }
    }
}
