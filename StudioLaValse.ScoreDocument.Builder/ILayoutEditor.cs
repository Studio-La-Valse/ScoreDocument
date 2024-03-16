using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an element that is able to alter its own layout.
    /// </summary>
    /// <typeparam name="TLayout"></typeparam>
    public interface ILayoutEditor<TLayout> where TLayout : ILayoutElement<TLayout>
    {
        /// <summary>
        /// Apply the specified layout to the element.
        /// </summary>
        /// <param name="layout"></param>
        void Apply(TLayout layout);
        /// <summary>
        /// Read the element's layout.
        /// </summary>
        /// <returns></returns>
        TLayout ReadLayout();
        /// <summary>
        /// Remove the element's layout.
        /// </summary>
        void RemoveLayout();
    }
}
