using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class StaffGroup : IStaffGroupReader
    {
        private readonly ScoreDocumentStyleTemplate documentStyleTemplate;
        private readonly IList<IScoreMeasureReader> scoreMeasures;


        public IInstrumentRibbonReader InstrumentRibbon { get; }


        public Instrument Instrument =>
            InstrumentRibbon.Instrument;
        public int IndexInSystem =>
            InstrumentRibbon.IndexInScore;


        public StaffGroup(IInstrumentRibbonReader instrumentRibbon, ScoreDocumentStyleTemplate documentStyleTemplate, IList<IScoreMeasureReader> scoreMeasures)
        {
            InstrumentRibbon = instrumentRibbon;

            this.scoreMeasures = scoreMeasures;
            this.documentStyleTemplate = documentStyleTemplate;
        }


        public IEnumerable<IStaffReader> EnumerateStaves()
        {
            var numberOfStaves = ReadLayout().NumberOfStaves;

            return EnumerateStaves(numberOfStaves);
        }

        public IEnumerable<IStaffReader> EnumerateStaves(int numberOfStaves)
        {
            for (var staffIndex = 0; staffIndex < numberOfStaves; staffIndex++)
            {
                yield return new Staff(staffIndex, documentStyleTemplate.StaffStyleTemplate, EnumerateMeasures());
            }
        }

        public IEnumerable<IInstrumentMeasureReader> EnumerateMeasures()
        {
            return scoreMeasures.Select(e => e.ReadMeasure(InstrumentRibbon.IndexInScore));
        }

        public IStaffGroupLayout ReadLayout()
        {
            var numberOfStaves = EnumerateMeasures().Max(m => m.ReadLayout().NumberOfStaves) ??
                    Instrument.NumberOfStaves;
            var distanceToNext = EnumerateMeasures().Max(m => m.ReadLayout().PaddingBottom) ??
                documentStyleTemplate.StaffGroupStyleTemplate.DistanceToNext;
            var collapsed = EnumerateMeasures().Any(m => m.ReadLayout().Collapsed);

            var layout = new StaffGroupLayout(numberOfStaves, distanceToNext, collapsed);
            return layout;
        }
    }
}
