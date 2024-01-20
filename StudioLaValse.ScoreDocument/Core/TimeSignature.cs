namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical time signature.
    /// </summary>
    public class TimeSignature : Duration
    {
        /// <inheritdoc/>
        public TimeSignature(int steps, PowerOfTwo nths) : base(steps, nths)
        {
            if (steps <= 0)
            {
                throw new ArgumentException("Steps cannot be smaller than or equal to 0.");
            }
        }
    }
}
