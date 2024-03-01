using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual instrument measure factory.
    /// </summary>
    public class VisualStaffMeasureFactory : IVisualInstrumentMeasureFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IVisualNoteGroupFactory noteGroupFactory;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="noteGroupFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualStaffMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualNoteGroupFactory noteGroupFactory, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.selection = selection;
            this.noteGroupFactory = noteGroupFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IInstrumentMeasureReader source, IStaffGroup staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, bool firstMeasure, ColorARGB color)
        {
            return new VisualStaffGroupMeasure(source, staffGroup, canvasTop, canvasLeft, width, paddingLeft, paddingRight, firstMeasure, color, noteGroupFactory, selection, scoreLayoutDictionary);
        }
    }
}
