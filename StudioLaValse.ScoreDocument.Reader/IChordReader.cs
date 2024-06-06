using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IChordReader : IChord<INoteReader, IGraceGroupReader>, INoteContainerReader<INoteReader>, IUniqueScoreElement
    {

    }
}
