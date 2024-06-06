using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IGraceGroupReader : IChordContainerReader<IGraceChordReader>, IGraceGroup<IGraceChordReader>, IHasLayout<IGraceGroupLayout>, IUniqueScoreElement
    {
        
    }
}
