using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStaffSystemReader : IStaffSystem<IStaffGroupReader, IScoreMeasureReader>, IHasLayout<IStaffSystemLayout>
    {

    }
}
