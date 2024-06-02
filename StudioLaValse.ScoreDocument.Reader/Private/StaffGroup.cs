using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class StaffGroup : IStaffGroupReader
    {
        private readonly IScoreDocumentLayout documentStyleTemplate;
        private readonly IEnumerable<IScoreMeasureReader> scoreMeasures;


        public IInstrumentRibbonReader InstrumentRibbon { get; }


        public Instrument Instrument =>
            InstrumentRibbon.Instrument;
        public int IndexInSystem =>
            InstrumentRibbon.IndexInScore;


        public StaffGroup(IInstrumentRibbonReader instrumentRibbon, IScoreDocumentLayout documentStyleTemplate, IEnumerable<IScoreMeasureReader> scoreMeasures)
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
                yield return new Staff(staffIndex, documentStyleTemplate, EnumerateMeasures());
            }
        }

        public IEnumerable<IInstrumentMeasureReader> EnumerateMeasures()
        {
            return scoreMeasures.Select(e => e.ReadMeasure(InstrumentRibbon.IndexInScore));
        }

        public IStaffGroupLayout ReadLayout()
        {
            var numberOfStaves = EnumerateMeasures().Max(m => m.ReadLayout().NumberOfStaves);
            if(numberOfStaves is null)
            {
                var highestStaffIndex = 1;
                foreach(var measure in EnumerateMeasures())
                {
                    foreach(var note in measure.ReadNotes())
                    {
                        highestStaffIndex = Math.Max(highestStaffIndex, note.ReadLayout().StaffIndex + 1);
                    }
                }
                numberOfStaves = Math.Max(Instrument.NumberOfStaves, highestStaffIndex);
            }
                
            var distanceToNext = EnumerateMeasures().Max(m => m.ReadLayout().PaddingBottom) ??
                documentStyleTemplate.StaffGroupPaddingBottom;
            var collapsed = EnumerateMeasures().Any(m => m.ReadLayout().Collapsed);

            var layout = new StaffGroupLayout(numberOfStaves.Value, distanceToNext, collapsed);
            return layout;
        }
    }
}
