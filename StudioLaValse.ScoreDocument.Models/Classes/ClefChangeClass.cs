using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class ClefChangeClass : IEquatable<ClefChangeClass>
    {
        public required string Clef { get; set; }

        //8 staves should be more than enough.
        [Range(0, 7)]
        public required int StaffIndex { get; set; }

        public required PositionClass Position { get; set; }

        public bool Equals(ClefChangeClass? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Clef == Clef && other.Position == Position && other.StaffIndex == StaffIndex;
        }
    }
}