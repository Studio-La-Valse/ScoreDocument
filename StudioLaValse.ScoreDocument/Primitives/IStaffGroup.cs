namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// The base interface for staff groups.
    /// </summary>
    public interface IStaffGroup : IScoreElement
    {
        /// <summary>
        /// The instrument of the staff group.
        /// </summary>
        Instrument Instrument { get; }
        /// <summary>
        /// The index in the system.
        /// </summary>
        int IndexInSystem { get; }
    }

    /// <summary>
    /// The base interface for staff groups.
    /// </summary>
    public interface IStaffGroup<TRibbon> : IStaffGroup where TRibbon : IInstrumentRibbon
    {
        /// <summary>
        /// The associated instrument ribbon.
        /// </summary>
        TRibbon InstrumentRibbon { get; }
    }

    /// <summary>
    /// The base interface for staff groups.
    /// </summary>
    public interface IStaffGroup<TStaff, TRibbon, TInstrumentMeasure> : IStaffGroup<TRibbon> where TStaff : IStaff where TRibbon : IInstrumentRibbon<TInstrumentMeasure> where TInstrumentMeasure : IInstrumentMeasure
    {
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
