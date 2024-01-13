namespace StudioLaValse.ScoreDocument.Core
{
    public class Position : Fraction
    {
        public Position(int beats, int mode) : base(beats, mode)
        {

        }


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

            return new Fraction(nominator, denominator).Simplify().ToPosition();
        }

        public static bool operator >(Position right, Position left)
        {
            return right.Decimal > left.Decimal;
        }

        public static bool operator >=(Position right, Position left)
        {
            return right.Decimal >= left.Decimal;
        }

        public static bool operator <(Position right, Position left)
        {
            return right.Decimal < left.Decimal;
        }

        public static bool operator <=(Position right, Position left)
        {
            return right.Decimal <= left.Decimal;
        }
    }
}
