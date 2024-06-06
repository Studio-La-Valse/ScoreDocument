using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using System.Runtime.CompilerServices;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IChordReader : IChord<INoteReader, IGraceGroupReader>, IPositionElement, IScoreElementReader<IChordLayout>
    {

    }
}
