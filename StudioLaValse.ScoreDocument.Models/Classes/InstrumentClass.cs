namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class InstrumentClass
    {
        public required string Name { get; set; }

        public required ICollection<string> Clefs { get; set; }
    }
}