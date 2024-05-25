using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class ClefChange : IEquatable<ClefChange>
    {
        public required string Clef { get; set; }

        [Range(0, int.MaxValue)]
        public required int StaffIndex { get; set; }

        public required Position Position { get; set; }

        public bool Equals(ClefChange? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Clef == Clef && other.Position == Position && other.StaffIndex == StaffIndex;
        }
    }
}