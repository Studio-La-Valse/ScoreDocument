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
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(steps);
        }
    }
}
