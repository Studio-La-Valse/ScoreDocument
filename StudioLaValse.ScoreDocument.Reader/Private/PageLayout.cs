﻿using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class PageLayout : IPageLayout
    {
        private readonly PageStyleTemplate pageStyleTemplate;
        private readonly ValueTemplateProperty<int> pageWidth;
        private readonly ValueTemplateProperty<int> pageHeight;
        private readonly ValueTemplateProperty<double> marginLeft;
        private readonly ValueTemplateProperty<double> marginTop;
        private readonly ValueTemplateProperty<double> marginRight;
        private readonly ValueTemplateProperty<double> marginBottom;


        public int PageWidth { get => pageWidth.Value; }
        public int PageHeight { get => pageHeight.Value; }
        public double MarginLeft { get => marginLeft.Value; }
        public double MarginTop { get => marginTop.Value; }
        public double MarginRight { get => marginRight.Value; }
        public double MarginBottom { get => marginBottom.Value; }


        public PageLayout(PageStyleTemplate pageStyleTemplate)
        {
            this.pageStyleTemplate = pageStyleTemplate;

            pageWidth = new ValueTemplateProperty<int>(() => this.pageStyleTemplate.PageWidth);
            pageHeight = new ValueTemplateProperty<int>(() => this.pageStyleTemplate.PageHeight);

            marginLeft = new ValueTemplateProperty<double>(() => this.pageStyleTemplate.MarginLeft);
            marginTop = new ValueTemplateProperty<double>(() => this.pageStyleTemplate.MarginTop);
            marginRight = new ValueTemplateProperty<double>(() => this.pageStyleTemplate.MarginRight);
            marginBottom = new ValueTemplateProperty<double>(() => this.pageStyleTemplate.MarginBottom);
        }
    }
}