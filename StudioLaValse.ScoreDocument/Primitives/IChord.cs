using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive chord.
    /// </summary>
    public interface IChord : IScoreElement, IUniqueScoreElement
    {

    }

    /// <inheritdoc/>
    public interface IChord<TNote> : IChord 
        where TNote : INote
    {
        /// <summary>
        /// Enumerate the primitives in the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TNote> ReadNotes();

        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes();

        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        BeamType? ReadBeamType(PowerOfTwo i);
    }

    /// <inheritdoc/>
    public interface IChord<TNote, TGrace> : IChord<TNote>
        where TNote : INote
        where TGrace : IGraceGroup
    {
        /// <summary>
        /// Read the grace group preceding this chord.
        /// </summary>
        /// <returns></returns>
        TGrace? ReadGraceGroup();
    }
}
