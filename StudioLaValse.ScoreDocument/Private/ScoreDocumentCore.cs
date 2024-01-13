namespace StudioLaValse.ScoreDocument.Private
{
    internal class ScoreDocumentCore : ScoreElement, IScoreDocumentEditor, IScoreDocumentReader
    {
        internal readonly Table<InstrumentMeasure, ScoreMeasure, InstrumentRibbon> contentTable;
        private readonly IKeyGenerator<int> keyGenerator;
        private IScoreDocumentLayout layout;




        public int NumberOfMeasures =>
            contentTable.Width;
        public int NumberOfInstruments =>
            contentTable.Height;





        public ScoreDocumentCore(Table<InstrumentMeasure, ScoreMeasure, InstrumentRibbon> contentTable, IScoreDocumentLayout layout, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.contentTable = contentTable;
            this.layout = layout;
            this.keyGenerator = keyGenerator;
        }






        public void AddInstrumentRibbon(Instrument instrument)
        {
            var layout = new InstrumentRibbonLayout(instrument);
            var instrumentRibbon = new InstrumentRibbon(this, instrument, layout, keyGenerator);
            contentTable.AddRow(instrumentRibbon);

            foreach (var staffSystem in contentTable.ColumnHeaders.Select(m => m.StaffSystemOrigin))
            {
                staffSystem.Register(instrumentRibbon);
            }
        }
        public void RemoveInstrumentRibbon(int elementId)
        {
            var ribbon = contentTable.RowHeaders.FirstOrDefault(r => r.ElementId.IntValue == elementId);
            if (ribbon is null)
            {
                throw new InvalidOperationException("Ribbon with element id not found!");
            }

            foreach (var staffSystem in contentTable.ColumnHeaders.Select(m => m.StaffSystemOrigin))
            {
                staffSystem.Unregister(ribbon);
            }

            contentTable.RemoveRow(ribbon.IndexInScore);
        }



        public void AppendScoreMeasure(TimeSignature? timeSignature = null)
        {
            if (!contentTable.RowHeaders.Any())
            {
                throw new Exception("Please construct an instrument ribbon first");
            }

            var previousElement = contentTable.ColumnHeaders.LastOrDefault();
            timeSignature ??= previousElement is not null ?
                    previousElement.TimeSignature :
                    new TimeSignature(4, 4);

            var keySignature = previousElement is not null ?
                    previousElement.ReadLayout().KeySignature :
                    new KeySignature(Step.C, MajorOrMinor.Major);

            var layout = new ScoreMeasureLayout(keySignature);

            var scoreMeasure = new ScoreMeasure(this, timeSignature, layout, keyGenerator);
            contentTable.AddColumn(scoreMeasure);
        }
        public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
        {
            if (!contentTable.RowHeaders.Any())
            {
                throw new Exception("Please construct an instrument ribbon first");
            }

            var previousElement = contentTable.ColumnHeaders.ElementAtOrDefault(index - 1);
            var nextElement = contentTable.ColumnHeaders.ElementAtOrDefault(index + 1);
            timeSignature ??= previousElement is not null ?
                    previousElement.TimeSignature :
                    nextElement is not null ?
                        nextElement.TimeSignature :
                        new TimeSignature(4, 4);

            var keySignature = previousElement is not null ?
                    previousElement.ReadLayout().KeySignature :
                    nextElement is not null ?
                        nextElement.ReadLayout().KeySignature :
                        new KeySignature(Step.C, MajorOrMinor.Major);

            var layout = new ScoreMeasureLayout(keySignature);

            var scoreMeasure = new ScoreMeasure(this, timeSignature, layout, keyGenerator);
            contentTable.InsertColumn(scoreMeasure, index);
        }
        public void RemoveScoreMeasure(int indexInScore)
        {
            contentTable.RemoveColumn(indexInScore);
        }


        public void Clear()
        {
            while (contentTable.Height > 0)
            {
                contentTable.RemoveRow(0);
            }

            while (contentTable.Width > 0)
            {
                contentTable.RemoveColumn(0);
            }
        }


        public IEnumerable<IScoreMeasureEditor> EditScoreMeasures() =>
            contentTable.ColumnHeaders;
        public IEnumerable<IInstrumentRibbonEditor> EditInstrumentRibbons() =>
            contentTable.RowHeaders;



        public IEnumerable<IScoreMeasureReader> ReadScoreMeasures() =>
            contentTable.ColumnHeaders;
        public IEnumerable<IInstrumentRibbonReader> ReadInstrumentRibbons() =>
            contentTable.RowHeaders;



        public IEnumerable<IScoreMeasure> EnumerateScoreMeasures()
        {
            return ReadScoreMeasures();
        }
        public IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons()
        {
            return ReadInstrumentRibbons();
        }



        public IEnumerable<IStaffSystemEditor> EditStaffSystems()
        {
            if (!EnumerateScoreMeasures().Any())
            {
                yield break;
            }

            var firstMeasure = contentTable.ColumnHeaders.First();
            if (!firstMeasure.ReadLayout().IsNewSystem)
            {
                throw new Exception("The first measure of the score must have a new system in it's layout supplied before the score can enumerate the staff systems.");
            }

            foreach (var measure in contentTable.ColumnHeaders)
            {
                if (measure.ReadLayout().IsNewSystem)
                {
                    yield return measure.StaffSystemOrigin;
                }
            }
        }
        public IEnumerable<IStaffSystemReader> ReadStaffSystems()
        {
            if (!EnumerateScoreMeasures().Any())
            {
                yield break;
            }

            var firstMeasure = contentTable.ColumnHeaders.First();
            if (!firstMeasure.ReadLayout().IsNewSystem)
            {
                throw new Exception("The first measure of the score must have a new system in it's layout supplied before the score can enumerate the staff systems.");
            }

            foreach (var measure in contentTable.ColumnHeaders)
            {
                if (measure.ReadLayout().IsNewSystem)
                {
                    yield return measure.StaffSystemOrigin;
                }
            }
        }



        public IScoreDocumentLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IScoreDocumentLayout layout)
        {
            this.layout = layout;
        }



        public IScoreMeasureEditor EditScoreMeasure(int indexInScore)
        {
            return contentTable.ColumnAt(indexInScore);
        }
        public IScoreMeasureReader ReadMeasure(int indexInScore)
        {
            return contentTable.ColumnAt(indexInScore);
        }



        public IInstrumentRibbonEditor EditInstrumentRibbon(int indexInScore)
        {
            return contentTable.RowAt(indexInScore);
        }
        public IInstrumentRibbonReader ReadInstrumentRibbon(int indexInScore)
        {
            return contentTable.RowAt(indexInScore);
        }


    }
}

