using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class Staff : IStaff
    {
        private readonly IScoreDocumentLayout staffStyleTemplate;
        private readonly IEnumerable<IInstrumentMeasure> measures;

        public int IndexInStaffGroup { get; }

        public Staff(int indexInStaffGroup, IScoreDocumentLayout staffStyleTemplate, IEnumerable<IInstrumentMeasure> measures)
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

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            yield break;
        }

        public override string ToString()
        {
            return $"Staff {IndexInStaffGroup}";
        }

        public void RemoveLayout()
        {
            
        }
    }
}
