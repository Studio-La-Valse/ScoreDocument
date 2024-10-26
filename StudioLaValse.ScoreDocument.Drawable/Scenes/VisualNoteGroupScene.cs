using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note group factory.
    /// </summary>
    public class VisualNoteGroupScene : IVisualNoteGroupScene
    {
        private readonly IVisualNoteScene noteFactory;
        private readonly IVisualRestScene restFactory;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="noteFactory"></param>
        /// <param name="restFactory"></param>
        /// <param name="glyphLibrary"></param>
        public VisualNoteGroupScene(IVisualNoteScene noteFactory, IVisualRestScene restFactory, IGlyphLibrary glyphLibrary)
        {
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.glyphLibrary = glyphLibrary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper Create(IMeasureBlock noteGroup, IStaffGroup staffGroup, IInstrumentMeasure instrumentMeasure, IReadOnlyDictionary<Position, double> positionDictionary, double canvasTopStaffGroup)
        {
            var instrumentScale = staffGroup.InstrumentRibbon.Scale.Value;
            return new VisualNoteGroup(
                noteGroup,
                staffGroup,
                instrumentMeasure,
                positionDictionary,
                canvasTopStaffGroup,
                glyphLibrary,
                noteFactory,
                restFactory,
                this);
        }
    }
}
