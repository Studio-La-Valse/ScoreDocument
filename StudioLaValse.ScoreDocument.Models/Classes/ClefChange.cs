using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class ClefChange
    {
        public required string Clef { get; set; }

        [Range(0, int.MaxValue)]
        public required int StaffIndex { get; set; }

        public required Position Position { get; set; }
    }
}