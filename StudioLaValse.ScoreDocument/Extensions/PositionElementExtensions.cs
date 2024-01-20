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
            if (positionElement.Grace)
            {
                return new Duration(0, 1);
            }

            if (positionElement.Tuplet.IsRedundant)
            {
                return positionElement.RythmicDuration;
            }

            return positionElement.RythmicDuration.ToActualDuration(positionElement.Tuplet);
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
    }
}
