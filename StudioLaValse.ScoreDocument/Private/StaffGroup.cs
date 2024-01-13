using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffGroup : ScoreElement, IStaffGroupEditor, IStaffGroupReader
    {
        private readonly Dictionary<int, Staff> staves = [];
        private readonly IInstrumentRibbonReader host;
        private readonly StaffSystem staffSystem;
        private readonly IKeyGenerator<int> keyGenerator;
        private IStaffGroupLayout layout;

        public Instrument Instrument => 
            host.Instrument;
        public int IndexInScore => 
            host.IndexInScore;

        public StaffGroup(StaffGroupLayout layout, IInstrumentRibbonReader host, StaffSystem staffSystem, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.layout = layout;
            this.host = host;
            this.staffSystem = staffSystem;
            this.keyGenerator = keyGenerator;
        }


        public void ApplyLayout(IStaffGroupLayout layout)
        {
            this.layout = layout;
        }
        public IStaffGroupLayout ReadLayout()
        {
            return layout;
        }


        public IEnumerable<IStaffEditor> EditStaves()
        {
            for (var i = 0; i < layout.NumberOfStaves; i++)
            {
                if (!staves.TryGetValue(i, out var staff))
                {
                    var layout = new StaffLayout();
                    staff = new Staff(layout, i, keyGenerator);
                    staves.Add(i, staff);
                }

                yield return staff;
            }
        }
        public IEnumerable<IStaffReader> ReadStaves()
        {
            for (var i = 0; i < layout.NumberOfStaves; i++)
            {
                if (!staves.TryGetValue(i, out var staff))
                {
                    var layout = new StaffLayout();
                    staff = new Staff(layout, i, keyGenerator);
                    staves.Add(i, staff);
                }

                yield return staff;
            }
        }
        public IEnumerable<IStaff> EnumerateStaves()
        {
            for (var i = 0; i < layout.NumberOfStaves; i++)
            {
                if (!staves.TryGetValue(i, out var staff))
                {
                    var layout = new StaffLayout();
                    staff = new Staff(layout, i, keyGenerator);
                    staves.Add(i, staff);
                }

                yield return staff;
            }
        }


        public IEnumerable<IRibbonMeasureReader> ReadMeasures()
        {
            return staffSystem.ReadMeasures().Select(m => m.ReadMeasure(host.IndexInScore));
        }
        public IEnumerable<IRibbonMeasure> EnumerateMeasures()
        {
            return ReadMeasures();
        }


        public IInstrumentRibbonReader ReadContext()
        {
            return host;
        }

        public IStaffEditor EditStaff(int staffIndex)
        {
            if (!staves.TryGetValue(staffIndex, out var staff))
            {
                var layout = new StaffLayout();
                staff = new Staff(layout, staffIndex, keyGenerator);
                staves.Add(staffIndex, staff);
            }

            return staff;
        }
    }
}
 