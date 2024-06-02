using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive measure block.
    /// </summary>
    public interface IMeasureBlock : IScoreElement, IUniqueScoreElement
    {

    }

    /// <inheritdoc/>
    public interface IMeasureBlock<TChord> : IMeasureBlock, IPositionElement where TChord : IChord
    {
        /// <summary>
        /// Enumerate the chords in the block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TChord> ReadChords();
    }

    /// <inheritdoc/>
    public interface IMeasureBlock<TChord, TSelf> : IMeasureBlock<TChord>, IPositionElement where TChord : IChord
                                                                                            where TSelf : IMeasureBlock<TChord, TSelf>
    {
        ///// <summary>
        ///// Tries to read the next measure block.
        ///// </summary>
        ///// <param name="right"></param>
        ///// <returns><see langword="true"/> if the next measure exists.</returns>
        //bool TryReadNext([NotNullWhen(true)] out TSelf? right);
        ///// <summary>
        ///// Tries to read the previous measure block.
        ///// </summary>
        ///// <param name="previous"></param>
        ///// <returns><see langword="true"/> if the previous measure exists.</returns>
        //bool TryReadPrevious([NotNullWhen(true)] out TSelf? previous);
    }
}
