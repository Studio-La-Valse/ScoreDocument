using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IInstrumentMeasureLayout
    {
        IEnumerable<ClefChange> ClefChanges { get; }
        KeySignature KeySignature { get; }
    }
}