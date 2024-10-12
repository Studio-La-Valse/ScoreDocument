using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffSystem : IStaffSystem
    {
        private readonly IScoreDocument scoreDocument;

        public IList<IScoreMeasure> ScoreMeasures { get; } = [];

        public ReadonlyTemplateProperty<double> PaddingBottom => ReadLayout().PaddingBottom;

        public ReadonlyTemplateProperty<double> VerticalStaffLineThickness => ReadLayout().VerticalStaffLineThickness;  

        public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness => ReadLayout().HorizontalStaffLineThickness;

        public ReadonlyTemplateProperty<double> Scale => ReadLayout().Scale;

        public ReadonlyTemplateProperty<ColorARGB> Color => ReadLayout().Color;

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
            var property = new ReadonlyTemplatePropertyFromFunc<double>(() =>
            {
                var paddingBottom = ScoreMeasures.Max(m => m.PaddingBottom.Value);
                paddingBottom ??= scoreDocument.StaffSystemPaddingBottom;
                return paddingBottom.Value;
            });
            
            return new StaffSystemLayout(property, scoreDocument);
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
