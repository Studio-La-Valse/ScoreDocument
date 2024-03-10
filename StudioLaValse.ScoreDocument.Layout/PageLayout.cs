using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    public class PageLayout : ILayoutElement<PageLayout>
    {
        private readonly PageStyleTemplate pageStyleTemplate;

        public TemplateProperty<int> PageWidth { get; }
        public TemplateProperty<int> PageHeight { get; }

        public TemplateProperty<double> MarginLeft { get; }
        public TemplateProperty<double> MarginTop { get; }
        public TemplateProperty<double> MarginRight { get; }
        public TemplateProperty<double> MarginBottom { get; }


        public PageLayout(PageStyleTemplate pageStyleTemplate)
        {
            this.pageStyleTemplate = pageStyleTemplate;

            PageWidth = new TemplateProperty<int>(() => this.pageStyleTemplate.PageWidth);
            PageHeight = new TemplateProperty<int>(() => this.pageStyleTemplate.PageHeight);

            MarginLeft = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginLeft);
            MarginTop = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginTop);
            MarginRight = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginRight);
            MarginBottom = new TemplateProperty<double>(() => this.pageStyleTemplate.MarginBottom);
        }

        public PageLayout Copy()
        {
            var copy = new PageLayout(pageStyleTemplate);
            copy.PageWidth.Field = this.PageWidth.Field;
            copy.PageHeight.Field = this.PageHeight.Field;
            copy.MarginTop.Field = this.MarginTop.Field;
            copy.MarginRight.Field = this.MarginRight.Field;
            copy.MarginBottom.Field = this.MarginBottom.Field;
            copy.MarginLeft.Field = this.MarginLeft.Field;
            return copy;
        }
    }
}