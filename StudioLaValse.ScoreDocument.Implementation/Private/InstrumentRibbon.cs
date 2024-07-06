using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class InstrumentRibbon : ScoreElement, IMementoElement<InstrumentRibbonMemento>
    {
        private readonly ScoreDocumentCore score;

        public Instrument Instrument { get; }
        public AuthorInstrumentRibbonLayout AuthorLayout { get; }
        public SecondaryInstrumentRibbonLayout UserLayout { get; set; }

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




        public InstrumentRibbonModel GetModel()
        {
            return new InstrumentRibbonModel
            {
                Id = Guid,
                Instrument = Instrument.Convert(),
                IndexInScore = IndexInScore,
                AbbreviatedName = AuthorLayout._AbbreviatedName.Field,
                Collapsed = AuthorLayout._Collapsed.Field,
                DisplayName = AuthorLayout._DisplayName.Field,
                NumberOfStaves = AuthorLayout._NumberOfStaves.Field,
                Scale = AuthorLayout._Scale.Field,
            };
        }

        public InstrumentRibbonLayoutModel GetLayoutModel()
        {
            return new InstrumentRibbonLayoutModel()
            {
                Id = UserLayout.Id,
                InstrumentRibbonId = Guid,
                AbbreviatedName = UserLayout._AbbreviatedName.Field,
                DisplayName = UserLayout._DisplayName.Field,
                NumberOfStaves = UserLayout._NumberOfStaves.Field,
                Collapsed = UserLayout.Collapsed,
                Scale = UserLayout.Scale
            };
        }

        public InstrumentRibbonMemento GetMemento()
        {
            return new InstrumentRibbonMemento
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                Instrument = Instrument.Convert(),
                IndexInScore = IndexInScore,
                AbbreviatedName = AuthorLayout._AbbreviatedName.Field,
                Collapsed = AuthorLayout._Collapsed.Field,
                DisplayName = AuthorLayout._DisplayName.Field,
                NumberOfStaves = AuthorLayout._NumberOfStaves.Field,
                Scale = AuthorLayout._Scale.Field,
            };
        }

        public void ApplyMemento(InstrumentRibbonMemento memento)
        {
            AuthorLayout.ApplyMemento(memento);

            UserLayout = new SecondaryInstrumentRibbonLayout(AuthorLayout, memento.Layout.Id);
            UserLayout.ApplyMemento(memento.Layout);
        }
    }
}
