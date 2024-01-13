namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffSystem : ScoreElement, IStaffSystemEditor, IStaffSystemReader
    {
        private readonly ScoreMeasure firstMeasure;
        private readonly Dictionary<InstrumentRibbon, StaffGroup> staffGroups = [];
        private readonly IKeyGenerator<int> keyGenerator;
        private IStaffSystemLayout layout;





        private StaffSystem(IKeyGenerator<int> keyGenerator, StaffSystemLayout layout, ScoreMeasure firstMeasure) : base(keyGenerator)
        {
            this.keyGenerator = keyGenerator;
            this.layout = layout;
            this.firstMeasure = firstMeasure;
        }
        public static StaffSystem Create(IKeyGenerator<int> keyGenerator, StaffSystemLayout layout, ScoreMeasure firstMeasure, IEnumerable<InstrumentRibbon> existingRibbons)
        {
            var staffSystem = new StaffSystem(keyGenerator, layout, firstMeasure);

            foreach (var instrumentRibbon in existingRibbons)
            {
                staffSystem.Register(instrumentRibbon);
            }

            return staffSystem;
        }




        public IEnumerable<IStaffGroup> EnumerateStaffGroups()
        {
            return staffGroups.Select(e => e.Value);
        }
        public IEnumerable<IStaffGroupReader> ReadStaffGroups()
        {
            return staffGroups.Select(e => e.Value);
        }
        public IEnumerable<IStaffGroupEditor> EditStaffGroups()
        {
            return staffGroups.Select(e => e.Value);
        }



        public IStaffGroupReader ReadStaffGroup(IInstrumentRibbonReader instrumentRibbon)
        {
            return staffGroups.First(s => s.Key.IndexInScore == instrumentRibbon.IndexInScore).Value;
        }
        public IStaffGroupEditor EditStaffGroup(int indexInScore)
        {
            return staffGroups.First(s => s.Key.IndexInScore == indexInScore).Value;
        }

        public IEnumerable<IScoreMeasure> EnumerateMeasures()
        {
            return ReadMeasures();
        }
        public IEnumerable<IScoreMeasureReader> ReadMeasures()
        {
            var measure = (IScoreMeasureReader)firstMeasure;
            yield return measure;

            while (true)
            {
                if (measure.TryReadNext(out measure))
                {
                    if (measure.ReadLayout().IsNewSystem)
                    {
                        yield break;
                    }

                    yield return measure;
                    continue;
                }

                yield break;
            }
        }


        public IStaffSystemLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IStaffSystemLayout layout)
        {
            this.layout = layout;
        }


        public void Register(InstrumentRibbon instrumentRibbon)
        {
            var layout = new StaffGroupLayout(instrumentRibbon.Instrument);
            var staffGroup = new StaffGroup(layout, instrumentRibbon, this, keyGenerator);
            staffGroups.TryAdd(instrumentRibbon, staffGroup);
        }
        public void Unregister(InstrumentRibbon instrumentRibbon)
        {
            staffGroups.Remove(instrumentRibbon);
        }
    }
}
