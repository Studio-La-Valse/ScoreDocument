namespace StudioLaValse.ScoreDocument.Models.Classes
{
    public class PitchClass
    {
        public required StepClass Step { get; set; }

        public required int Octave { get; set; }
    }
}