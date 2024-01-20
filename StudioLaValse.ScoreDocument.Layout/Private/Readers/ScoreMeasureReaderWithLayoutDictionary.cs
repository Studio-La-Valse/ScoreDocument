using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class ScoreMeasureReaderWithLayoutDictionary : IScoreMeasureReader
    {
        private readonly IScoreMeasureReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ScoreMeasureReaderWithLayoutDictionary(IScoreMeasureReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int IndexInScore => source.IndexInScore;

        public bool IsLastInScore => source.IsLastInScore;

        public TimeSignature TimeSignature => source.TimeSignature;

        public int Id => source.Id;

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IScoreMeasureLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IInstrumentMeasureReader ReadMeasure(int ribbonIndex)
        {
            return source.ReadMeasure(ribbonIndex);
        }

        public IEnumerable<IInstrumentMeasureReader> ReadMeasures()
        {
            return source.ReadMeasures().Select(e => e.UseLayout(layoutDictionary));
        }

        public bool TryReadNext([NotNullWhen(true)] out IScoreMeasureReader? next)
        {
            if (source.TryReadNext(out next))
            {
                next = next.UseLayout(layoutDictionary);
                return true;
            }

            return false;
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IScoreMeasureReader? previous)
        {
            if (source.TryReadPrevious(out previous))
            {
                previous = previous.UseLayout(layoutDictionary);
                return true;
            }
            return false;
        }

        public IStaffSystemReader ReadStaffSystemOrigin()
        {
            return source.ReadStaffSystemOrigin().UseLayout(layoutDictionary);
        }

        public IStaffSystem GetStaffSystemOrigin()
        {
            return source.GetStaffSystemOrigin();
        }
    }
}
