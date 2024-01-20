namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a persistent, unique score element.
    /// </summary>
    public abstract class ScoreElement : PersistentElement, IUniqueScoreElement
    {
        /// <inheritdoc/>
        public ScoreElement(IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {

        }

        /// <inheritdoc/>
        public int Id => base.ElementId.IntValue;

        /// <inheritdoc/>
        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }
    }
}
