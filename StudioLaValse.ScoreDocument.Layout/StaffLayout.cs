using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff layout.
    /// </summary>
    public class StaffLayout : ILayoutElement<StaffLayout>
    {
        private readonly StaffStyleTemplate styleTemplate;



        public TemplateProperty<double> DistanceToNext { get; }



        public StaffLayout(StaffStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
            DistanceToNext = new TemplateProperty<double>(() =>  styleTemplate.DistanceToNext);
        }




        /// <inheritdoc/>
        public StaffLayout Copy()
        {
            var copy = new StaffLayout(styleTemplate);
            copy.DistanceToNext.Field = DistanceToNext.Field;
            return copy;
        }
    }
}
