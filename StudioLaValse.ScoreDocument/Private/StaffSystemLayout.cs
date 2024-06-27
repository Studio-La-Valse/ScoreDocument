using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffSystemLayout : IStaffSystemLayout
    {
        public double PaddingBottom { get; }


        public StaffSystemLayout(double paddingBottom)
        {
            PaddingBottom = paddingBottom;
        }
    }
}
