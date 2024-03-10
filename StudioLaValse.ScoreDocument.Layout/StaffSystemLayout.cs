using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff system layout.
    /// </summary>
    public class StaffSystemLayout : ILayoutElement<StaffSystemLayout>
    {
        private readonly StaffSystemStyleTemplate styleTemplate;

        public TemplateProperty<double> PaddingBottom { get; }


        public StaffSystemLayout(StaffSystemStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;

            PaddingBottom = new TemplateProperty<double>(() => styleTemplate.PaddingBottom);
        }


        /// <inheritdoc/>
        public StaffSystemLayout Copy()
        {
            var copy = new StaffSystemLayout(styleTemplate);
            copy.PaddingBottom.Field = PaddingBottom.Field;
            return copy;
        }
    }
}
