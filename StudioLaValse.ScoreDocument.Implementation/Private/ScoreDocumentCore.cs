using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class ScoreDocumentCore : ScoreElement, IUniqueScoreElement, IMementoElement<ScoreDocumentMemento>
    {
        private readonly ScoreContentTable contentTable;
        private readonly ScoreDocumentStyleTemplate styleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;


        public int NumberOfMeasures =>
            contentTable.Width;
        public int NumberOfInstruments =>
            contentTable.Height;



        public AuthorScoreDocumentLayout AuthorLayout { get; }
        public UserScoreDocumentLayout UserLayout { get; private set; }



        public ScoreDocumentCore(ScoreContentTable contentTable,
                                 ScoreDocumentStyleTemplate styleTemplate,
                                 AuthorScoreDocumentLayout primaryLayout,
                                 UserScoreDocumentLayout secondaryLayout,
                                 IKeyGenerator<int> keyGenerator,
                                 Guid guid) : base(keyGenerator, guid)
        {
            this.contentTable = contentTable;
            this.styleTemplate = styleTemplate;
            this.keyGenerator = keyGenerator;

            AuthorLayout = primaryLayout;
            UserLayout = secondaryLayout;
        }




        public void AddInstrumentRibbon(Instrument instrument)
        {
            var layoutId = Guid.NewGuid();
            var ribbonId = Guid.NewGuid();
            var instrumentRibbon = CreateInstrumentRibbonCore(instrument, ribbonId, layoutId);
            contentTable.AddInstrumentRibbon(instrumentRibbon);
        }
        public InstrumentRibbon CreateInstrumentRibbonCore(Instrument instrument, Guid ribbonId, Guid layoutId)
        {
            var primaryLayout = new AuthorInstrumentRibbonLayout(instrument, styleTemplate, ribbonId, UserLayout);
            var secondaryLayout = new UserInstrumentRibbonLayout(primaryLayout, layoutId, UserLayout);
            var instrumentRibbon = new InstrumentRibbon(this, instrument, primaryLayout, secondaryLayout, keyGenerator, ribbonId);
            return instrumentRibbon;
        }

        public void RemoveInstrumentRibbon(int indexInScore)
        {
            contentTable.RemoveInstrumentRibbon(indexInScore);
        }

        public int IndexOf(ScoreMeasure scoreMeasure)
        {
            return contentTable.IndexOf(scoreMeasure);
        }
        public int IndexOf(InstrumentRibbon instrumentRibbon)
        {
            return contentTable.IndexOf(instrumentRibbon);
        }


        public ScoreMeasure CreateScoreMeasureCore(Guid guid, Guid layoutGuid, TimeSignature? timeSignature = null)
        {
            if (!contentTable.RowHeaders.Any())
            {
                throw new Exception("Please construct an instrument ribbon first");
            }

            var previousElement = contentTable.ColumnHeaders.LastOrDefault();
            timeSignature ??= previousElement is not null ?
                    previousElement.TimeSignature :
                    new TimeSignature(4, 4);

            var layout = new AuthorScoreMeasureLayout(styleTemplate.ScoreMeasureStyleTemplate, styleTemplate);
            var secondaryLayout = new UserScoreMeasureLayout(layoutGuid, layout, styleTemplate.ScoreMeasureStyleTemplate, styleTemplate);
            ScoreMeasure scoreMeasure = new(this, timeSignature, layout, secondaryLayout, styleTemplate, keyGenerator, guid);
            return scoreMeasure;
        }
        public void AppendScoreMeasure(TimeSignature? timeSignature = null)
        {
            var scoreMeasure = CreateScoreMeasureCore(Guid.NewGuid(), Guid.NewGuid(), timeSignature);
            contentTable.AddScoreMeasure(scoreMeasure);
        }
        public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
        {
            var scoreMeasure = CreateScoreMeasureCore(Guid.NewGuid(), Guid.NewGuid(), timeSignature);
            contentTable.InsertScoreMeasure(scoreMeasure, index);
        }
        public void RemoveScoreMeasure(int indexInScore)
        {
            contentTable.RemoveScoreMeasure(indexInScore);
        }


        public void Clear()
        {
            while (contentTable.Height > 0)
            {
                contentTable.RemoveInstrumentRibbon(0);
            }

            while (contentTable.Width > 0)
            {
                contentTable.RemoveScoreMeasure(0);
            }
        }


        public IEnumerable<ScoreMeasure> EnumerateMeasuresCore()
        {
            return contentTable.ColumnHeaders;
        }
        public IEnumerable<InstrumentMeasure> EnumerateScoreMeasuresCore(ScoreMeasure scoreMeasure)
        {
            return contentTable.GetInstrumentMeasuresInScoreMeasure(scoreMeasure.IndexInScore);
        }
        public IEnumerable<InstrumentMeasure> EnumerateScoreMeasuresCore(InstrumentRibbon instrumentRibbon)
        {
            return contentTable.GetInstrumentMeasuresInInstrumentRibbon(instrumentRibbon.IndexInScore);
        }
        public InstrumentMeasure GetMeasureCore(int scoreMeasureIndex, int ribbonIndex)
        {
            return contentTable.GetInstrumentMeasure(scoreMeasureIndex, ribbonIndex);
        }
        public IEnumerable<InstrumentRibbon> EnumerateRibbonsCore()
        {
            return contentTable.RowHeaders;
        }



        public ScoreMeasure GetScoreMeasureCore(int indexInScore)
        {
            return contentTable.ScoreMeasureAt(indexInScore);
        }
        public InstrumentRibbon GetInstrumentRibbonCore(int indexInScore)
        {
            return contentTable.InstrumentRibbonAt(indexInScore);
        }


        public ScoreDocumentModel GetModel()
        {
            return new ScoreDocumentModel
            {
                Id = Guid,
                InstrumentRibbons = EnumerateRibbonsCore().Select(e => e.GetModel()).ToList(),
                ScoreMeasures = EnumerateMeasuresCore().Select(e => e.GetModel()).ToList(),
            };
        }

        public void ApplyModel(ScoreDocumentModel model)
        {
            var fakeDictionary = new ScoreDocumentLayoutDictionary()
            {
                ScoreDocumentLayout = ScoreDocumentLayoutModel.Create(Guid),
                ChordLayouts = [],
                GraceChordLayouts = [],
                GraceGroupLayouts = [],
                GraceNoteLayouts = [],
                InstrumentMeasureLayouts = [],
                InstrumentRibbonLayouts = [],
                MeasureBlockLayouts = [],
                NoteLayouts = [],
                ScoreMeasureLayouts = [],
            };

            var memento = model.Join(fakeDictionary);
            ApplyMemento(memento);
        }

        public ScoreDocumentLayoutModel GetLayoutModel()
        {
            var model = new ScoreDocumentLayoutModel()
            {
                Id = UserLayout.Id,
                ScoreDocumentId = Guid
            };
            return model;
        }

        public ScoreDocumentLayoutDictionary GetLayoutDictionary()
        {
            return GetMemento().ExtractLayout();
        }

        public void ApplyLayoutDictionary(ScoreDocumentLayoutDictionary layoutDictionary)
        {
            var memento = GetModel().Join(layoutDictionary);
            ApplyMemento(memento);
        }

        public ScoreDocumentMemento GetMemento()
        {
            return new ScoreDocumentMemento
            {
                Id = Guid,
                InstrumentRibbons = EnumerateRibbonsCore().Select(e => e.GetMemento()).ToList(),
                ScoreMeasures = EnumerateMeasuresCore().Select(e => e.GetMemento()).ToList(),
                Layout = GetLayoutModel()
            };
        }

        public void ApplyMemento(ScoreDocumentMemento memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);

            foreach (var instrumentMemento in memento.InstrumentRibbons)
            {
                var instrumentRibbon = CreateInstrumentRibbonCore(instrumentMemento.Instrument.Convert(), instrumentMemento.Id, Guid.NewGuid());
                contentTable.AddInstrumentRibbon(instrumentRibbon);

                instrumentRibbon.ApplyMemento(instrumentMemento);
            }

            foreach (var scoreMeasureMemento in memento.ScoreMeasures)
            {
                var scoreMeasure = CreateScoreMeasureCore(scoreMeasureMemento.Id, Guid.NewGuid(), scoreMeasureMemento.TimeSignature.Convert());
                contentTable.AddScoreMeasure(scoreMeasure);

                scoreMeasure.ApplyMemento(scoreMeasureMemento);
            }

            UserLayout = new UserScoreDocumentLayout(styleTemplate, memento.Layout.Id);
            UserLayout.ApplyMemento(memento.Layout);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}

