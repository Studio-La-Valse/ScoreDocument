namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An instrument measure layout.
    /// </summary>
    public interface IInstrumentMeasureLayout : ILayout
    {
        /// <summary>
        /// Enumerate the clef changes in the instrument measure.
        /// </summary>
        IEnumerable<ClefChange> EnumerateClefChanges();

        /// <summary>
        /// Add a clefchange to this instrument measure.
        /// </summary>
        /// <param name="clefChange"></param>
        void AddClefChange(ClefChange clefChange);

        /// <summary>
        /// Remove a clef change from the instrument measure.
        /// </summary>
        /// <param name="clefChange"></param>
        void RemoveClefChange(ClefChange clefChange);

        /// <summary>
        /// Clear all clef changes.
        /// </summary>
        void ClearClefChanges();

        /// <summary>
        /// Get the requested padding bottom for the specified staff index.
        /// If no padding is requested, null will be returned.
        /// </summary>
        /// <param name="staffIndex"></param>
        /// <returns></returns>
        double? GetPaddingBottom(int staffIndex);

        /// <summary>
        /// Request a padding at the bottom of this score measure. 
        /// If no other measures in the staff group have made a request, the provided value will be used in the staff group.
        /// If other measures have made a request, the highest value will be used.
        /// When the specified value is null, the request is removed from the staff group.
        /// </summary>
        /// <param name="staffIndex"></param>
        /// <param name="paddingBottom"></param>
        void RequestPaddingBottom(int staffIndex, double? paddingBottom = null);

        /// <summary>
        /// The key signature.
        /// Provided by the host score measure.
        /// </summary>
        ReadonlyTemplateProperty<KeySignature> KeySignature { get; }

        /// <summary>
        /// The distance to the next staff gruop.
        /// </summary>
        TemplateProperty<double?> PaddingBottom { get; }

        /// <summary>
        /// Request the staff group containing this measure to be collapsed. 
        /// To reset this request, set this value to null.
        /// </summary>
        TemplateProperty<bool?> Collapsed { get; }

        /// <summary>
        /// Request the staff group containing to display the specified amount of saves.
        /// If no other instrument measures have made the same request, this value will be displayed.
        /// If other instrument measures have made this request, the highest value will be used.
        /// If no requests have been made in the same staff group, the default amount of staves will be displayed.
        /// Specify null to restore the requested number of staves.
        /// </summary>
        TemplateProperty<int?> NumberOfStaves { get; }
    }
}