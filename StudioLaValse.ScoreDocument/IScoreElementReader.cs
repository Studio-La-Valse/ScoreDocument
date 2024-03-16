using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base interface for all score element readers. Ensures all elements have an integer id to compare elements, a guid to persist the element outside of the lifecycle of the application and a collection of children.
    /// </summary>
    public interface IScoreElementReader : IUniqueScoreElement, IScoreEntity, IScoreElement
    {

    }
}
