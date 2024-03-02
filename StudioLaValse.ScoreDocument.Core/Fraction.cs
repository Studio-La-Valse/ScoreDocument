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
        public int Numinator { get; }
        /// <summary>
        /// The denominator of the fraction.
        /// </summary>
        public int Denominator { get; }
        /// <summary>
        /// Casts the fraction to a decimal.
        /// </summary>
        public decimal Decimal =>
            Numinator / (decimal)Denominator;


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

            Numinator = numinator;

            Denominator = denominator;
        }

        /// <summary>
        /// Simplify this fraction to the greatest common divisor of the numinator and denominator.
        /// </summary>
        /// <returns></returns>
        public virtual Fraction Simplify()
        {
            var greatestCommonDivisor = Numinator.GCD(Denominator);

            var minPosition = Numinator / greatestCommonDivisor;

            var minSteps = Denominator / greatestCommonDivisor;

            return new Fraction(minPosition, minSteps);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Numinator} / {Denominator}";
        }
    }
}
