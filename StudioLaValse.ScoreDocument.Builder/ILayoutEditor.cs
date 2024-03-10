using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// An interface that allows an element to edit it's attached layout.
    /// </summary>
    /// <typeparam name="TLayout"></typeparam>
    public interface ILayoutEditor<TLayout> where TLayout : ILayoutElement<TLayout>
    {
        /// <summary>
        /// Read the layout.
        /// </summary>
        /// <returns></returns>
        TLayout ReadLayout();
        /// <summary>
        /// Apply a new layout to this element.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(TLayout layout);
        /// <summary>
        /// Removes the applied layout from this element.
        /// </summary>
        void RemoveLayout();
    }
}
