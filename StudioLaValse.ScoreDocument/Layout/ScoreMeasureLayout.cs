namespace StudioLaValse.ScoreDocument.Layout
{
    public class ScoreMeasureLayout : IScoreMeasureLayout
    {

        public KeySignature KeySignature { get; init; }
        public double PaddingLeft { get; init; }
        public double PaddingRight { get; init; }
        public double Width { get; init; }
        public bool IsNewSystem { get; init; }


        public ScoreMeasureLayout(KeySignature? keySignature = null, double paddingleft = 10, double paddingright = 5, double width = 100, bool isNewSystem = false)
        {
            KeySignature = keySignature ?? new KeySignature(Step.C, MajorOrMinor.Major);
            PaddingLeft = paddingleft;
            PaddingRight = paddingright;
            Width = width;
            IsNewSystem = isNewSystem;
        }


        public IScoreMeasureLayout Copy()
        {
            return new ScoreMeasureLayout(KeySignature, PaddingLeft, PaddingRight, Width, IsNewSystem);
        }
    }
}