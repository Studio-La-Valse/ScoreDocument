using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using System.Runtime.CompilerServices;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface INoteContainerReader<TNote> : INoteContainer<TNote, IGraceGroupReader>, IScoreElementReader<IChordLayout>
    {

    }
}
