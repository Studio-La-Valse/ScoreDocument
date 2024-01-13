namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IInstrumentMeasureEditor : IInstrumentMeasure
    {
        int MeasureIndex { get; }
        int RibbonIndex { get; }


        IEnumerable<IMeasureBlockEditor> EditBlocks(int voice);


        void Clear();
        void ClearVoice(int voice);
        void AddVoice(int voice);


        void PrependBlock(int voice, Duration duration, bool grace);
        void AppendBlock(int voice, Duration duration, bool grace);


        void ApplyLayout(IInstrumentMeasureLayout layout);
        IInstrumentMeasureLayout ReadLayout();
    }
}
