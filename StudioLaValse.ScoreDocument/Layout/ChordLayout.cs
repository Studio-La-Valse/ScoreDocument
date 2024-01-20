namespace StudioLaValse.ScoreDocument.Layout
{
    /// <inheritdoc/>
    public class ChordLayout : IChordLayout
    {
        /// <inheritdoc/>
        public double XOffset { get; }
        /// <inheritdoc/>
        public Dictionary<int, BeamType> Beams { get; }

        /// <summary>
        /// Create a default layout.
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="stemLength"></param>
        public ChordLayout(double xOffset = 0, double stemLength = 4)
        {
            XOffset = xOffset;
            Beams = [];
        }
        private ChordLayout(Dictionary<int, BeamType> beams, double xOffset = 0, double stemLength = 4)
        {
            XOffset = xOffset;
            Beams = beams;
        }

        /// <inheritdoc/>
        public IChordLayout Copy()
        {
            var beams = new Dictionary<int, BeamType>();
            foreach (var entry in Beams)
            {
                beams.Add(entry.Key, entry.Value);
            }

            return new ChordLayout(beams, XOffset);
        }
    }
}