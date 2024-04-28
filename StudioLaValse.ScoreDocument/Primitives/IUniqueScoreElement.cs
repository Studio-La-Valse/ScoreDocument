namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Defines a score element that has an ID that can be queried to compare two score elements.
    /// </summary>
    public interface IUniqueScoreElement
    {
        /// <summary>
        /// The element id of the element. Note that this value is constant throughout the lifecycle of this element. Use this value to compare two different instances of the same element. 
        /// </summary>
        int Id { get; }
    }
}
