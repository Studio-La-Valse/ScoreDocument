using StudioLaValse.ScoreDocument.Layout.Models;

namespace StudioLaValse.ScoreDocument.Layout
{
    public class MeasureElementContainerLayout : IMeasureElementContainerLayout
    {
        public double StemLength { get; }
        public double XOffset { get; }
        public Dictionary<int, BeamType> Beams { get; }

        public MeasureElementContainerLayout(double xOffset = 0, double stemLength = 4)
        {
            StemLength = stemLength;
            XOffset = xOffset;
            Beams = [];
        }
        public MeasureElementContainerLayout(Dictionary<int, BeamType> beams, double xOffset=0, double stemLength = 4)
        {
            StemLength = stemLength;
            XOffset = xOffset;
            Beams = beams;
        }

        public IMeasureElementContainerLayout Copy()
        {
            var beams = new Dictionary<int, BeamType>();
            foreach(var entry in Beams)
            {
                beams.Add(entry.Key, entry.Value);
            }

            return new MeasureElementContainerLayout(beams, XOffset, StemLength);
        }
    }
}