using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffGroupLayout : IStaffGroupLayout
    {
        public ReadonlyTemplateProperty<bool> Collapsed { get; }

        public ReadonlyTemplateProperty<int> NumberOfStaves { get; }

        public ReadonlyTemplateProperty<double> DistanceToNext { get; }

        public ReadonlyTemplateProperty<double> VerticalStaffLineThickness { get; }

        public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness { get; }

        public ReadonlyTemplateProperty<ColorARGB> Color { get; }

        public ReadonlyTemplateProperty<double> Scale { get; }

        public StaffGroupLayout(
            ReadonlyTemplateProperty<int> numberOfStaves,
            ReadonlyTemplateProperty<double> distanceToNext,
            ReadonlyTemplateProperty<bool> collapsed,
            ReadonlyTemplateProperty<double> horizontalStaffLineThickness,
            ReadonlyTemplateProperty<double> verticalStaffLineThickness,
            ReadonlyTemplateProperty<ColorARGB> color,
            ReadonlyTemplateProperty<double> scale)
        {
            NumberOfStaves = numberOfStaves;
            DistanceToNext = distanceToNext;
            Collapsed = collapsed;
            VerticalStaffLineThickness = verticalStaffLineThickness;
            HorizontalStaffLineThickness = horizontalStaffLineThickness;
            Color = color;
            Scale = scale;
        }
    }
}
