namespace StudioLaValse.ScoreDocument.Implementation
{
    public class InstrumentRibbon : ScoreElement, IMementoElement<InstrumentRibbonModel>
    {
        private readonly ScoreDocumentCore score;

        public Instrument Instrument { get; }
        public AuthorInstrumentRibbonLayout AuthorLayout { get; }
        public SecondaryInstrumentRibbonLayout UserLayout { get; }

        public int IndexInScore => score.IndexOf(this);
        public ScoreDocumentCore HostScoreDocument => score;


        public InstrumentRibbon(ScoreDocumentCore score,
                                Instrument instrument,
                                AuthorInstrumentRibbonLayout layout,
                                SecondaryInstrumentRibbonLayout secondaryLayout,
                                IKeyGenerator<int> keyGenerator,
                                Guid guid) : base(keyGenerator, guid)
        {
            this.score = score;

            Instrument = instrument;
            AuthorLayout = layout;
            UserLayout = secondaryLayout;
        }


        public InstrumentMeasure GetMeasureCore(int index)
        {
            return score.GetMeasureCore(index, IndexInScore);
        }


        public IEnumerable<InstrumentMeasure> EnumerateMeasuresCore()
        {
            return score.EnumerateScoreMeasuresCore(this);
        }




        public InstrumentRibbonModel GetMemento()
        {
            return new InstrumentRibbonModel
            {
                Id = Guid,
                Instrument = Instrument.Convert(),
                IndexInScore = IndexInScore,
                Layout = UserLayout.GetMemento(),
                AbbreviatedName = AuthorLayout._AbbreviatedName.Field,
                Collapsed = AuthorLayout._Collapsed.Field,
                DisplayName = AuthorLayout._DisplayName.Field,
                NumberOfStaves = AuthorLayout._NumberOfStaves.Field,
                Scale = AuthorLayout._Scale.Field,
            };
        }
        public void ApplyMemento(InstrumentRibbonModel memento)
        {
            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);
        }
    }
}
