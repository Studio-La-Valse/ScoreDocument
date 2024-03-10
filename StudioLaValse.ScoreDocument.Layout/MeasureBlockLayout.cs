using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note group.
    /// </summary>
    public class MeasureBlockLayout : ILayoutElement<MeasureBlockLayout>
    {
        private readonly MeasureBlockStyleTemplate styleTemplate;

        public TemplateProperty<double> StemLength { get; set; }
        public TemplateProperty<double> BeamAngle { get; set; }

        /// <summary>
        /// Create the default layout.
        /// </summary>
        public MeasureBlockLayout(MeasureBlockStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
            StemLength = new TemplateProperty<double>(() => styleTemplate.StemLength);
            BeamAngle = new TemplateProperty<double>(() => styleTemplate.BracketAngle);
        }

        /// <inheritdoc/>
        public MeasureBlockLayout Copy()
        {
            var copy = new MeasureBlockLayout(styleTemplate);
            copy.StemLength.Field = StemLength.Field;
            copy.BeamAngle.Field = BeamAngle.Field;
            return copy;
        }
    }
}
