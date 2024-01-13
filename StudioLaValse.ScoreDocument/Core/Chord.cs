namespace StudioLaValse.ScoreDocument.Core
{
    public class Chord
    {
        private readonly Step origin;
        private readonly ChordStructure chordStructure;



        public Chord(Step origin, ChordStructure scaleStructure)
        {
            this.origin = origin;
            this.chordStructure = scaleStructure;
        }


        public IEnumerable<Step> EnumerateSteps()
        {
            foreach (var step in chordStructure.Intervals)
            {
                yield return origin + step;
            }
        }

        public bool Contains(Step step)
        {
            return EnumerateSteps().Any(_step => _step.Equals(step));
        }
    }
}
