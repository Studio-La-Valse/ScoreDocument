namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// The base interface for staff groups.
    /// </summary>
    public interface IStaffGroup<TStaff, TRibbon, TInstrumentMeasure> 
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
        TRibbon InstrumentRibbon { get; }

        /// <summary>
        /// Enumerate the staves.
        /// </summary>
        IEnumerable<TStaff> EnumerateStaves();
        /// <summary>
        /// Enuemrate the measures.
        /// </summary>
        IEnumerable<TInstrumentMeasure> EnumerateMeasures();
    }
}
