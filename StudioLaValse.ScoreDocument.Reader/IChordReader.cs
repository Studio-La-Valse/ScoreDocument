using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a chord reader.
    /// </summary>
    public interface IChordReader : IChord<INoteReader>, IPositionElement, IScoreElementReader<IChordLayout>
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
