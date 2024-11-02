using StudioLaValse.ScoreDocument.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class PositionClass : IEquatable<PositionClass>
    {
        [Range(0, Constants.SmallestStep)]
        public required int Numerator { get; set; }

        [PowerOfTwo]
        [Range(1, Constants.SmallestStep)]
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
