namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class StaffSystemStyleTemplate
    {
        public required double DistanceToNext { get; set; }

        public static StaffSystemStyleTemplate Create()
        {
            return new StaffSystemStyleTemplate()
            {
                DistanceToNext = 20,
            };
        }

        public void Apply(StaffSystemStyleTemplate template)
        {
            DistanceToNext = template.DistanceToNext;
        }
    }
}
