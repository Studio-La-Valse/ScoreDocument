namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Defines a score entity that can be persisted outside of the lifecycle of the application.
    /// </summary>
    public interface IScoreEntity
    {
        /// <summary>
        /// The Guid of the entity.
        /// </summary>
        Guid Guid { get; }
    }
}