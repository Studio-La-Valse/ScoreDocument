using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a score document.
    /// </summary>
    public class ScoreDocumentLayout : ILayoutElement<ScoreDocumentLayout>
    {
        private readonly ScoreDocumentStyleTemplate styleTemplate;
        private readonly TemplateProperty<double> scale;
        private readonly TemplateProperty<double> horizontalStaffLineThickness;
        private readonly TemplateProperty<double> verticalStaffLineThickness;
        private readonly TemplateProperty<double> stemLineThickness;
        private readonly TemplateProperty<double> firstSystemIndent;
        private readonly NullableTemplateProperty<ColorARGB> pageColor;
        private readonly NullableTemplateProperty<ColorARGB> foregroundColor;

        public double Scale
        {
            get
            {
                return scale.Value;
            }
            set
            {
                scale.Value = value;
            }
        }
        public double HorizontalStaffLineThickness
        {
            get
            {
                return horizontalStaffLineThickness.Value;
            }
            set
            {
                horizontalStaffLineThickness.Value = value;
            }
        }
        public double VerticalStaffLineThickness
        {
            get
            {
                return verticalStaffLineThickness.Value;
            }
            set
            {
                verticalStaffLineThickness.Value = value;
            }
        }
        public double StemLineThickness
        {
            get
            {
                return stemLineThickness.Value;
            }
            set
            {
                stemLineThickness.Value = value;
            }
        }
        public double FirstSystemIndent
        {
            get
            {
                return firstSystemIndent.Value;
            }
            set
            {
                firstSystemIndent.Value = value;
            }
        }
        public ColorARGB PageColor
        {
            get
            {
                return pageColor.Value;
            }
            set
            {
                pageColor.Value = value;
            }
        }
        public ColorARGB ForegroundColor
        {
            get
            {
                return foregroundColor.Value;
            }
            set
            {
                foregroundColor.Value = value;
            }
        }
        public Dictionary<Instrument, double> InstrumentScales =>
            styleTemplate.InstrumentScales;

        public ScoreDocumentLayout(ScoreDocumentStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
            this.scale = new TemplateProperty<double>(() => styleTemplate.Scale);
            this.horizontalStaffLineThickness = new TemplateProperty<double>(() => styleTemplate.HorizontalStaffLineThickness);
            this.verticalStaffLineThickness = new TemplateProperty<double>(() => styleTemplate.VerticalStaffLineThickness);
            this.stemLineThickness = new TemplateProperty<double>(() => styleTemplate.StemLineThickness);
            this.firstSystemIndent = new TemplateProperty<double>(() => styleTemplate.FirstSystemIndent);
            this.pageColor = new NullableTemplateProperty<ColorARGB>(() => styleTemplate.PageColor);
            this.foregroundColor = new NullableTemplateProperty<ColorARGB>(() => styleTemplate.ForegroundColor);
        }



        /// <inheritdoc/>
        public ScoreDocumentLayout Copy()
        {
            var copy = new ScoreDocumentLayout(styleTemplate);
            copy.scale.Field = scale.Field;
            copy.horizontalStaffLineThickness.Field = horizontalStaffLineThickness.Field;
            copy.verticalStaffLineThickness.Field = verticalStaffLineThickness.Field;
            copy.stemLineThickness.Field = stemLineThickness.Field;
            copy.firstSystemIndent.Field = firstSystemIndent.Field;
            copy.pageColor.Field = pageColor.Field;
            copy.foregroundColor.Field = foregroundColor.Field;
            return copy;
        }
    }
}