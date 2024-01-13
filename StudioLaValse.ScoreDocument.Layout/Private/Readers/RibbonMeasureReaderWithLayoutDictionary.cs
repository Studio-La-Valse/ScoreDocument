using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class RibbonMeasureReaderWithLayoutDictionary : IRibbonMeasureReader
    {
        private readonly IRibbonMeasureReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public RibbonMeasureReaderWithLayoutDictionary(IRibbonMeasureReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int MeasureIndex => source.MeasureIndex;

        public TimeSignature TimeSignature => source.TimeSignature;

        public int Id => source.Id;

        public int RibbonIndex => source.RibbonIndex;

        public Instrument Instrument => source.Instrument;

        public IEnumerable<IMeasureBlock> EnumerateBlocks(int voice)
        {
            return source.EnumerateBlocks(voice);
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return source.EnumerateVoices();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IEnumerable<IMeasureBlockReader> ReadBlocks(int voice)
        {
            return source.ReadBlocks(voice);
        }

        public IRibbonMeasureLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IScoreMeasureReader ReadMeasureContext()
        {
            return source.ReadMeasureContext().UseLayout(layoutDictionary);
        }

        public bool TryReadNext([NotNullWhen(true)] out IRibbonMeasureReader? next)
        {
            if(source.TryReadNext(out next))
            {
                next = next.UseLayout(layoutDictionary);
                return true;
            }

            return false;
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IRibbonMeasureReader? previous)
        {
            if( source.TryReadPrevious(out previous))
            {
                previous = previous.UseLayout(layoutDictionary);
                return true;
            }
            return false;
        }
    }
}
