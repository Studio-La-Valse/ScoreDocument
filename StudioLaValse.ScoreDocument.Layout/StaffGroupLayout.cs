using StudioLaValse.ScoreDocument.Core;
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
        private readonly TemplateProperty<int> numberOfStaves;
        private readonly TemplateProperty<double> distanceToNext;

        public bool Collapsed { get; set; }


        public int NumberOfStaves { get => numberOfStaves.Value; set => numberOfStaves.Value = value; }
        public double DistanceToNext { get => distanceToNext.Value; set => distanceToNext.Value = value; }


        public StaffGroupLayout(StaffGroupStyleTemplate styleTemplate, Instrument instrument)
        {
            this.styleTemplate = styleTemplate;
            this.instrument = instrument;

            numberOfStaves = new TemplateProperty<int>(() => instrument.NumberOfStaves);
            distanceToNext = new TemplateProperty<double>(() => styleTemplate.DistanceToNext);
            Collapsed = false;
        }


        /// <inheritdoc/>
        public StaffGroupLayout Copy()
        {
            var copy = new StaffGroupLayout(styleTemplate, instrument)
            {
                Collapsed = Collapsed
            };
            copy.numberOfStaves.Field = numberOfStaves.Field;
            copy.distanceToNext.Field = distanceToNext.Field;
            return copy;
        }
    }
}
