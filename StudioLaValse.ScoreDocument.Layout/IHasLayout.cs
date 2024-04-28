namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An element that has a layout.
    /// </summary>
    /// <typeparam name="TLayout"></typeparam>
    public interface IHasLayout<TLayout>
    {
        /// <summary>
        /// Read the layout.
        /// </summary>
        /// <returns></returns>
        TLayout ReadLayout();
    }
}