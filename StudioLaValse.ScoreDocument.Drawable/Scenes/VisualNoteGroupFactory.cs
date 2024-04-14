using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note group factory.
    /// </summary>
    public class VisualNoteGroupFactory : IVisualNoteGroupFactory
    {
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IVisualBeamBuilder visualBeamBuilder;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="noteFactory"></param>
        /// <param name="restFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualNoteGroupFactory(IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            visualBeamBuilder = new VisualBeamBuilder();
        }
        /// <inheritdoc/>
        public BaseContentWrapper Build(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, IInstrumentMeasureReader instrumentMeasure, double canvasTopStaffGroup, double canvasLeft, double allowedSpace, double lineSpacing, ColorARGB colorARGB)
        {
            var scoreLayout = scoreLayoutDictionary.DocumentLayout();
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = 1d;
            if (scoreLayout.InstrumentScales.TryGetValue(staffGroup.Instrument, out var value))
            {
                instrumentScale = value;
            }
            return new VisualNoteGroup(noteGroup, staffGroup, instrumentMeasure, canvasTopStaffGroup, canvasLeft, allowedSpace, lineSpacing, scoreScale, instrumentScale, noteFactory, restFactory, visualBeamBuilder, colorARGB, scoreLayoutDictionary);
        }
    }
}
