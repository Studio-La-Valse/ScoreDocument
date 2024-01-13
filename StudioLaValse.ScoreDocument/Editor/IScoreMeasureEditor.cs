namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IScoreMeasureEditor : IScoreMeasure
    {
        int IndexInScore { get; }
        IEnumerable<IRibbonMeasureEditor> EditMeasures();
        IRibbonMeasureEditor EditMeasure(int ribbonIndex);
        void ApplyLayout(IScoreMeasureLayout layout);
        IScoreMeasureLayout ReadLayout();
        IStaffSystemEditor EditStaffSystemOrigin();
    }
}
