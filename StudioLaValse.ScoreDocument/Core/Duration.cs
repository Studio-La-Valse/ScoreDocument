namespace StudioLaValse.ScoreDocument.Core
{
    public class Duration : Fraction
    {
        public Duration(int numberOf, PowerOfTwo nths) : base(numberOf, nths)
        {

        }




        public override Duration Simplify()
        {
            if (Numinator == 0)
            {
                return new Duration(0, 1);
            }

            var denom = Denominator;
            var num = Numinator;
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





        public static Duration operator +(Duration first, Duration second)
        {
            if (first.Denominator == second.Denominator)
            {
                return new Duration(first.Numinator + second.Numinator, first.Denominator);
            }

            var nominator =
                first.Numinator * second.Denominator +
                first.Denominator * second.Numinator;

            var denominator = first.Denominator * second.Denominator;

            return new Duration(nominator, denominator).Simplify();
        }

        public static bool operator <(Duration first, Duration second)
        {
            return first.Decimal < second.Decimal;
        }

        public static bool operator >(Duration first, Duration second)
        {
            return first.Decimal > second.Decimal;
        }

        public static bool operator <=(Duration first, decimal second)
        {
            return first.Decimal <= second;
        }

        public static bool operator >=(Duration first, decimal second)
        {
            return first.Decimal >= second;
        }

        public static implicit operator Position(Duration duration) => new(duration.Numinator, duration.Denominator);
    }
}
