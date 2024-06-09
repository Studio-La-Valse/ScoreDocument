using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a note reader.
    /// </summary>
    public interface INoteReader : INote, IScoreElementReader<INoteLayout>, IPositionElement
    {

    }
}
