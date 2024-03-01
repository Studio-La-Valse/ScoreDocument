using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.ScoreElements
{
    /// <summary>
    /// Represents a staff group reader.
    /// </summary>
    public interface IStaffGroup : IUniqueScoreElement
    {
        /// <summary>
        /// The instrument of the staffgroup.
        /// </summary>
        Instrument Instrument { get; }

        int IndexInSystem { get; }
        /// <summary>
        /// Reads the host instrument ribbon.
        /// </summary>
        /// <returns></returns>
        IInstrumentRibbonReader InstrumentRibbon { get; }

        /// <summary>
        /// Reads the individual staves in the staffgroup.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaff> EnumerateStaves(int numberOfStaves);
        /// <summary>
        /// Reads the instrument measures in the staff group.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasureReader> EnumerateMeasures();

    }
}
