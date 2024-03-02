using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a chord reader.
    /// </summary>
    public interface IChordReader : IChord<INoteReader>, IPositionElement, IScoreElementReader
    {
        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<(BeamType beam, PowerOfTwo duration)> ReadBeamTypes();
        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        BeamType? ReadBeamType(PowerOfTwo i);
    }
}
