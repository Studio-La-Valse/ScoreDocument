namespace StudioLaValse.ScoreDocument.Layout
{
    public class StaffSystemLayout : IStaffSystemLayout
    {
        public double PaddingTop { get; }


        public StaffSystemLayout(double paddingTop = 30)
        {
            PaddingTop = paddingTop;
        }

        public IStaffSystemLayout Copy()
        {
            return new StaffSystemLayout(PaddingTop);
        }
    }
}
