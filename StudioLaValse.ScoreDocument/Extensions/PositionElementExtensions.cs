namespace StudioLaValse.ScoreDocument.Extensions
{
    public static class PositionElementExtensions
    {
        public static Fraction ActualDuration(this IPositionElement positionElement)
        {
            if (positionElement.Grace)
            {
                return new Duration(0, 1);
            }

            if (positionElement.Tuplet is null)
            {
                return positionElement.RythmicDuration;
            }

            return positionElement.RythmicDuration.ToActualDuration(positionElement.Tuplet);
        }


        public static Position PositionEnd(this IPositionElement positionElement)
        {
            return positionElement.Position + positionElement.ActualDuration();
        }


        public static bool ContainsPosition(this IPositionElement positionElement, Position position)
        {
            return positionElement.Position.Decimal <= position.Decimal && (positionElement.Position + positionElement.ActualDuration()).Decimal > position.Decimal;
        }
    }
}
