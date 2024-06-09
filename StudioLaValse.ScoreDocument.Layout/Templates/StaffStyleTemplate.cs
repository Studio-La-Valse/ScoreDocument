namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class StaffStyleTemplate
    {
        public required double DistanceToNext { get; set; }

        public static StaffStyleTemplate Create()
        {
            return new StaffStyleTemplate()
            {
                DistanceToNext = 13,
            };
        }

        public void Apply(StaffStyleTemplate template)
        {
            DistanceToNext = template.DistanceToNext;
        }
    }
}
