using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IInstrumentMeasureLayout
    {
        IEnumerable<ClefChange> ClefChanges { get; }
        KeySignature KeySignature { get; }

        double? GetPaddingBottom(int staffIndex);

        public double? PaddingBottom { get; }
        public bool Collapsed { get; }
        public int? NumberOfStaves { get; }
    }
}