namespace StudioLaValse.ScoreDocument.Layout
{
    public class ClefChange
    {
        private readonly Clef clef;
        private readonly int staffIndex;
        private readonly Position position;

        public ClefChange(Clef clef, int staffIndex, Position position)
        {
            this.clef = clef;
            this.staffIndex = staffIndex;
            this.position = position;
        }

        public Clef Clef => clef;

        public int StaffIndex => staffIndex;

        public Position Position => position;
    }
}
