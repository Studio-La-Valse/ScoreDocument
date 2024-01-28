using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Reader;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class RibbonMeasureReaderWithLayoutDictionary : IInstrumentMeasureReader
    {
        private readonly IInstrumentMeasureReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public RibbonMeasureReaderWithLayoutDictionary(IInstrumentMeasureReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int MeasureIndex => source.MeasureIndex;

        public TimeSignature TimeSignature => source.TimeSignature;

        public int Id => source.Id;

        public int RibbonIndex => source.RibbonIndex;

        public Instrument Instrument => source.Instrument;

        public IMeasureBlockChain BlockChainAt(int voice)
        {
            return source.BlockChainAt(voice);
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return source.EnumerateVoices();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IMeasureBlockChainReader ReadBlockChainAt(int voice)
        {
            return source.ReadBlockChainAt(voice);
        }

        public IInstrumentMeasureLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IScoreMeasureReader ReadMeasureContext()
        {
            return source.ReadMeasureContext().UseLayout(layoutDictionary);
        }

        public bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasureReader? next)
        {
            if (source.TryReadNext(out next))
            {
                next = next.UseLayout(layoutDictionary);
                return true;
            }

            return false;
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasureReader? previous)
        {
            if (source.TryReadPrevious(out previous))
            {
                previous = previous.UseLayout(layoutDictionary);
                return true;
            }
            return false;
        }
    }
}
