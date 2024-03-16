using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff system layout.
    /// </summary>
    public class StaffSystemLayout : ILayoutElement<StaffSystemLayout>
    {
        private readonly StaffSystemStyleTemplate styleTemplate;
        private TemplateProperty<double> paddingBottom;

        public double PaddingBottom { get => paddingBottom.Value; set => paddingBottom.Value = value; }


        public StaffSystemLayout(StaffSystemStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;

            paddingBottom = new TemplateProperty<double>(() => styleTemplate.PaddingBottom);
        }


        /// <inheritdoc/>
        public StaffSystemLayout Copy()
        {
            var copy = new StaffSystemLayout(styleTemplate);
            copy.paddingBottom.Field = paddingBottom.Field;
            return copy;
        }
    }
}
