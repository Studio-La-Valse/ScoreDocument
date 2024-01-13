namespace StudioLaValse.ScoreDocument.Core
{
    public class Tuplet
    {
        public Duration SourceLength { get; }
        public Duration TargetLength { get; }

        public bool IsRedundant =>
            SourceLength.Decimal == TargetLength.Decimal;


        public Tuplet(Duration sourceLength, Duration targetLength)
        {
            SourceLength = sourceLength;
            TargetLength = targetLength;
        }
    }
}
