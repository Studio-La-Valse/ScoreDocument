using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// The default implementation of the equality comparer for the <see cref="Position"/> type.
    /// </summary>
    public class PositionComparer : IEqualityComparer<Position>
    {
        /// <summary>
        /// Compares the two positions. Will throw an exception if either is null.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool Equals(Position? x, Position? y)
        {
            if (x == null || y == null)
            {
                throw new InvalidOperationException();
            }

            return x.Decimal == y.Decimal;
        }

        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] Position obj)
        {
            return obj.Decimal.GetHashCode();
        }
    }
}
