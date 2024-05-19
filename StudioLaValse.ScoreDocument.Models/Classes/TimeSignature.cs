using StudioLaValse.ScoreDocument.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class TimeSignature
    {
        [Range(1, int.MaxValue)]
        public required int Numerator { get; set; }

        [PowerOfTwo]
        public required int Denominator { get; set; }
    }
}