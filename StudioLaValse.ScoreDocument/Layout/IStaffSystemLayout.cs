namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The staff default staff system layout interface.
    /// </summary>
    public interface IStaffSystemLayout
    {
        /// <summary>
        /// Space below the staff system in mm. Not affected by score scale.
        /// </summary>
        double PaddingBottom { get; }
    }
}