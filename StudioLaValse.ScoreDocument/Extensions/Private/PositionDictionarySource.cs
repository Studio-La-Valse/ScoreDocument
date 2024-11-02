using StudioLaValse.ScoreDocument.Core.Extensions;
using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Extensions.Private
{
    internal class PositionDictionarySource
    {
        private readonly Dictionary<Position, PositionInMeasure> positions;
        private readonly IEqualityComparer<Position> equalityComparer;

        public PositionInMeasure this[Position position] => positions[position];

        public PositionDictionarySource(IEqualityComparer<Position> equalityComparer)
        {
            positions = new Dictionary<Position, PositionInMeasure>(equalityComparer);
            this.equalityComparer = equalityComparer;
        }
        public PositionInMeasure Get(Position position)
        {
            return positions[position];
        }

        public void Add(Position position, PositionInMeasure positionInMeasure)
        {
            if (positions.ContainsKey(position))
            {
                throw new InvalidOperationException();
            }
            positions[position] = positionInMeasure;
        }
        public IEnumerable<KeyValuePair<Position, PositionInMeasure>> ReadAll()
        {
            return positions;
        }
        public void Override(Position position, PositionInMeasure positionInMeasure)
        {
            if (!positions.ContainsKey(position))
            {
                throw new InvalidOperationException();
            }

            positions[position] = positionInMeasure;
        }
        public bool TryGetValue(Position position, [NotNullWhen(true)] out PositionInMeasure? positionInMeasure)
        {
            return positions.TryGetValue(position, out positionInMeasure);
        }
        public PositionInMeasure GetFirst()
        {
            if (positions.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return positions.OrderBy(e => e.Key.Decimal).First().Value;
        }
        public PositionInMeasure GetLast()
        {
            if (positions.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return positions.OrderByDescending(e => e.Key.Decimal).First().Value;
        }
        public PositionDictionarySource Remap(double canvasLeft, double measureWidth)
        {
            var newDictionary = new PositionDictionarySource(equalityComparer);
            if (positions.Count == 0)
            {
                return newDictionary;
            }

            var originalMin = GetFirst().Position;
            var originalMax = GetLast().GetRight();
            var originalWidth = originalMax - originalMin;

            var originalMinSpace = positions.Min(e => e.Value.SpaceRight);
            var originalMaxSpace = positions.Max(e => e.Value.SpaceRight);

            var canvasRight = canvasLeft + measureWidth;
            var newWidth = canvasRight - canvasLeft;
            foreach (var kv in positions.OrderBy(kv => kv.Key.Decimal))
            {
                var originalPosition = kv.Value.Position;
                var newPosition = originalPosition.Map(originalMin, originalMax, canvasLeft, canvasRight);

                var originalSpaceRight = kv.Value.SpaceRight;
                var newSpaceRight = originalMinSpace == originalMaxSpace ?
                    newWidth / originalWidth * originalMinSpace :
                    originalSpaceRight.Map(originalMinSpace, originalMaxSpace, originalWidth, newWidth);

                var mappedPositionInMeasure = new PositionInMeasure(newPosition, newSpaceRight);
                newDictionary.Add(kv.Key, mappedPositionInMeasure);
            }

            return newDictionary;
        }
        public PositionDictionary AsReadOnly()
        {
            return new PositionDictionary(this);
        }
    }
}
