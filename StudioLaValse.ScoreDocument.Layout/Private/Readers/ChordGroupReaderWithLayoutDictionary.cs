using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class ChordGroupReaderWithLayoutDictionary : IMeasureBlockReader
    {
        private readonly IMeasureBlockReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ChordGroupReaderWithLayoutDictionary(IMeasureBlockReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public bool Grace => source.Grace;

        public Duration Duration => source.Duration;

        public int Id => source.Id;

        public IEnumerable<IChordReader> ReadChords()
        {
            return source.ReadChords().Select(e => e.UseLayout(layoutDictionary));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public INoteGroupLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public bool TryReadNext([NotNullWhen(true)] out IMeasureBlockReader? right)
        {
            if (source.TryReadNext(out right))
            {
                right = right.UseLayout(layoutDictionary);
                return true;
            }

            return false;
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlockReader? previous)
        {
            if (source.TryReadPrevious(out previous))
            {
                previous = previous.UseLayout(layoutDictionary);
                return true;
            }

            return false;
        }

        public IEnumerable<IChord> EnumerateChords()
        {
            return source.EnumerateChords();
        }
    }
}
