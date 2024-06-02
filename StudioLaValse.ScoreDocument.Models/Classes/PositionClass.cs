using StudioLaValse.ScoreDocument.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class PositionClass : IEquatable<PositionClass>
    {
        public required int Numerator { get; set; }

        [PowerOfTwo]
        [Range(1, int.MaxValue)]
        public required int Denominator { get; set; }

        public bool Equals(PositionClass? other)
        {
            if(other is null)
            {
                return false;
            }

            return other.Numerator == Numerator && other.Denominator == Denominator;
        }
    }
}
