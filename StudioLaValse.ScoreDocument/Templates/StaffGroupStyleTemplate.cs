namespace StudioLaValse.ScoreDocument.Templates
{
    /// <summary>
    /// A staff group style template.
    /// </summary>
    public class StaffGroupStyleTemplate
    {
        /// <summary>
        /// The distance to the next staff group.
        /// </summary>
        public required double DistanceToNext { get; set; }

        /// <summary>
        /// Create a default staff group style.
        /// </summary>
        /// <returns></returns>
        public static StaffGroupStyleTemplate Create()
        {
            return new StaffGroupStyleTemplate()
            {
                DistanceToNext = 15,
            };
        }

        /// <summary>
        /// Apply the style from the source to this taff group style.
        /// </summary>
        /// <param name="template"></param>
        public void Apply(StaffGroupStyleTemplate template)
        {
            DistanceToNext = template.DistanceToNext;
        }
    }
}
