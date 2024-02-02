using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class InstrumentMeasure : ScoreElement, IInstrumentMeasureEditor, IInstrumentMeasureReader
    {
        private readonly Dictionary<int, MeasureBlockChain> blockChains;
        private readonly ScoreMeasure scoreMeasure;
        private readonly InstrumentRibbon hostRibbon;
        private readonly IKeyGenerator<int> keyGenerator;
        private IInstrumentMeasureLayout layout;




        public int MeasureIndex =>
            scoreMeasure.IndexInScore;
        public int RibbonIndex =>
            hostRibbon.IndexInScore;
        public TimeSignature TimeSignature =>
            scoreMeasure.TimeSignature;
        public Instrument Instrument =>
            hostRibbon.Instrument;


        internal InstrumentMeasure(ScoreMeasure scoreMeasure, InstrumentRibbon hostRibbon, IInstrumentMeasureLayout layout, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.scoreMeasure = scoreMeasure;
            this.hostRibbon = hostRibbon;
            this.keyGenerator = keyGenerator;
            this.layout = layout;

            blockChains = [];
        }



        public void PrependBlock(int voice, RythmicDuration duration, bool grace)
        {
            blockChains[voice].Prepend(duration, grace);
        }
        public void AppendBlock(int voice, RythmicDuration duration, bool grace)
        {
            blockChains[voice].Append(duration, grace);
        }




        public IScoreMeasureReader ReadMeasureContext() =>
            scoreMeasure;


        public void Clear()
        {
            blockChains.Clear();
        }
        public void RemoveVoice(int voice)
        {
            blockChains.Remove(voice);
        }
        public void AddVoice(int voice)
        {
            blockChains.TryAdd(voice, new MeasureBlockChain(this, voice, keyGenerator));
        }


        public IMeasureBlockChain BlockChainAt(int voice)
        {
            if (blockChains.TryGetValue(voice, out var chain))
            {
                return chain;
            }
            throw new Exception($"No voice {voice} found.");
        }
        public IMeasureBlockChainReader ReadBlockChainAt(int voice)
        {
            if (blockChains.TryGetValue(voice, out var chain))
            {
                return chain;
            }
            throw new Exception($"No voice {voice} found.");

        }
        public IMeasureBlockChainEditor EditBlockChainAt(int voice)
        {
            if (blockChains.TryGetValue(voice, out var editor))
            {
                return editor;
            }
            throw new Exception($"No voice {voice} found.");
        }



        public IEnumerable<int> EnumerateVoices()
        {
            return blockChains.Select(c => c.Key);
        }




        public IInstrumentMeasureLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IInstrumentMeasureLayout layout)
        {
            this.layout = layout;
        }




        public bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasureReader? previous)
        {
            previous = null;
            if (scoreMeasure.TryReadPrevious(out var previousScoreMeasure))
            {
                previous = previousScoreMeasure.ReadMeasure(RibbonIndex);
                return true;
            }
            return false;
        }
        public bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasureReader? next)
        {
            next = null;
            if (scoreMeasure.TryReadNext(out var nextScoreMeasure))
            {
                next = nextScoreMeasure.ReadMeasure(RibbonIndex);
                return true;
            }
            return false;
        }
    }
}
