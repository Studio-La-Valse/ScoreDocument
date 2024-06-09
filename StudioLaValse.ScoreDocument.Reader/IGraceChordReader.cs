using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IGraceChordReader : INoteContainerReader<IGraceNoteReader>, IGraceChord<IGraceNoteReader, IGraceGroupReader>
    {

    }
}
