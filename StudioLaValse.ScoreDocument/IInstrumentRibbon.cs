using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument ribbon editor.
    /// </summary>
    public interface IInstrumentRibbon : IInstrumentRibbonLayout, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// The index of the isntrument ribbon in the score.
        /// </summary>
        int IndexInScore { get; }

        /// <summary>
        /// The instrument of the instrument ribbon.
        /// </summary>
        Instrument Instrument { get; }

        /// <summary>
        /// Enumerate the measures in the ribbon.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasure> ReadMeasures();

        /// <summary>
        /// Edit the instrument measure at the specified index.
        /// </summary>
        /// <param name="measureIndex"></param>
        /// <returns></returns>
        IInstrumentMeasure ReadMeasure(int measureIndex);
    }
}
