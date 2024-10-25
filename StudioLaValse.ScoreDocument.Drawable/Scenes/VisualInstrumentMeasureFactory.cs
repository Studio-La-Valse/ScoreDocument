using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual instrument measure factory.
    /// </summary>
    public class VisualInstrumentMeasureFactory : IVisualInstrumentMeasureFactory
    {
        private readonly IVisualNoteGroupFactory noteGroupFactory;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="noteGroupFactory"></param>
        /// <param name="glyphLibrary"></param>
        public VisualInstrumentMeasureFactory(IVisualNoteGroupFactory noteGroupFactory, IGlyphLibrary glyphLibrary)
        {
            this.noteGroupFactory = noteGroupFactory;
            this.glyphLibrary = glyphLibrary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IInstrumentMeasure source, IStaffGroup staffGroup, IReadOnlyDictionary<Position, double> positions, double canvasTop, double canvasLeft, double width)
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
