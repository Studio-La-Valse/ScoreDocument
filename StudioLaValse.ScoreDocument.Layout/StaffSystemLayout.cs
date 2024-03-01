namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff system layout.
    /// </summary>
    public class StaffSystemLayout
    {
        /// <inheritdoc/>
        public double PaddingTop { get; set; }

        /// <summary>
        /// Create a new staff system layout.
        /// </summary>
        /// <param name="paddingTop"></param>
        public StaffSystemLayout(double paddingTop = 30)
        {
            PaddingTop = paddingTop;
        }


        /// <inheritdoc/>
        public StaffSystemLayout Copy()
        {
            return new StaffSystemLayout(PaddingTop);
        }
    }
}
