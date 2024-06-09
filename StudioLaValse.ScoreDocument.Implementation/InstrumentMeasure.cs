namespace StudioLaValse.ScoreDocument.Implementation
{
    public class InstrumentMeasure : ScoreElement, IUniqueScoreElement, IMementoElement<InstrumentMeasureModel>
    {
        private readonly Dictionary<int, MeasureBlockChain> blockChains;
        private readonly ScoreMeasure scoreMeasure;
        private readonly InstrumentRibbon hostRibbon;
        private readonly ScoreDocumentStyleTemplate documentStyleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;



        public int MeasureIndex =>
            scoreMeasure.IndexInScore;
        public int RibbonIndex =>
            hostRibbon.IndexInScore;
        public TimeSignature TimeSignature =>
            scoreMeasure.TimeSignature;
        public Instrument Instrument =>
            hostRibbon.Instrument;
        public ScoreMeasure ScoreMeasure =>
            scoreMeasure;


        public ScoreMeasure HostMeasure => scoreMeasure;
        public AuthorInstrumentMeasureLayout AuthorLayout { get; }
        public UserInstrumentMeasureLayout UserLayout { get; }

        internal InstrumentMeasure(
            ScoreMeasure scoreMeasure,
            InstrumentRibbon hostRibbon,
            ScoreDocumentStyleTemplate documentStyleTemplate,
            AuthorInstrumentMeasureLayout authorLayout,
            UserInstrumentMeasureLayout userLayout,
            IKeyGenerator<int> keyGenerator,
            Guid guid) : base(keyGenerator, guid)
        {
            this.scoreMeasure = scoreMeasure;
            this.hostRibbon = hostRibbon;
            this.documentStyleTemplate = documentStyleTemplate;
            this.keyGenerator = keyGenerator;

            blockChains = [];

            AuthorLayout = authorLayout;
            UserLayout = userLayout;
        }





        public MeasureBlockChain GetBlockChainOrThrowCore(int voice)
        {
            return blockChains.TryGetValue(voice, out var chain) ? chain : throw new Exception($"No voice {voice} found.");
        }


        public void Clear()
        {
            blockChains.Clear();
            AuthorLayout.Restore();
            UserLayout.Restore();
        }
        public void RemoveVoice(int voice)
        {
            _ = blockChains.Remove(voice);
        }
        public void AddVoice(int voice)
        {
            var guid = Guid.NewGuid();
            _ = blockChains.TryAdd(voice, new MeasureBlockChain(this, documentStyleTemplate, voice, keyGenerator, guid));
        }





        public IEnumerable<int> EnumerateVoices()
        {
            return blockChains.Select(c => c.Key);
        }






        public bool TryReadPrevious([NotNullWhen(true)] out InstrumentMeasure? previous)
        {
            previous = null;
            if (scoreMeasure.TryReadPrevious(out var previousScoreMeasure))
            {
                previous = previousScoreMeasure.GetMeasureCore(RibbonIndex);
                return true;
            }
            return false;
        }
        public bool TryReadNext([NotNullWhen(true)] out InstrumentMeasure? next)
        {
            next = null;
            if (scoreMeasure.TryReadNext(out var nextScoreMeasure))
            {
                next = nextScoreMeasure.GetMeasureCore(RibbonIndex);
                return true;
            }
            return false;
        }




        public InstrumentMeasureModel GetMemento()
        {
            return new InstrumentMeasureModel
            {
                Id = Guid,
                ScoreMeasureIndex = MeasureIndex,
                InstrumentRibbonIndex = RibbonIndex,
                MeasureBlocks = blockChains.Values.SelectMany(v => v.GetBlocksCore()).Select(b => b.GetMemento()).ToList(),
                Layout = UserLayout.GetMemento(),
                ClefChanges = AuthorLayout._ClefChanges.Select(e => e.Convert()).ToList(),
                Collapsed = AuthorLayout._Collapsed.Field,
                NumberOfStaves = AuthorLayout._NumberOfStaves.Field,
                PaddingBottom = AuthorLayout._PaddingBottom.Field,
                StaffPaddingBottom = AuthorLayout._PaddingBottomForStaves.DeepCopy()
            };
        }
        public void ApplyMemento(InstrumentMeasureModel memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);

            foreach (var voiceGroup in memento.MeasureBlocks.GroupBy(e => e.Voice))
            {
                var voice = voiceGroup.Key;
                AddVoice(voice);
                var blockChain = GetBlockChainOrThrowCore(voice);

                blockChain.Clear();
                foreach(var block in voiceGroup)
                {
                    blockChain.AppendCore(block.RythmicDuration.Convert(), block.Id, block.Layout?.Id ?? Guid.NewGuid());
                    var newBlock = blockChain.GetBlocksCore().Last();
                    newBlock.ApplyMemento(block);
                }
            }
        }
    }
}
