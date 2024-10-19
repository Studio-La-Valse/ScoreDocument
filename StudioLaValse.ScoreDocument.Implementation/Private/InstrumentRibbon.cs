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
        public UserInstrumentRibbonLayout UserLayout { get; set; }

        public int IndexInScore => score.IndexOf(this);
        public ScoreDocumentCore HostScoreDocument => score;


        public InstrumentRibbon(ScoreDocumentCore score,
                                Instrument instrument,
                                AuthorInstrumentRibbonLayout layout,
                                UserInstrumentRibbonLayout secondaryLayout,
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
                Visibility = AuthorLayout._Collapsed.Field?.ConvertVisibility(),
                DisplayName = AuthorLayout._DisplayName.Field,
                NumberOfStaves = AuthorLayout._NumberOfStaves.Field,
                Scale = AuthorLayout._Scale.Field,
                ZIndex = AuthorLayout._ZIndex.Field,
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
                Visibility = UserLayout._Collapsed.Field?.ConvertVisibility(),
                Scale = UserLayout._Scale.Field,
                ZIndex = UserLayout._ZIndex.Field,
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
                Visibility = AuthorLayout._Collapsed.Field?.ConvertVisibility(),
                DisplayName = AuthorLayout._DisplayName.Field,
                NumberOfStaves = AuthorLayout._NumberOfStaves.Field,
                Scale = AuthorLayout._Scale.Field,
                ZIndex = AuthorLayout._ZIndex.Field,
            };
        }

        public void ApplyMemento(InstrumentRibbonMemento memento)
        {
            AuthorLayout.ApplyMemento(memento);

            UserLayout = new UserInstrumentRibbonLayout(AuthorLayout, memento.Layout.Id, score.UserLayout);
            UserLayout.ApplyMemento(memento.Layout);
        }
    }
}
