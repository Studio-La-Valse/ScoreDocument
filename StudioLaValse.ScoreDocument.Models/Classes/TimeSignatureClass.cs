using StudioLaValse.ScoreDocument.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class TimeSignatureClass
    {
        [Range(1, Constants.SmallestStep)]
        public required int Numerator { get; set; }

        [PowerOfTwo]
        [Range(2, Constants.SmallestStep)]
        public required int Denominator { get; set; }
    }
}