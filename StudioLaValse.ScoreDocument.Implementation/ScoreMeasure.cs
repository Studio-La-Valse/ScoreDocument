namespace StudioLaValse.ScoreDocument.Implementation
{
    public class ScoreMeasure : ScoreElement, IUniqueScoreElement, IMementoElement<ScoreMeasureModel>
    {
        private readonly ScoreDocumentCore score;


        public TimeSignature TimeSignature { get; }
        public AuthorScoreMeasureLayout AuthorLayout { get; }
        public UserScoreMeasureLayout UserLayout { get; }


        public int IndexInScore =>
            score.IndexOf(this);
        public bool IsLastInScore =>
            IndexInScore == score.NumberOfMeasures - 1;
        public ScoreDocumentCore HostDocument =>
            score;



        internal ScoreMeasure(ScoreDocumentCore score,
                              TimeSignature timeSignature,
                              ScoreDocumentStyleTemplate styleTemplate,
                              AuthorScoreMeasureLayout layout,
                              UserScoreMeasureLayout secondaryLayout,
                              IKeyGenerator<int> keyGenerator,
                              Guid guid) : base(keyGenerator, guid)
        {
            this.score = score;

            TimeSignature = timeSignature;
            AuthorLayout = layout;
            UserLayout = secondaryLayout;
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


        public ScoreMeasureModel GetMemento()
        {
            return new ScoreMeasureModel
            {
                Id = Guid,
                Layout = UserLayout.GetMemento(),
                InstrumentMeasures = EnumerateMeasuresCore().Select(e => e.GetMemento()).ToList(),
                TimeSignature = TimeSignature.Convert(),
                IndexInScore = IndexInScore,
                KeySignature = AuthorLayout._KeySignature.Field?.Convert(),
                PaddingBottom = AuthorLayout._PaddingBottom.Field,
            };
        }
        public void ApplyMemento(ScoreMeasureModel memento)
        {
            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);

            foreach (var measureMemento in memento.InstrumentMeasures)
            {
                var measure = GetMeasureCore(measureMemento.InstrumentRibbonIndex);
                measure.ApplyMemento(measureMemento);
            }
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is null ? false : other.Id == Id;
        }
    }
}
