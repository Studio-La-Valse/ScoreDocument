using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base interface for staff groups.
    /// </summary>
    public interface IStaffGroup : IScoreElement, IStaffGroupLayout
    {
        /// <summary>
        /// The instrument of the staff group.
        /// </summary>
        Instrument Instrument { get; }

        /// <summary>
        /// The index in the system.
        /// </summary>
        int IndexInSystem { get; }

        /// <summary>
        /// The associated instrument ribbon.
        /// </summary>
        IInstrumentRibbon InstrumentRibbon { get; }

        /// <summary>
        /// Enumerate the staves.
        /// </summary>
        IEnumerable<IStaff> EnumerateStaves();

        /// <summary>
        /// Enumerate a set number of staves.
        /// </summary>
        /// <param name="numberOfStaves"></param>
        /// <returns></returns>
        IEnumerable<IStaff> EnumerateStaves(int numberOfStaves);

        /// <summary>
        /// Enuemrate the measures.
        /// </summary>
        IEnumerable<IInstrumentMeasure> EnumerateMeasures();


    }
}
