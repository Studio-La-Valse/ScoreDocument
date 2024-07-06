namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="IPositionElement"/> interface.
    /// </summary>
    public static class PositionElementExtensions
    {
        /// <summary>
        /// Calculates the actual duration of the element, by taking into account the tuplet information.
        /// </summary>
        /// <param name="positionElement"></param>
        /// <returns></returns>
        public static Fraction ActualDuration(this IPositionElement positionElement)
        {
            return positionElement.Tuplet.IsRedundant
                ? positionElement.RythmicDuration
                : positionElement.Tuplet.ToActualDuration(positionElement.RythmicDuration);
        }

        /// <summary>
        /// Calculates the position at the end of the element, taking into account it's actual duration.
        /// </summary>
        /// <param name="positionElement"></param>
        /// <returns></returns>
        public static Fraction PositionEnd(this IPositionElement positionElement)
        {
            return positionElement.Position + positionElement.ActualDuration();
        }

        /// <summary>
        /// Calculates whether the specified position element contais the specified position. 
        /// If the position and the start of the position are equal, this method will return true.
        /// If the position and the end of the position are equal, this method will return false;
        /// </summary>
        /// <param name="positionElement"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool ContainsPosition(this IPositionElement positionElement, Position position)
        {
            return
                positionElement.Position.Decimal <= position.Decimal &&
                (positionElement.Position + positionElement.RythmicDuration).Decimal > position.Decimal;
        }
    }
}
