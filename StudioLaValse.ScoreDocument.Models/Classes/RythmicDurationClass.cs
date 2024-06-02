using StudioLaValse.ScoreDocument.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class RythmicDurationClass
    {
        [PowerOfTwo]
        [Range(1, int.MaxValue)]
        public required int PowerOfTwo { get; set; }

        [Range(0, int.MaxValue)]
        public required int Dots { get; set; }
    }
}