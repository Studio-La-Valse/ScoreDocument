namespace StudioLaValse.ScoreDocument.Layout
{
    public class MeasureElementLayout : IMeasureElementLayout
    {
        public AccidentalDisplay ForceAccidental { get; }
        public int StaffIndex { get; }
        public double XOffset { get; }

        public MeasureElementLayout(int staffIndex = 0, double xOffset = 0, AccidentalDisplay forceAccidental = AccidentalDisplay.Default)
        {
            StaffIndex = staffIndex;
            XOffset = xOffset;
            ForceAccidental = forceAccidental;
        }

        public IMeasureElementLayout Copy()
        {
            return new MeasureElementLayout(StaffIndex, XOffset, ForceAccidental);
        }
    }
}