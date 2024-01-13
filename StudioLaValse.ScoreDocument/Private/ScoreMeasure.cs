using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class ScoreMeasure : ScoreElement, IScoreMeasureEditor, IScoreMeasureReader
    {
        private readonly ScoreDocumentCore score;
        private IScoreMeasureLayout layout;
        private readonly StaffSystem staffSystemReturnCarriage;


        public TimeSignature TimeSignature { get; }


        public int IndexInScore =>
            score.contentTable.IndexOf(this);
        public bool IsLastInScore =>
            IndexInScore == score.NumberOfMeasures - 1;
        public StaffSystem StaffSystemOrigin
        {
            get => staffSystemReturnCarriage;
        }


        internal ScoreMeasure(ScoreDocumentCore score, TimeSignature timeSignature, IScoreMeasureLayout layout, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.score = score;
            this.layout = layout;
            staffSystemReturnCarriage = StaffSystem.Create(keyGenerator, new StaffSystemLayout(), this, score.contentTable.RowHeaders);
            TimeSignature = timeSignature;
        }


        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return EditMeasures();
        }
        public IEnumerable<IInstrumentMeasureEditor> EditMeasures()
        {
            var measures = score.contentTable.GetCellsColumn(IndexInScore);
            return measures;
        }
        public IEnumerable<IInstrumentMeasureReader> ReadMeasures()
        {
            var measures = score.contentTable.GetCellsColumn(IndexInScore);
            return measures;
        }



        public IInstrumentMeasureEditor EditMeasure(int indexInScore)
        {
            return score.contentTable.GetCell(IndexInScore, indexInScore);
        }
        public IInstrumentMeasureReader ReadMeasure(int indexInScore)
        {
            return score.contentTable.GetCell(IndexInScore, indexInScore);
        }



        public bool TryReadPrevious([NotNullWhen(true)] out IScoreMeasureReader? previous)
        {
            previous = null;
            if (IndexInScore == 0)
            {
                return false;
            }

            try
            {
                previous = score.contentTable.ColumnAt(IndexInScore - 1);
            }
            catch { }

            return previous is not null;
        }
        public bool TryReadNext([NotNullWhen(true)] out IScoreMeasureReader? next)
        {
            next = null;
            if (IndexInScore + 1 >= score.NumberOfMeasures)
            {
                return false;
            }

            try
            {
                next = score.contentTable.ColumnAt(IndexInScore + 1);
            }
            catch { }

            return next is not null;
        }




        public void ApplyLayout(IScoreMeasureLayout layout)
        {
            this.layout = layout;
        }
        public IScoreMeasureLayout ReadLayout()
        {
            return layout;
        }

        public IStaffSystemEditor EditStaffSystemOrigin()
        {
            return StaffSystemOrigin;
        }
        public IStaffSystemReader ReadStaffSystemOrigin()
        {
            return StaffSystemOrigin;
        }
        public IStaffSystem GetStaffSystemOrigin()
        {
            return StaffSystemOrigin;
        }
    }
}
