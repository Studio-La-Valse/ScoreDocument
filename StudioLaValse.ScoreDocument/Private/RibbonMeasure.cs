using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class RibbonMeasure : ScoreElement, IRibbonMeasureEditor, IRibbonMeasureReader
    {
        private readonly Dictionary<int, MeasureBlockChain> blockChains;
        private readonly ScoreMeasure scoreMeasure;
        private readonly InstrumentRibbon hostRibbon;
        private readonly IKeyGenerator<int> keyGenerator;
        private IRibbonMeasureLayout layout;



        public IChordReader this[Position position, int voice]
        {
            get
            {
                var chain = blockChains[voice];
                var block = chain.Blocks.First(b => b.ContainsPosition(position));
                var container = block.Containers.First(c => c.ContainsPosition(position));
                return container;
            }
        }



        public int MeasureIndex =>
            scoreMeasure.IndexInScore;
        public int RibbonIndex => 
            hostRibbon.IndexInScore;
        public TimeSignature TimeSignature =>
            scoreMeasure.TimeSignature;
        public Instrument Instrument => 
            hostRibbon.Instrument;


        internal RibbonMeasure(ScoreMeasure scoreMeasure, InstrumentRibbon hostRibbon, IRibbonMeasureLayout layout, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.scoreMeasure = scoreMeasure;
            this.hostRibbon = hostRibbon;
            this.keyGenerator = keyGenerator;
            this.layout = layout;

            blockChains = [];
        }



        public void PrependBlock(int voice, Duration duration, bool grace)
        {
            blockChains[voice].Prepend(duration, grace);
        }
        public void AppendBlock(int voice, Duration duration, bool grace)
        {
            blockChains[voice].Append(duration, grace);
        }




        public IScoreMeasureReader ReadMeasureContext() =>
            scoreMeasure;

        public void Clear()
        {
            blockChains.Clear();
        }

        public void ClearVoice(int voice)
        {
            blockChains.Remove(voice);
        }
        public void AddVoice(int voice)
        {
            blockChains.TryAdd(voice, new MeasureBlockChain(this, voice, keyGenerator));
        }


        public IEnumerable<IMeasureBlock> EnumerateBlocks(int voice)
        {
            if (blockChains.TryGetValue(voice, out var chain))
            {
                return chain.Blocks;
            }
            return new List<IMeasureBlockReader>();
        }
        public IEnumerable<IMeasureBlockReader> ReadBlocks(int voice)
        {
            if (blockChains.TryGetValue(voice, out var chain))
            {
                return chain.Blocks;
            }
            return new List<IMeasureBlockReader>();
        }

        public IEnumerable<IMeasureBlockEditor> EditBlocks(int voice)
        {
            if(blockChains.TryGetValue(voice, out var editor))
            {
                return editor.Blocks;
            }
            throw new Exception($"No voice {voice} found.");
        }



        public IEnumerable<int> EnumerateVoices()
        {
            return blockChains.Select(c => c.Key);
        }




        public IRibbonMeasureLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IRibbonMeasureLayout layout)
        {
            this.layout = layout;
        }




        public bool TryReadPrevious([NotNullWhen(true)] out IRibbonMeasureReader? previous)
        {
            previous = null;
            if(scoreMeasure.TryReadPrevious(out var previousScoreMeasure))
            {
                previous = previousScoreMeasure.ReadMeasure(RibbonIndex);
                return true;
            }
            return false;
        }
        public bool TryReadNext([NotNullWhen(true)] out IRibbonMeasureReader? next)
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
