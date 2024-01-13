using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.VisualParents;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly ISelection<IUniqueScoreElement> selection;

        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, ISelection<IUniqueScoreElement> selection)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.selection = selection;
        }


        public BaseContentWrapper CreateContent(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, ColorARGB color)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, systemMeasureFactory, color, selection);
        }
    }
}
