using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual rest factory.
    /// </summary>
    public class VisualRestFactory : IVisualRestFactory
    {
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="glyphLibrary"></param>
        public VisualRestFactory(IGlyphLibrary glyphLibrary)
        {
            this.glyphLibrary = glyphLibrary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IChord chord, double canvasLeft, double canvasTop)
        {
            var staffIndex = chord.StaffIndex;
            var lineIndex = chord.Line;
            var offsetDots = lineIndex % 2 == 0;

            return new VisualRest(chord, canvasLeft, canvasTop, offsetDots, glyphLibrary);
        }
    }
}
