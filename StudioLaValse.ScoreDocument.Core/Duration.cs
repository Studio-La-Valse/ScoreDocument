﻿namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a duration of a musical element.
    /// Extends a generic fraction.
    /// </summary>
    public class Duration : Fraction, IEquatable<Duration>
    {
        /// <inheritdoc/>
        public new PowerOfTwo Denominator { get; }

        /// <summary>
        /// Construct a duration for an integer value representing number of beats, and a power of two representing the length of one beat.
        /// </summary>
        /// <param name="numberOf"></param>
        /// <param name="nths"></param>
        public Duration(int numberOf, PowerOfTwo nths) : base(numberOf, nths)
        {
            Denominator = nths;
        }



        /// <summary>
        /// Simplify this duration. For example, a 2/4ths duration will be simplified to a 1/2nd duration.
        /// </summary>
        /// <returns></returns>
        public override Duration Simplify()
        {
            if (Numerator == 0)
            {
                return new Duration(0, 1);
            }

            var denom = Denominator;
            var num = Numerator;
            var halfDenom = denom / 2M;
            var halfNum = num / 2M;

            while (halfDenom % 1 == 0 && halfNum % 1 == 0)
            {
                denom /= 2;
                num /= 2;
                halfDenom = denom / 2M;
                halfNum = num / 2M;
            }

            return new Duration(num, denom);
        }

        /// <inheritdoc/>
        public bool Equals(Duration? other)
        {
            if(other is null)
            {
                return false;
            }

            return Decimal == other.Decimal;
        }




        /// <summary>
        /// Add two durations.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Duration operator +(Duration first, Duration second)
        {
            if (first.Denominator == second.Denominator)
            {
                return new Duration(first.Numerator + second.Numerator, first.Denominator);
            }

            var nominator =
                (first.Numerator * second.Denominator) +
                (first.Denominator * second.Numerator);

            var denominator = first.Denominator * second.Denominator;

            return new Duration(nominator, denominator).Simplify();
        }

        /// <summary>
        /// Determine whether the first duration is shorter than the second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <(Duration first, Duration second)
        {
            return first.Decimal < second.Decimal;
        }

        /// <summary>
        /// Determine whether the first duration is longer than the second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >(Duration first, Duration second)
        {
            return first.Decimal > second.Decimal;
        }

        /// <summary>
        /// Determine whether the first duration is shorter than or equal to the second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <=(Duration first, decimal second)
        {
            return first.Decimal <= second;
        }

        /// <summary>
        /// Determine whether the first duration is longer than or equal to the second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >=(Duration first, decimal second)
        {
            return first.Decimal >= second;
        }

        /// <summary>
        /// Multply this position by n steps.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Duration operator *(Duration duration, int n)
        {
            return new Duration(duration.Numerator * n, duration.Denominator).Simplify();
        }
    }
}
