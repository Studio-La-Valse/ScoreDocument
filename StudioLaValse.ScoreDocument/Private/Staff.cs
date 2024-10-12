using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class Staff : IStaff
    {
        private readonly IScoreDocumentLayout scoreDocument;
        private readonly IInstrumentRibbonLayout instrumentRibbon;
        private readonly IEnumerable<IInstrumentMeasure> measures;

        public int IndexInStaffGroup { get; }

        public ReadonlyTemplateProperty<double> DistanceToNext => ReadLayout().DistanceToNext;

        public ReadonlyTemplateProperty<double> VerticalStaffLineThickness => ReadLayout().VerticalStaffLineThickness;

        public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness => ReadLayout().HorizontalStaffLineThickness;

        public ReadonlyTemplateProperty<ColorARGB> Color => ReadLayout().Color;

        public ReadonlyTemplateProperty<double> Scale => ReadLayout().Scale;

        public Staff(int indexInStaffGroup, IScoreDocumentLayout scoreDocument, IInstrumentRibbonLayout instrumentRibbon, IEnumerable<IInstrumentMeasure> measures)
        {
            this.scoreDocument = scoreDocument;
            this.instrumentRibbon = instrumentRibbon;
            this.measures = measures;

            IndexInStaffGroup = indexInStaffGroup;
        }

        public IStaffLayout ReadLayout()
        {
            var property = new ReadonlyTemplatePropertyFromFunc<double>(() =>
            {
                var paddingBottom = measures.Max(m => m.GetPaddingBottom(IndexInStaffGroup));
                paddingBottom ??= scoreDocument.StaffPaddingBottom;
                return paddingBottom.Value;
            });

            var layout = new StaffLayout(property, scoreDocument, instrumentRibbon);
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
    }
}
