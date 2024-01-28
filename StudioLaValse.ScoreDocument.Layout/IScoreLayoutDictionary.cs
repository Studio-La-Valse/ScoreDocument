using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The score layout dictionary interface.
    /// </summary>
    public interface IScoreLayoutDictionary
    {
        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <returns></returns>
        IScoreDocumentLayout GetOrCreate(IScoreDocument scoreDocument);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <param name="layout"></param>
        void Assign(IScoreDocument scoreDocument, IScoreDocumentLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IMeasureElementLayout GetOrCreate(INote element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="layout"></param>
        void Assign(INote element, IMeasureElementLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IChordLayout GetOrCreate(IChord element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="chord"></param>
        /// <param name="layout"></param>
        void Assign(IChord chord, IChordLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        INoteGroupLayout GetOrCreate(IMeasureBlock element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="measureBlock"></param>
        /// <param name="layout"></param>
        void Assign(IMeasureBlock measureBlock, INoteGroupLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IInstrumentMeasureLayout GetOrCreate(IInstrumentMeasure element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="layout"></param>
        void Assign(IInstrumentMeasure ribbonMeasure, IInstrumentMeasureLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IInstrumentRibbonLayout GetOrCreate(IInstrumentRibbon element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="instrumentRibbon"></param>
        /// <param name="layout"></param>
        void Assign(IInstrumentRibbon instrumentRibbon, IInstrumentRibbonLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IScoreMeasureLayout GetOrCreate(IScoreMeasure element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <param name="layout"></param>
        void Assign(IScoreMeasure scoreMeasure, IScoreMeasureLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IStaffLayout GetOrCreate(IStaff element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="staffReader"></param>
        /// <param name="layout"></param>
        void Assign(IStaff staffReader, IStaffLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IStaffGroupLayout GetOrCreate(IStaffGroup element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="layout"></param>
        void Assign(IStaffGroup staffGroup, IStaffGroupLayout layout);



        /// <summary>
        /// Get or create the layout for the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IStaffSystemLayout GetOrCreate(IStaffSystem element);
        /// <summary>
        /// Assign the specified layout to the specified element.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="layout"></param>
        void Assign(IStaffSystem staffSystem, IStaffSystemLayout layout);
    }
}
