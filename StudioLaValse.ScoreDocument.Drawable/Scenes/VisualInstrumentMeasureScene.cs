using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual instrument measure factory.
    /// </summary>
    public class VisualInstrumentMeasureScene : IVisualInstrumentMeasureScene
    {
        private readonly IVisualNoteGroupScene noteGroupFactory;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="noteGroupFactory"></param>
        /// <param name="glyphLibrary"></param>
        public VisualInstrumentMeasureScene(IVisualNoteGroupScene noteGroupFactory, IGlyphLibrary glyphLibrary)
        {
            this.noteGroupFactory = noteGroupFactory;
            this.glyphLibrary = glyphLibrary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IInstrumentMeasure source, IStaffGroup staffGroup, IReadOnlyDictionary<Position, double> positions, double canvasTop, double canvasLeft, double width)
        {
            return new VisualStaffGroupMeasure(
                source,
                staffGroup,
                positions,
                canvasTop,
                canvasLeft,
                width,
                glyphLibrary,
                noteGroupFactory);
        }
    }
}
