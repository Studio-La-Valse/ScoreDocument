namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a score measure editor.
    /// </summary>
    public interface IScoreMeasureEditor : IScoreMeasure
    {
        /// <summary>
        /// Specifies the index in the score.
        /// </summary>
        int IndexInScore { get; }


        /// <summary>
        /// Edit the instrument measures in this score measure.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasureEditor> EditMeasures();
        /// <summary>
        /// Edit the instrument measure at the specified ribbon index.
        /// </summary>
        /// <param name="ribbonIndex"></param>
        /// <returns></returns>
        IInstrumentMeasureEditor EditMeasure(int ribbonIndex);

        /// <summary>
        /// All measures have a staff system origin. 
        /// The score document enumerates <see cref="IStaffSystem"/>s dependent on the <see cref="IScoreMeasureLayout"/>.
        /// Therefore the specified <see cref="IStaffSystemEditor"/> may not be part of the enumerated staff systems.
        /// Call this method to edit the staff system layout regardless.
        /// </summary>
        /// <returns></returns>
        IStaffSystemEditor EditStaffSystemOrigin();

        /// <summary>
        /// Apply the specified layout to the element.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IScoreMeasureLayout layout);
        /// <summary>
        /// Read the layout from the element.
        /// </summary>
        /// <returns></returns>
        IScoreMeasureLayout ReadLayout();

    }
}
