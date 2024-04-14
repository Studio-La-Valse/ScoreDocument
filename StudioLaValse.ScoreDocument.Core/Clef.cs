namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a clef.
    /// </summary>
    public class Clef
    {
        internal int TopMostSharpLine { get; }
        internal int TopMostFlatLine { get; }


        /// <summary>
        /// The treble clef.
        /// </summary>
        public static Clef Treble => new(10, ClefSpecies.G, -1, 1);
        /// <summary>
        /// The soprano clef.
        /// </summary>
        public static Clef Soprano => new(8, ClefSpecies.C, 0, 2);
        /// <summary>
        /// The mezzo soprano clef.
        /// </summary>
        public static Clef MezzoSoprano => new(6, ClefSpecies.C, 0, 0);
        /// <summary>
        /// The alto clef.
        /// </summary>
        public static Clef Alto => new(4, ClefSpecies.C, 0, 2);
        /// <summary>
        /// The tenor clef.
        /// </summary>
        public static Clef Tenor => new(2, ClefSpecies.C, 0, 0);
        /// <summary>
        /// The baritone clef.
        /// </summary>
        public static Clef Baritone => new(0, ClefSpecies.C, 1, 1);
        /// <summary>
        /// The bass clef.
        /// </summary>
        public static Clef Bass => new(-2, ClefSpecies.F, 1, 3);

        /// <summary>
        /// For this clef, the middele c is placed on this line index. The line index starts at the top line of the staff line group, at index 0.
        /// </summary>
        public int AnchorLineMiddleC { get; }
        /// <summary>
        /// The clef species of this clef.
        /// </summary>
        public ClefSpecies ClefSpecies { get; }


        private Clef(int lineForMiddleC, ClefSpecies species, int topMostSharpLine, int topMostFlatLine)
        {
            AnchorLineMiddleC = lineForMiddleC;
            ClefSpecies = species;
            TopMostSharpLine = topMostSharpLine;
            TopMostFlatLine = topMostFlatLine;
        }

        /// <summary>
        /// For this clef, calculate at what line index the specified pitch will be displayed. 
        /// The line index starts at 0, starting at the topmost line of the staff.
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public int LineIndexAtPitch(Pitch pitch)
        {
            var lineIndexAtMiddleC = AnchorLineMiddleC;

            var linesPerOctave = 7;

            return lineIndexAtMiddleC + ((3 - pitch.Octave) * linesPerOctave) + (7 - pitch.Step.StepsFromC);
        }
    }
}
