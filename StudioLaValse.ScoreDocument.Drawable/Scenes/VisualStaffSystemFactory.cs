using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="glyphLibrary"></param>
        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, IGlyphLibrary glyphLibrary)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.glyphLibrary = glyphLibrary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IStaffSystem staffSystem, double canvasLeft, double canvasTop, double length)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, glyphLibrary, systemMeasureFactory);
        }
    }
}
