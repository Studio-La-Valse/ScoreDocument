namespace StudioLaValse.ScoreDocument.Core
{
    public class TimeSignature : Duration
    {
        public TimeSignature(int steps, PowerOfTwo nths) : base(steps, nths.Value)
        {
            if (steps <= 0)
            {
                throw new ArgumentException("Steps cannot be smaller than or equal to 0.");
            }
        }
    }
}
