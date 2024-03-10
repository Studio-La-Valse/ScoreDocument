using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a score measure.
    /// </summary>
    public class ScoreMeasureLayout : ILayoutElement<ScoreMeasureLayout>
    {
        private readonly ScoreMeasureStyleTemplate scoreMeasureStyleTemplate;
        private readonly IScoreMeasure scoreMeasure;

        public TemplateProperty<KeySignature> KeySignature { get; }
        public TemplateProperty<double> PaddingLeft { get; }
        public TemplateProperty<double> PaddingRight { get; }
        public TemplateProperty<double> Width { get; }


        public ScoreMeasureLayout(ScoreMeasureStyleTemplate scoreMeasureStyleTemplate, IScoreMeasure scoreMeasure)
        {
            this.scoreMeasureStyleTemplate = scoreMeasureStyleTemplate;
            this.scoreMeasure = scoreMeasure;
            
            KeySignature = new TemplateProperty<KeySignature>(() => scoreMeasure.KeySignature);
            Width = new TemplateProperty<double>(() => scoreMeasureStyleTemplate.Width);
            PaddingLeft = new TemplateProperty<double>(() => scoreMeasureStyleTemplate.PaddingLeft);
            PaddingRight = new TemplateProperty<double>(() => scoreMeasureStyleTemplate.PaddingRight);
        }

        /// <inheritdoc/>
        public ScoreMeasureLayout Copy()
        {
            var copy = new ScoreMeasureLayout(scoreMeasureStyleTemplate, scoreMeasure);
            copy.Width.Field = Width.Field;
            copy.PaddingLeft.Field = PaddingLeft.Field;
            copy.PaddingRight.Field = PaddingRight.Field;
            copy.KeySignature.Field = KeySignature.Field;
            return copy;
        }
    }
}