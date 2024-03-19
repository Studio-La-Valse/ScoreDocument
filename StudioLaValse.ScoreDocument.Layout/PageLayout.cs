using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    public class PageLayout : ILayoutElement<PageLayout>
    {
        private readonly PageStyleTemplate pageStyleTemplate;
        private readonly TemplateProperty<int> pageWidth;
        private readonly TemplateProperty<int> pageHeight;
        private readonly TemplateProperty<double> marginLeft;
        private readonly TemplateProperty<double> marginTop;
        private readonly TemplateProperty<double> marginRight;
        private readonly TemplateProperty<double> marginBottom;

        public int PageWidth { get => pageWidth.Value; set => pageWidth.Value = value; }
        public int PageHeight { get => pageHeight.Value; set => pageHeight.Value = value; }
        public double MarginLeft { get => marginLeft.Value; set => marginLeft.Value = value; }
        public double MarginTop { get => marginTop.Value; set => marginTop.Value = value; }
        public double MarginRight { get => marginRight.Value; set => marginRight.Value = value; }
        public double MarginBottom { get => marginBottom.Value; set => marginBottom.Value = value; }


        public PageLayout(PageStyleTemplate pageStyleTemplate)
        {
            this.pageStyleTemplate = pageStyleTemplate;

            pageWidth = new TemplateProperty<int>(() => this.pageStyleTemplate.PageWidth);
            pageHeight = new TemplateProperty<int>(() => this.pageStyleTemplate.PageHeight);

            marginLeft = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginLeft);
            marginTop = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginTop);
            marginRight = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginRight);
            marginBottom = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginBottom);
        }

        public PageLayout Copy()
        {
            var copy = new PageLayout(pageStyleTemplate);
            copy.pageWidth.Field = pageWidth.Field;
            copy.pageHeight.Field = pageHeight.Field;
            copy.marginTop.Field = marginTop.Field;
            copy.marginRight.Field = marginRight.Field;
            copy.marginBottom.Field = marginBottom.Field;
            copy.marginLeft.Field = marginLeft.Field;
            return copy;
        }
    }
}