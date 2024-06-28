namespace StudioLaValse.ScoreDocument.Templates
{
    /// <summary>
    /// Staff system style template.
    /// </summary>
    public class StaffSystemStyleTemplate
    {
        /// <summary>
        /// Distance to the next staff system.
        /// </summary>
        public required double DistanceToNext { get; set; }

        /// <summary>
        /// Create a default staff system style template.
        /// </summary>
        /// <returns></returns>
        public static StaffSystemStyleTemplate Create()
        {
            return new StaffSystemStyleTemplate()
            {
                DistanceToNext = 20,
            };
        }

        /// <summary>
        /// Apply another staff system style template.
        /// </summary>
        /// <param name="template"></param>
        public void Apply(StaffSystemStyleTemplate template)
        {
            DistanceToNext = template.DistanceToNext;
        }
    }
}
