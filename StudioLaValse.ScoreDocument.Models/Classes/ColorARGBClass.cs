using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class ColorARGBClass
    {
        [Range(0, 255)]
        public required int R { get; set; }

        [Range(0, 255)]
        public required int G { get; set; }

        [Range(0, 255)]
        public required int B { get; set; }

        [Range(0, 255)]
        public required int A { get; set; }
    }
}