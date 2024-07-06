namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A staff style template.
    /// </summary>
    public class StaffStyleTemplate
    {
        /// <summary>
        /// The distance to the next staff.
        /// </summary>
        public required double DistanceToNext { get; set; }

        /// <summary>
        /// Create a default staff style template.
        /// </summary>
        /// <returns></returns>
        public static StaffStyleTemplate Create()
        {
            return new StaffStyleTemplate()
            {
                DistanceToNext = 15,
            };
        }

        /// <summary>
        /// Apply the values from the source to this staff style template.
        /// </summary>
        /// <param name="template"></param>
        public void Apply(StaffStyleTemplate template)
        {
            DistanceToNext = template.DistanceToNext;
        }
    }
}
