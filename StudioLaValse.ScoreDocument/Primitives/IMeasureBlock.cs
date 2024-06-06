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
    public interface IMeasureBlock<TChord> : IMeasureBlock
        where TChord : IChord
    {
        /// <summary>
        /// Enumerate the chords in the block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TChord> ReadChords();
    }
}
