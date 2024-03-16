using StudioLaValse.ScoreDocument.Core;
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
        private readonly TemplateProperty<double> width;
        private readonly TemplateProperty<KeySignature> keySignature;
        private readonly TemplateProperty<double> paddingLeft;
        private readonly TemplateProperty<double> paddingRight;

        public KeySignature KeySignature
        {
            get
            {
                return keySignature.Value;
            }
            set
            {
                keySignature.Value = value;
            }
        }
        public double PaddingLeft
        {
            get
            {
                return paddingLeft.Value;
            }
            set
            {
                paddingLeft.Value = value;
            }
        }
        public double PaddingRight
        {
            get
            {
                return paddingRight.Value;
            }
            set
            {
                paddingRight.Value = value;
            }
        }
        public double Width
        {
            get
            {
                return width.Value;
            }
            set
            {
                width.Value = value;
            }
        }


        public ScoreMeasureLayout(ScoreMeasureStyleTemplate scoreMeasureStyleTemplate, IScoreMeasure scoreMeasure)
        {
            this.scoreMeasureStyleTemplate = scoreMeasureStyleTemplate;
            this.scoreMeasure = scoreMeasure;

            keySignature = new TemplateProperty<KeySignature>(() => scoreMeasure.KeySignature);
            width = new TemplateProperty<double>(() => scoreMeasureStyleTemplate.Width);
            paddingLeft = new TemplateProperty<double>(() => scoreMeasureStyleTemplate.PaddingLeft);
            paddingRight = new TemplateProperty<double>(() => scoreMeasureStyleTemplate.PaddingRight);
        }


        /// <inheritdoc/>
        public ScoreMeasureLayout Copy()
        {
            var copy = new ScoreMeasureLayout(scoreMeasureStyleTemplate, scoreMeasure);
            copy.width.Field = width.Field;
            copy.paddingLeft.Field = paddingLeft.Field;
            copy.paddingRight.Field = paddingRight.Field;
            copy.keySignature.Field = keySignature.Field;
            return copy;
        }
    }
}