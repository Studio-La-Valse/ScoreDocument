namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Visibility of score elements.
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// Business as usual.
        /// </summary>
        Visible = 1,
        /// <summary>
        /// Collapse the element.
        /// </summary>
        Collapsed = 0,
        /// <summary>
        /// Completely hide the element.
        /// </summary>
        Hidden = -1
    }
}