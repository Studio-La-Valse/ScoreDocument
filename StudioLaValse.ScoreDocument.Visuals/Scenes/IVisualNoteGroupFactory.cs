using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public interface IVisualNoteGroupFactory
    {
        BaseContentWrapper Build(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double spacing, ColorARGB color);
    }
}
