using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IGraceGroupReader : IGraceGroup<IGraceChordReader>, IHasLayout<IGraceGroupLayout>
    {
        bool TryGetIndex(IGraceChordReader graceChordReader, out int index);
    }
}
