namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IRibbonMeasure : IUniqueScoreElement
    {
        TimeSignature TimeSignature { get; }
        Instrument Instrument { get; }

        IEnumerable<int> EnumerateVoices();
        IEnumerable<IMeasureBlock> EnumerateBlocks(int voice);
    }
}
