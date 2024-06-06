using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a measure block reader.
    /// </summary>
    public interface IMeasureBlockReader : IChordContainerReader<IChordReader>, IMeasureBlock<IChordReader>, IScoreElementReader<IMeasureBlockLayout>, IPositionElement
    {

    }
}
