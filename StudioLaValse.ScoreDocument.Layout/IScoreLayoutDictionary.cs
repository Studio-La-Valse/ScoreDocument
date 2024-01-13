using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreLayoutDictionary
    {
        IScoreDocumentLayout GetOrCreate(IScoreDocument scoreDocument);
        void Assign(IScoreDocument scoreDocument, IScoreDocumentLayout layout);

        IMeasureElementLayout GetOrCreate(INote element);
        void Assign(INote element, IMeasureElementLayout layout);

        IChordLayout GetOrCreate(IChord element);
        void Assign(IChord chord, IChordLayout layout);

        INoteGroupLayout GetOrCreate(IMeasureBlockReader element);
        void Assign(IMeasureBlockReader chord, INoteGroupLayout layout);

        IInstrumentMeasureLayout GetOrCreate(IInstrumentMeasure element);
        void Assign(IInstrumentMeasure ribbonMeasure, IInstrumentMeasureLayout layout);

        IInstrumentRibbonLayout GetOrCreate(IInstrumentRibbon element);
        void Assign(IInstrumentRibbon instrumentRibbon, IInstrumentRibbonLayout layout);

        IScoreMeasureLayout GetOrCreate(IScoreMeasure element);
        void Assign(IScoreMeasure scoreMeasure, IScoreMeasureLayout layout);

        IStaffLayout GetOrCreate(IStaff element);
        void Assign(IStaff staffReader, IStaffLayout layout);

        IStaffGroupLayout GetOrCreate(IStaffGroup element);
        void Assign(IStaffGroup staffGroup, IStaffGroupLayout layout);

        IStaffSystemLayout GetOrCreate(IStaffSystem element);
        void Assign(IStaffSystem staffSystem, IStaffSystemLayout layout);
    }
}
