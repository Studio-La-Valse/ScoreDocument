namespace StudioLaValse.ScoreDocument.Core
{
    public class Clef
    {
        public static Clef Treble => new Clef(10, ClefSpecies.G, -1, 1);
        public static Clef Soprano => new Clef(8, ClefSpecies.C, 0, 2);
        public static Clef MezzoSoprano => new Clef(6, ClefSpecies.C, 0, 0);
        public static Clef Alto => new Clef(4, ClefSpecies.C, 0, 2);
        public static Clef Tenor => new Clef(2, ClefSpecies.C, 0, 0);
        public static Clef Baritone => new Clef(0, ClefSpecies.C, 1, 1);
        public static Clef Bass => new Clef(-2, ClefSpecies.F, 1, 3);


        public int AnchorLineMiddleC { get; }
        public int TopMostSharpLine { get; }
        public int TopMostFlatLine { get; }
        public ClefSpecies ClefSpecies { get; }


        private Clef(int lineForMiddleC, ClefSpecies species, int topMostSharpLine, int topMostFlatLine)
        {
            AnchorLineMiddleC = lineForMiddleC;
            ClefSpecies = species;
            TopMostSharpLine = topMostSharpLine;
            TopMostFlatLine = topMostFlatLine;
        }
        public static Clef FromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Please provide a valid clef name.");
            }

            return name switch
            {
                "Treble" => Treble,
                "Soprano" => Soprano,
                "MezzoSoprano" => MezzoSoprano,
                "Mezzo Soprano" => MezzoSoprano,
                "Alto" => Alto,
                "Tenor" => Tenor,
                "Baritone" => Baritone,
                "Bass" => Bass,
                _ => throw new NotSupportedException($"{name} is not a recognized clef type")
            };
        }

        public int LineIndexAtPitch(Pitch pitch)
        {
            var lineIndexAtMiddleC = AnchorLineMiddleC;

            var linesPerOctave = 7;

            return lineIndexAtMiddleC + (3 - pitch.Octave) * linesPerOctave + (7 - pitch.Step.StepsFromC);
        }
    }
}
