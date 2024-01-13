using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IMeasureBlockReader : IMeasureBlock
    {
        IEnumerable<IChordReader> ReadChords();
        INoteGroupLayout ReadLayout();

        bool TryReadNext([NotNullWhen(true)] out IMeasureBlockReader? right);
        bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlockReader? previous);
    }
}
