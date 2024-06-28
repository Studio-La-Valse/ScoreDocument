using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffSystemLayout : IStaffSystemLayout
    {
        public ReadonlyTemplateProperty<double> PaddingBottom { get; }


        public StaffSystemLayout(ReadonlyTemplateProperty<double> paddingBottom)
        {
            PaddingBottom = paddingBottom;
        }
    }
}
