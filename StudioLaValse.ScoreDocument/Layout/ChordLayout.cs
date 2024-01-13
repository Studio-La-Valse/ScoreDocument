namespace StudioLaValse.ScoreDocument.Layout
{
    public class ChordLayout : IChordLayout
    {
        public double StemLength { get; }
        public double XOffset { get; }
        public Dictionary<int, BeamType> Beams { get; }

        public ChordLayout(double xOffset = 0, double stemLength = 4)
        {
            StemLength = stemLength;
            XOffset = xOffset;
            Beams = [];
        }
        public ChordLayout(Dictionary<int, BeamType> beams, double xOffset = 0, double stemLength = 4)
        {
            StemLength = stemLength;
            XOffset = xOffset;
            Beams = beams;
        }

        public IChordLayout Copy()
        {
            var beams = new Dictionary<int, BeamType>();
            foreach (var entry in Beams)
            {
                beams.Add(entry.Key, entry.Value);
            }

            return new ChordLayout(beams, XOffset, StemLength);
        }
    }
}