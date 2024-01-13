namespace StudioLaValse.ScoreDocument.Core
{
    public class Fraction
    {
        protected readonly decimal precision = 1 / 128M;

        public int Numinator { get; }
        public int Denominator { get; }
        public decimal Decimal =>
            Numinator / (decimal)Denominator;



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


        public virtual Fraction Simplify()
        {
            var greatestCommonDivisor = GreatestCommonDivisor(Numinator, Denominator);

            var minPosition = Numinator / greatestCommonDivisor;

            var minSteps = Denominator / greatestCommonDivisor;

            return new Fraction(minPosition, minSteps);
        }



        public override string ToString()
        {
            return $"{Numinator} / {Denominator}";
        }
    }
}
