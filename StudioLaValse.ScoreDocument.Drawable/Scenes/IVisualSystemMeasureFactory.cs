using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualSystemMeasureFactory
    {
        BaseContentWrapper CreateContent(IScoreMeasureReader scoreMeasure, IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double width, ColorARGB color);
    }
}
