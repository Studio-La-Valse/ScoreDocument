using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffSystem : IStaffSystem
    {
        private readonly IScoreDocument scoreDocument;

        public IList<IScoreMeasure> ScoreMeasures { get; } = [];

        public double PaddingBottom => ReadLayout().PaddingBottom;


        public StaffSystem(IScoreDocument scoreDocument)
        {
            this.scoreDocument = scoreDocument;
        }


        public IEnumerable<IScoreMeasure> EnumerateMeasures()
        {
            return ScoreMeasures;
        }

        public IEnumerable<IStaffGroup> EnumerateStaffGroups()
        {
            return scoreDocument.ReadInstrumentRibbons().Select(r => new StaffGroup(r, scoreDocument, ScoreMeasures));
        }

        public IStaffSystemLayout ReadLayout()
        {
            var paddingBottom = ScoreMeasures.Max(m => m.PaddingBottom);
            paddingBottom ??= scoreDocument.StaffSystemPaddingBottom;
            return new StaffSystemLayout(paddingBottom.Value);
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return EnumerateMeasures().OfType<IScoreElement>().Concat(EnumerateStaffGroups());
        }

        public override string ToString()
        {
            return $"Staff System";
        }
    }
}
