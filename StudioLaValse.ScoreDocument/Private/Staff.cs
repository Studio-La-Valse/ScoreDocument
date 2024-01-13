using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class Staff : ScoreElement, IStaffEditor, IStaffReader
    {
        private IStaffLayout staffLayout;
        private readonly int index;

        public int IndexInStaffGroup => index;

        public Staff(StaffLayout staffLayout, int index, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.staffLayout = staffLayout;
            this.index = index;
        }

        public void ApplyLayout(IStaffLayout layout)
        {
            staffLayout = layout;
        }

        public IStaffLayout ReadLayout()
        {
            return staffLayout;
        }
    }
}
