namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a unique and persistent element in the score.
    /// </summary>
    public interface IUniqueScoreElement : IEquatable<IUniqueScoreElement>
    {
        /// <summary>
        /// The element id of the element. Note that this value is constant throughout the lifecycle of this element. Use this value to compare two different instances of the same element. 
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The guid of the element. Note that this value is constant outside of the lifecycle of the current application.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// Enumerate the logical children of this element.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IUniqueScoreElement> EnumerateChildren();
    }
}
