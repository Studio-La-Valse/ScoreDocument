using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a measure block reader.
    /// </summary>
    public interface IMeasureBlockReader : IMeasureBlock
    {
        /// <summary>
        /// Reads the contents of the measure block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IChordReader> ReadChords();
        /// <summary>
        /// Reads the layout of the measure block.
        /// </summary>
        /// <returns></returns>
        INoteGroupLayout ReadLayout();

        /// <summary>
        /// Tries to read the next measure block.
        /// </summary>
        /// <param name="right"></param>
        /// <returns><see langword="true"/> if the next measure exists.</returns>
        bool TryReadNext([NotNullWhen(true)] out IMeasureBlockReader? right);
        /// <summary>
        /// Tries to read the previous measure block.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns><see langword="true"/> if the previous measure exists.</returns>
        bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlockReader? previous);
    }
}
