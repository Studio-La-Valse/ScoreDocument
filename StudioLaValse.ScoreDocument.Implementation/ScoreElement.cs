using StudioLaValse.Key;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public abstract class ScoreElement : IUniqueScoreElement
    {
        public int Id { get; }
        public Guid Guid { get; }


        public ScoreElement(IKeyGenerator<int> keyGenerator, Guid guid)
        {
            Id = keyGenerator.Generate();
            Guid = guid;
        }
        public ScoreElement(int id, Guid guid)
        {
            Id = id;
            Guid = guid;
        }

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
