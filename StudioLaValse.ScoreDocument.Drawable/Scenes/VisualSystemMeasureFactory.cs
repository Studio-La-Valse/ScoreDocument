using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual system measure factory.
    /// </summary>
    public class VisualSystemMeasureFactory : IVisualSystemMeasureFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="visualInstrumentMeasureFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualSystemMeasureFactory(ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.selection = selection;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreMeasureReader scoreMeasure, IStaffSystem staffSystem, double canvasLeft, double canvasTop, double width, bool firstMeasure, ColorARGB colorARGB)
        {
            return new VisualSystemMeasure(scoreMeasure, staffSystem, canvasLeft, canvasTop, width, firstMeasure, colorARGB, selection, visualInstrumentMeasureFactory, scoreLayoutDictionary);
        }
    }
}
