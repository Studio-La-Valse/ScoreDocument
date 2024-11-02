namespace StudioLaValse.ScoreDocument.Core.Extensions
{
    /// <summary>
    /// Extensions for doubles.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Remap numbers from a source- to a target range.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minStart"></param>
        /// <param name="maxStart"></param>
        /// <param name="minEnd"></param>
        /// <param name="maxEnd"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static double Map(this double value, double minStart, double maxStart, double minEnd, double maxEnd)
        {
            var fraction = maxStart - minStart; 
            if (fraction == 0) 
            { 
                throw new InvalidOperationException("Cannot remap numbers if the starting min and max values are equal."); 
            }
            var scale = (value - minStart) / fraction; 
            return minEnd + (maxEnd - minEnd) * scale;
        }

        /// <summary>
        /// Remap numbers from a source- to a target range.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minStart"></param>
        /// <param name="maxStart"></param>
        /// <param name="minEnd"></param>
        /// <param name="maxEnd"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Map(this decimal value, decimal minStart, decimal maxStart, decimal minEnd, decimal maxEnd)
        {
            var fraction = maxStart - minStart;
            if (fraction == 0)
            {
                throw new InvalidOperationException("Cannot remap numbers if the starting min and max values are equal.");
            }
            var scale = (value - minStart) / fraction;
            return minEnd + (maxEnd - minEnd) * scale;
        }
    }
}
