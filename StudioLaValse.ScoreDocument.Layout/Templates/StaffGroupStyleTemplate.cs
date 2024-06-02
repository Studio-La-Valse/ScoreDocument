namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class StaffGroupStyleTemplate
    {
        public required double DistanceToNext { get; set; }

        public static StaffGroupStyleTemplate Create()
        {
            return new StaffGroupStyleTemplate()
            {
                DistanceToNext = 25,
            };
        }

        public void Apply(StaffGroupStyleTemplate template)
        {
            DistanceToNext = template.DistanceToNext;
        }
    }
}
