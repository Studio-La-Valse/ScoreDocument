using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemScene : IVisualStaffSystemScene
    {
        private readonly IVisualSystemMeasureScene systemMeasureFactory;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IPositionDictionaryBuilder positionDictionaryBuilder;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="glyphLibrary"></param>
        /// <param name="positionDictionaryBuilder"></param>
        public VisualStaffSystemScene(IVisualSystemMeasureScene systemMeasureFactory, IGlyphLibrary glyphLibrary, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.glyphLibrary = glyphLibrary;
            this.positionDictionaryBuilder = positionDictionaryBuilder;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IStaffSystem staffSystem, double canvasLeft, double canvasTop, double length)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, glyphLibrary, systemMeasureFactory, positionDictionaryBuilder);
        }
    }
}
