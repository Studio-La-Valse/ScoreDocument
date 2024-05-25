using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class StaffSystem : IStaffSystemReader
    {
        private readonly IScoreDocumentReader scoreDocument;
        private readonly ScoreDocumentStyleTemplate documentStyleTemplate;

        public IList<IScoreMeasureReader> ScoreMeasures { get; } = [];


        public StaffSystem(IScoreDocumentReader scoreDocument, ScoreDocumentStyleTemplate documentStyleTemplate)
        {
            this.scoreDocument = scoreDocument;
            this.documentStyleTemplate = documentStyleTemplate;
        }


        public IEnumerable<IScoreMeasureReader> EnumerateMeasures()
        {
            return ScoreMeasures;
        }

        public IEnumerable<IStaffGroupReader> EnumerateStaffGroups()
        {
            return scoreDocument.ReadInstrumentRibbons().Select(r => new StaffGroup(r, documentStyleTemplate, ScoreMeasures));
        }

        public IStaffSystemLayout ReadLayout()
        {
            var paddingBottom = ScoreMeasures.Max(m => m.ReadLayout().PaddingBottom);
            paddingBottom ??= documentStyleTemplate.StaffSystemStyleTemplate.PaddingBottom;
            return new StaffSystemLayout(paddingBottom.Value);
        }
    }
}
