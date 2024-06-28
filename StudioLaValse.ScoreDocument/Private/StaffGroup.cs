using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffGroup : IStaffGroup
    {
        private readonly IScoreDocument documentStyleTemplate;
        private readonly IEnumerable<IScoreMeasure> scoreMeasures;


        public IInstrumentRibbon InstrumentRibbon { get; }


        public Instrument Instrument =>
            InstrumentRibbon.Instrument;
        public int IndexInSystem =>
            InstrumentRibbon.IndexInScore;

        public ReadonlyTemplateProperty<bool> Collapsed => ReadLayout().Collapsed;

        public ReadonlyTemplateProperty<double> DistanceToNext => ReadLayout().DistanceToNext;

        public ReadonlyTemplateProperty<int> NumberOfStaves => ReadLayout().NumberOfStaves;



        public StaffGroup(IInstrumentRibbon instrumentRibbon,
            IScoreDocument documentStyleTemplate,
            IEnumerable<IScoreMeasure> scoreMeasures)
        {
            InstrumentRibbon = instrumentRibbon;

            this.scoreMeasures = scoreMeasures;
            this.documentStyleTemplate = documentStyleTemplate;
        }



        public IEnumerable<IStaff> EnumerateStaves()
        {
            var numberOfStaves = ReadLayout().NumberOfStaves;

            return EnumerateStaves(numberOfStaves);
        }

        public IEnumerable<IStaff> EnumerateStaves(int numberOfStaves)
        {
            for (var staffIndex = 0; staffIndex < numberOfStaves; staffIndex++)
            {
                yield return new Staff(staffIndex, documentStyleTemplate, EnumerateMeasures());
            }
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return scoreMeasures.Select(e => e.ReadMeasure(InstrumentRibbon.IndexInScore));
        }

        public IStaffGroupLayout ReadLayout()
        {
            var numberOfStaves = new ReadonlyTemplatePropertyFromFunc<int>(() =>
            {
                var numberOfStaves = EnumerateMeasures().Max(m => m.NumberOfStaves.Value);
                if (numberOfStaves is null)
                {
                    var highestStaffIndex = 1;
                    foreach (var measure in EnumerateMeasures())
                    {
                        foreach (var note in measure.ReadNotes())
                        {
                            highestStaffIndex = Math.Max(highestStaffIndex, note.StaffIndex + 1);
                        }
                    }
                    numberOfStaves = Math.Max(Instrument.NumberOfStaves, highestStaffIndex);
                }
                return numberOfStaves.Value;
            });

            var distanceToNext = new ReadonlyTemplatePropertyFromFunc<double>(() =>
            {
                var distanceToNext = EnumerateMeasures().Max(m => m.PaddingBottom.Value) ??
                    documentStyleTemplate.StaffGroupPaddingBottom.Value;
                return distanceToNext;
            });

            var collapsed = new ReadonlyTemplatePropertyFromFunc<bool>(() =>
            {
                var collapsed = EnumerateMeasures().Any(m => m.Collapsed.Value ?? false);
                return collapsed;
            });
            
            var layout = new StaffGroupLayout(numberOfStaves, distanceToNext, collapsed);
            return layout;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return EnumerateMeasures();
        }

        public override string ToString()
        {
            return $"Staff Group : [{Instrument}]";
        }
    }
}
