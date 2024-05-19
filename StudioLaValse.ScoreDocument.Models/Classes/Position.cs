using StudioLaValse.ScoreDocument.Models.Attributes;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class Position
    {
        public required int Numerator { get; set; }

        [PowerOfTwo]
        public required int Denominator { get; set; }
    }
}
