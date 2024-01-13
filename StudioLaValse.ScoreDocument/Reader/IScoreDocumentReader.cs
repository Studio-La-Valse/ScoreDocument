using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IScoreDocumentReader : IScoreDocument, IUniqueScoreElement
    {
        int NumberOfMeasures { get; }
        int NumberOfInstruments { get; }


        IEnumerable<IScoreMeasureReader> ReadScoreMeasures();
        IEnumerable<IInstrumentRibbonReader> ReadInstrumentRibbons();
        IEnumerable<IStaffSystemReader> ReadStaffSystems();



        IScoreMeasureReader ReadMeasure(int indexInScore);
        IInstrumentRibbonReader ReadInstrumentRibbon(int indexInScore);


        IScoreDocumentLayout ReadLayout();
    }
}
