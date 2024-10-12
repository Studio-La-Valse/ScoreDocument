using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class ScoreMeasure : ScoreElement, IUniqueScoreElement, IMementoElement<ScoreMeasureMemento>
    {
        private readonly ScoreDocumentCore score;
        private readonly ScoreDocumentStyleTemplate scoreDocumentStyleTemplate;

        public TimeSignature TimeSignature { get; }
        public AuthorScoreMeasureLayout AuthorLayout { get; }
        public UserScoreMeasureLayout UserLayout { get; set; }


        public int IndexInScore =>
            score.IndexOf(this);
        public bool IsLastInScore =>
            IndexInScore == score.NumberOfMeasures - 1;
        public ScoreDocumentCore HostDocument =>
            score;



        internal ScoreMeasure(ScoreDocumentCore score,
                              TimeSignature timeSignature,
                              AuthorScoreMeasureLayout layout,
                              UserScoreMeasureLayout secondaryLayout,
                              ScoreDocumentStyleTemplate scoreDocumentStyleTemplate,
                              IKeyGenerator<int> keyGenerator,
                              Guid guid) : base(keyGenerator, guid)
        {
            this.score = score;

            TimeSignature = timeSignature;
            AuthorLayout = layout;
            UserLayout = secondaryLayout;
            this.scoreDocumentStyleTemplate = scoreDocumentStyleTemplate;
        }




        public IEnumerable<InstrumentMeasure> EnumerateMeasuresCore()
        {
            var measures = score.EnumerateScoreMeasuresCore(this);
            return measures;
        }
        public InstrumentMeasure GetMeasureCore(int ribbonIndex)
        {
            return score.GetMeasureCore(IndexInScore, ribbonIndex);
        }
        public bool TryReadPrevious([NotNullWhen(true)] out ScoreMeasure? previous)
        {
            previous = null;
            if (IndexInScore == 0)
            {
                return false;
            }

            try
            {
                previous = score.GetScoreMeasureCore(IndexInScore - 1);
            }
            catch
            {

            }

            return previous is not null;
        }
        public bool TryReadNext([NotNullWhen(true)] out ScoreMeasure? next)
        {
            next = null;
            if (IndexInScore + 1 >= score.NumberOfMeasures)
            {
                return false;
            }

            try
            {
                next = score.GetScoreMeasureCore(IndexInScore + 1);
            }
            catch { }

            return next is not null;
        }


        public ScoreMeasureModel GetModel()
        {
            return new ScoreMeasureModel
            {
                Id = Guid,
                InstrumentMeasures = EnumerateMeasuresCore().Select(e => e.GetModel()).ToList(),
                IndexInScore = IndexInScore,
                TimeSignature = TimeSignature.Convert(),
                KeySignature = AuthorLayout._KeySignature.Field?.Convert(),
                PaddingBottom = AuthorLayout._PaddingBottom.Field,
            };
        }
        public ScoreMeasureLayoutModel GetLayoutModel()
        {
            return new ScoreMeasureLayoutModel()
            {
                Id = UserLayout.Id,
                ScoreMeasureId = Guid,
                KeySignature = UserLayout._KeySignature.Field?.Convert(),
                PaddingBottom = UserLayout._PaddingBottom.Field,
            };
        }
        public ScoreMeasureMemento GetMemento()
        {
            return new ScoreMeasureMemento()
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                InstrumentMeasures = EnumerateMeasuresCore().Select(e => e.GetMemento()).ToList(),
                IndexInScore = IndexInScore,
                TimeSignature = TimeSignature.Convert(),
                KeySignature = AuthorLayout._KeySignature.Field?.Convert(),
                PaddingBottom = AuthorLayout._PaddingBottom.Field,
            };
        }

        public void ApplyMemento(ScoreMeasureMemento memento)
        {
            AuthorLayout.ApplyMemento(memento);

            foreach (var measureMemento in memento.InstrumentMeasures)
            {
                var measure = GetMeasureCore(measureMemento.InstrumentRibbonIndex);
                measure.ApplyMemento(measureMemento);
            }

            var layoutMemento = memento.Layout;
            UserLayout = new UserScoreMeasureLayout(Guid, AuthorLayout, scoreDocumentStyleTemplate.ScoreMeasureStyleTemplate, scoreDocumentStyleTemplate);
            UserLayout.ApplyMemento(layoutMemento);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}
