namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Defines a score element that has an ID that can be queried to compare two score elements.
    /// </summary>
    public interface IUniqueScoreElement : IEquatable<IUniqueScoreElement>
    {
        /// <summary>
        /// The element id of the element. 
        /// Note that this value is constant throughout the lifecycle of this element, and may change outside of it. 
        /// Use this value to compare two different instances of the same element. 
        /// </summary>
        int Id { get; }
    }
}
