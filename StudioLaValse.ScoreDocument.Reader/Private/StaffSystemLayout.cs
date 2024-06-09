using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Reader.Private
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
