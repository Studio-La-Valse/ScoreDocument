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
            if (numinator < 0)
                throw new IndexOutOfRangeException("Cannot create a musical fractoin with a numinator smaller than 0");

            if (denominator <= 0)
                throw new Exception("A musical fraction cannot have a denominator of 0 or smaller.");

            Numinator = numinator;

            Denominator = denominator;
        }


        private static int GreatestCommonDivisor(int a, int b)
        {
            while (a != 0 && b != 0)
                if (a > b)
                    a %= b;
                else
                    b %= a;

            return a | b;
        }

        /// <summary>
        /// Simplify this fraction to the greatest common divisor of the numinator and denominator.
        /// </summary>
        /// <returns></returns>
        public virtual Fraction Simplify()
        {
            var greatestCommonDivisor = GreatestCommonDivisor(Numinator, Denominator);

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
