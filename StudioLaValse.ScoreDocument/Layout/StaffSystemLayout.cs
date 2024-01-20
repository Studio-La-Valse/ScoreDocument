﻿namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff system layout.
    /// </summary>
    public class StaffSystemLayout : IStaffSystemLayout
    {
        /// <inheritdoc/>
        public double PaddingTop { get; }

        /// <summary>
        /// Create a new staff system layout.
        /// </summary>
        /// <param name="paddingTop"></param>
        public StaffSystemLayout(double paddingTop = 30)
        {
            PaddingTop = paddingTop;
        }

        /// <inheritdoc/>
        public IStaffSystemLayout Copy()
        {
            return new StaffSystemLayout(PaddingTop);
        }
    }
}
