using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IGraceChordReader : IGraceChord<IGraceNoteReader, IGraceGroupReader>, IHasLayout<IChordLayout>
    {

    }
}
