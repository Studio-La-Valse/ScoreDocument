using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive measure block.
    /// </summary>
    public interface IMeasureBlock<TChord> : IChordContainer<TChord>, IPositionElement, IUniqueScoreElement
    {
        
    }
}
