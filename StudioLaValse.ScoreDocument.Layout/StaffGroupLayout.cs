using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a staff group.
    /// </summary>
    public class StaffGroupLayout : ILayoutElement<StaffGroupLayout>
    {
        private readonly StaffGroupStyleTemplate styleTemplate;
        private readonly Instrument instrument;

        public bool Collapsed { get; set; }


        public TemplateProperty<int> NumberOfStaves { get; }
        public TemplateProperty<double> DistanceToNext { get; }
        public TemplateProperty<double> LineSpacing { get; }


        public StaffGroupLayout(StaffGroupStyleTemplate styleTemplate, Instrument instrument)
        {
            this.styleTemplate = styleTemplate;
            this.instrument = instrument;

            NumberOfStaves = new TemplateProperty<int>(() => instrument.NumberOfStaves);
            DistanceToNext = new TemplateProperty<double>(() => styleTemplate.LineSpacing);
            LineSpacing = new TemplateProperty<double>(() => styleTemplate.LineSpacing);
            Collapsed = false;
        }


        /// <inheritdoc/>
        public StaffGroupLayout Copy()
        {
            var copy = new StaffGroupLayout(styleTemplate, instrument)
            {
                Collapsed = Collapsed
            };
            copy.NumberOfStaves.Field = NumberOfStaves.Field;
            copy.DistanceToNext.Field = DistanceToNext.Field;
            copy.LineSpacing.Field = LineSpacing.Field;
            return copy;
        }
    }
}
