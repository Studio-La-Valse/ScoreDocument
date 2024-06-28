using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a measure block editor.
    /// </summary>
    public interface IMeasureBlock : IMeasureBlockLayout, IChordContainer<IChord, INote>, IPositionElement, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <param name="pitches"></param>
        void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches);

        /// <summary>
        /// Try to read the next block in the <see cref="IMeasureBlockChain"/>.
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        bool TryReadNext([NotNullWhen(true)] out IMeasureBlock? right);

        /// <summary>
        /// Try to read the previous block in the <see cref="IMeasureBlockChain"/>.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlock? left);
    }
}
