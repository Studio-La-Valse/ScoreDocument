using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class Staff : IStaffReader
    {
        private readonly IScoreDocumentLayout staffStyleTemplate;
        private readonly IEnumerable<IInstrumentMeasureReader> measures;

        public int IndexInStaffGroup { get; }

        public Staff(int indexInStaffGroup, IScoreDocumentLayout staffStyleTemplate, IEnumerable<IInstrumentMeasureReader> measures)
        {
            this.staffStyleTemplate = staffStyleTemplate;
            this.measures = measures;

            IndexInStaffGroup = indexInStaffGroup;
        }

        public IStaffLayout ReadLayout()
        {
            var paddingBottom = measures.Max(m => m.ReadLayout().GetPaddingBottom(IndexInStaffGroup));
            paddingBottom ??= staffStyleTemplate.StaffPaddingBottom;
            var layout = new StaffLayout(paddingBottom.Value);
            return layout;
        }
    }
}
