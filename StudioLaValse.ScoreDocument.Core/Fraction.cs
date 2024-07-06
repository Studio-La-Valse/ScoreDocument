using StudioLaValse.ScoreDocument.Core.Private;

namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a fraction of two non negative integers.
    /// The Denominator may never be equal to 0.
    /// </summary>
    public class Fraction
    {
        /// <summary>
        /// The precisino for decimmal operations.
        /// </summary>
        protected readonly decimal precision = 1 / 128M;

        /// <summary>
        /// The numinator of the fraction.
        /// </summary>
        public int Numerator { get; }
        /// <summary>
        /// The denominator of the fraction.
        /// </summary>
        public int Denominator { get; }
        /// <summary>
        /// Casts the fraction to a decimal.
        /// </summary>
        public decimal Decimal =>
            Numerator / (decimal)Denominator;


        /// <summary>
        /// Construct a fraction from two non-negative integers. The denominator may never be equal to 0.
        /// </summary>
        /// <param name="numinator"></param>
        /// <param name="denominator"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="Exception"></exception>
        public Fraction(int numinator, int denominator)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(numinator);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(denominator);

            Numerator = numinator;

            Denominator = denominator;
        }

        /// <summary>
        /// Simplify this fraction to the greatest common divisor of the numinator and denominator.
        /// </summary>
        /// <returns></returns>
        public virtual Fraction Simplify()
        {
            var greatestCommonDivisor = Numerator.GCD(Denominator);

            var minPosition = Numerator / greatestCommonDivisor;

            var minSteps = Denominator / greatestCommonDivisor;

            return new Fraction(minPosition, minSteps);
        }

        /// <summary>
        /// Add two fractions.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Fraction operator +(Fraction first, Fraction second)
        {
            if (first.Denominator == second.Denominator)
            {
                return new Duration(first.Numerator + second.Numerator, first.Denominator);
            }

            var nominator =
                (first.Numerator * second.Denominator) +
                (first.Denominator * second.Numerator);

            var denominator = first.Denominator * second.Denominator;

            return new Fraction(nominator, denominator).Simplify();
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Numerator} / {Denominator}";
        }
    }
}
