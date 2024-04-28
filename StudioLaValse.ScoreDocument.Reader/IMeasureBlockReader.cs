using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a measure block reader.
    /// </summary>
    public interface IMeasureBlockReader : IMeasureBlock<IChordReader, IMeasureBlockReader>, IScoreElementReader<IMeasureBlockLayout>
    {

    }
}
