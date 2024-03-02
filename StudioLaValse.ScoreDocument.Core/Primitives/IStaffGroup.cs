namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a staff group reader.
    /// </summary>
    public interface IStaffGroup
    {
        /// <summary>
        /// The instrument of the staffgroup.
        /// </summary>
        Instrument Instrument { get; }
        /// <summary>
        /// The index of the staffgroup in the system.
        /// </summary>
        int IndexInSystem { get; }
    }

    /// <summary>
    /// Represents a staff group reader.
    /// </summary>
    public interface IStaffGroup<TInstrumentRibbon, TInstrumentMeasure, TStaff> : IStaffGroup where TInstrumentRibbon : IInstrumentRibbon<TInstrumentMeasure>
                                                                                              where TInstrumentMeasure : IInstrumentMeasure
                                                                                              where TStaff : IStaff
    {
        /// <summary>
        /// Reads the host instrument ribbon.
        /// </summary>
        /// <returns></returns>
        TInstrumentRibbon InstrumentRibbon { get; }
        /// <summary>
        /// Reads the individual staves in the staffgroup.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TStaff> EnumerateStaves(int numberOfStaves);
        /// <summary>
        /// Reads the instrument measures in the staff group.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TInstrumentMeasure> EnumerateMeasures();
    }
}
