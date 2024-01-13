namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IInstrumentMeasure : IUniqueScoreElement
    {
        TimeSignature TimeSignature { get; }
        Instrument Instrument { get; }

        IEnumerable<int> EnumerateVoices();
        IEnumerable<IMeasureBlock> EnumerateBlocks(int voice);
    }
}
