namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The base interface for all layout elements.
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    public interface ILayoutElement<TSelf> where TSelf : ILayoutElement<TSelf>
    {
        /// <summary>
        /// Copy this element to a new instance.
        /// </summary>
        /// <returns></returns>
        TSelf Copy();
    }
}