namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A record for the positoin in a measure.
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="SpaceRight"></param>
    public record PositionInMeasure(double Position, double SpaceRight)
    {
        /// <summary>
        /// Calculate the right-most side of the position.
        /// </summary>
        /// <returns></returns>
        public double GetRight()
        {
            return Position + SpaceRight;
        }
    }
}
