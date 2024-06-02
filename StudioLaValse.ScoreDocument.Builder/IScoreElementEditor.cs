namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a score element that may be edited.
    /// </summary>
    public interface IScoreElementEditor : IScoreElement
    {

    }

    /// <summary>
    /// Represents a score element that may be edited.
    /// </summary>
    public interface IScoreElementEditor<TLayout> : IScoreElementEditor, IScoreElement, IHasLayout<TLayout>
    {
        /// <summary>
        /// Restores the layout to the default layout.
        /// </summary>
        void RemoveLayout();
    }
}
