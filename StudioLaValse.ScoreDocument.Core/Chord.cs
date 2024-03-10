namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a chord.
    /// </summary>
    public class Chord
    {
        private readonly Step origin;
        private readonly ChordStructure chordStructure;


        /// <summary>
        /// Construct a chord from an origin and a chord structure.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="chordStructure"></param>
        public Chord(Step origin, ChordStructure chordStructure)
        {
            this.origin = origin;
            this.chordStructure = chordStructure;
        }

        /// <summary>
        /// Enumerate the steps in the chord.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Step> EnumerateSteps()
        {
            foreach (Interval step in chordStructure.Intervals)
            {
                yield return origin + step;
            }
        }

        /// <summary>
        /// Returns true when the chord contains the specified step.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public bool Contains(Step step)
        {
            return EnumerateSteps().Any(_step => _step.Equals(step));
        }
    }
}
