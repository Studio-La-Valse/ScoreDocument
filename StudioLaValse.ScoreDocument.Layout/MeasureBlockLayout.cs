using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note group.
    /// </summary>
    public class MeasureBlockLayout : ILayoutElement<MeasureBlockLayout>
    {
        private readonly MeasureBlockStyleTemplate styleTemplate;
        private readonly TemplateProperty<double> stemLength;
        private readonly TemplateProperty<double> beamAngle;

        public double StemLength { get => stemLength.Value; set => stemLength.Value = value; }
        public double BeamAngle { get => beamAngle.Value; set => beamAngle.Value = value; }

        /// <summary>
        /// Create the default layout.
        /// </summary>
        public MeasureBlockLayout(MeasureBlockStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
            stemLength = new TemplateProperty<double>(() => styleTemplate.StemLength);
            beamAngle = new TemplateProperty<double>(() => styleTemplate.BracketAngle);
        }

        /// <inheritdoc/>
        public MeasureBlockLayout Copy()
        {
            var copy = new MeasureBlockLayout(styleTemplate);
            copy.stemLength.Field = stemLength.Field;
            copy.beamAngle.Field = beamAngle.Field;
            return copy;
        }
    }
}
