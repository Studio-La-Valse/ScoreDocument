using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualInstrumentMeasureFactory
    {
        BaseContentWrapper CreateContent(IRibbonMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, ColorARGB color);
    }
}
