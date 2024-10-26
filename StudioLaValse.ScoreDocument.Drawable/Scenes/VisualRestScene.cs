using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual rest factory.
    /// </summary>
    public class VisualRestScene : IVisualRestScene
    {
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="glyphLibrary"></param>
        public VisualRestScene(IGlyphLibrary glyphLibrary)
        {
            this.glyphLibrary = glyphLibrary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Create(IChord chord, double canvasLeft, double canvasTop)
        {
            var lineIndex = chord.Line;
            var offsetDots = lineIndex % 2 == 0;

            return new VisualRest(chord, canvasLeft, canvasTop, offsetDots, glyphLibrary);
        }
    }
}
