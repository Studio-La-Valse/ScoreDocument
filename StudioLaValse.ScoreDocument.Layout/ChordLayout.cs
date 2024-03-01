namespace StudioLaValse.ScoreDocument.Layout
{
    public class ChordLayout
    {
        public double XOffset { get; set; }



        public ChordLayout(double xOffset = 0)
        {
            XOffset = xOffset;
        }



       
        public ChordLayout Copy()
        {
            return new ChordLayout(XOffset);
        }
    }
}