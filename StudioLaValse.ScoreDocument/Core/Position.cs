namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical position.
    /// </summary>
    public class Position : Fraction
    {
        /// <summary>
        /// Construct a position from a numinator and a denominator.
        /// </summary>
        /// <param name="beats"></param>
        /// <param name="mode"></param>
        public Position(int beats, int mode) : base(beats, mode)
        {

        }

        /// <summary>
        /// Adds a duration to the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static Position operator +(Position position, Fraction step)
        {
            if (position.Denominator == step.Denominator)
            {
                return new Position(position.Numinator + step.Numinator, position.Denominator);
            }

            var nominator =
                position.Numinator * step.Denominator +
                position.Denominator * step.Numinator;

            var denominator = position.Denominator * step.Denominator;

            return new Position(nominator, denominator).Simplify().ToPosition();
        }

        /// <summary>
        /// Specifies whether the right position is greater than the left position.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool operator >(Position right, Position left)
        {
            return right.Decimal > left.Decimal;
        }

        /// <summary>
        /// Specifies whether the right position is greater than or equal to the left position.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool operator >=(Position right, Position left)
        {
            return right.Decimal >= left.Decimal;
        }

        /// <summary>
        /// Specifies whether the right position is smaller than the left position.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool operator <(Position right, Position left)
        {
            return right.Decimal < left.Decimal;
        }

        /// <summary>
        /// Specifies whether the right position is smaller than or equal to the left position.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool operator <=(Position right, Position left)
        {
            return right.Decimal <= left.Decimal;
        }
    }
}
