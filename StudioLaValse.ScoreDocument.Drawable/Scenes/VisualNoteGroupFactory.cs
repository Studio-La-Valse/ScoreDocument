using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using StudioLaValse.ScoreDocument.Reader;

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
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IVisualBeamBuilder visualBeamBuilder;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="noteFactory"></param>
        /// <param name="restFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <param name="unitToPixelConverter"></param>
        public VisualNoteGroupFactory(IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, IScoreDocumentLayout scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter)
        {
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
            visualBeamBuilder = new VisualBeamBuilder(scoreLayoutDictionary);
        }
        /// <inheritdoc/>
        public BaseContentWrapper Build(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, IInstrumentMeasureReader instrumentMeasure, IReadOnlyDictionary<Position, double> positionDictionary, double canvasTopStaffGroup, double lineSpacing)
        {
            var scoreLayout = scoreLayoutDictionary;
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;
            return new VisualNoteGroup(
                noteGroup,
                staffGroup,
                instrumentMeasure,
                positionDictionary,
                canvasTopStaffGroup,
                lineSpacing,
                scoreScale,
                instrumentScale,
                noteFactory,
                restFactory,
                visualBeamBuilder,
                scoreLayoutDictionary,
                this, 
                unitToPixelConverter);
        }
    }
}
