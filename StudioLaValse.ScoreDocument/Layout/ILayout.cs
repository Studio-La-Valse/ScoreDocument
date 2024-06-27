namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A base interface for all layout elements.
    /// </summary>
    public interface ILayout
    {
        /// <summary>
        /// Reset all (eligable) fields to their default unset value.
        /// </summary>
        void Restore();
    }
}