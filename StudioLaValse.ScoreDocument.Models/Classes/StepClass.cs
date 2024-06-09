using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class StepClass
    {
        [Range(0, 12)]
        public required int StepsFromC { get; set; }

        [Range(-3, 4)]
        public required int Shifts { get; set; }
    }
}
