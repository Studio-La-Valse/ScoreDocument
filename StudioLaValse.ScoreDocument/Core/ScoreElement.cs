namespace StudioLaValse.ScoreDocument.Core
{
    public abstract class ScoreElement : PersistentElement, IUniqueScoreElement
    {
        public ScoreElement(IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {

        }

        public int Id => base.ElementId.IntValue;

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
