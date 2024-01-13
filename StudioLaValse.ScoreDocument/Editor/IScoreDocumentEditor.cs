namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IScoreDocumentEditor : IScoreDocument
    {
        int NumberOfMeasures { get; }
        int NumberOfInstruments { get; }


        IEnumerable<IScoreMeasureEditor> EditScoreMeasures();
        IEnumerable<IInstrumentRibbonEditor> EditInstrumentRibbons();
        IEnumerable<IStaffSystemEditor> EditStaffSystems();


        void Clear();


        void AddInstrumentRibbon(Instrument instrument);
        void RemoveInstrumentRibbon(int elementId);


        void AppendScoreMeasure(TimeSignature? timeSignature = null);
        void InsertScoreMeasure(int index, TimeSignature? timeSignature = null);
        void RemoveScoreMeasure(int indexInScore);


        IScoreMeasureEditor EditScoreMeasure(int indexInScore);
        IInstrumentRibbonEditor EditInstrumentRibbon(int indexInScore);


        void ApplyLayout(IScoreDocumentLayout layout);
        IScoreDocumentLayout ReadLayout();
    }
}
