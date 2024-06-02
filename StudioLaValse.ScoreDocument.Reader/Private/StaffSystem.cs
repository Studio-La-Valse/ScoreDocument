using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class StaffSystem : IStaffSystemReader
    {
        private readonly IScoreDocumentReader scoreDocument;

        public IList<IScoreMeasureReader> ScoreMeasures { get; } = [];


        public StaffSystem(IScoreDocumentReader scoreDocument)
        {
            this.scoreDocument = scoreDocument;
        }


        public IEnumerable<IScoreMeasureReader> EnumerateMeasures()
        {
            return ScoreMeasures;
        }

        public IEnumerable<IStaffGroupReader> EnumerateStaffGroups()
        {
            return scoreDocument.ReadInstrumentRibbons().Select(r => new StaffGroup(r, scoreDocument.ReadLayout(), ScoreMeasures));
        }

        public IStaffSystemLayout ReadLayout()
        {
            var paddingBottom = ScoreMeasures.Max(m => m.ReadLayout().PaddingBottom);
            paddingBottom ??= scoreDocument.ReadLayout().StaffSystemPaddingBottom;
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
