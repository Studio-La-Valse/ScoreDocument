using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffSystem : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IStaffSystemReader staffSystem;
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double canvasTop;
        private readonly ColorARGB baseColor;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        public StaffSystemLayout Layout => scoreLayoutDictionary.StaffSystemLayout(staffSystem);
        public DrawableLineVertical OpeningLine =>
            new DrawableLineVertical(canvasLeft, canvasTop, staffSystem.CalculateHeight(scoreLayoutDictionary), 0.1, color: baseColor);
        public DrawableLineVertical ClosingLine
        {
            get
            {
                var isLast = staffSystem.EnumerateMeasures().Last().IsLastInScore;
                var x = isLast ? canvasLeft + length - 1 : canvasLeft + length;

                return new DrawableLineVertical(x, canvasTop, staffSystem.CalculateHeight(scoreLayoutDictionary), 0.1, color: baseColor);
            }
        }
        public DrawableLineVertical? EndOfPiece
        {
            get
            {
                var isLast = staffSystem.EnumerateMeasures().Last().IsLastInScore;
                if (!isLast)
                {
                    return null;
                }

                var thickness = isLast ? 0.5 : 0.1;

                return new DrawableLineVertical(canvasLeft + length, canvasTop - 0.05, staffSystem.CalculateHeight(scoreLayoutDictionary) + 0.1, thickness: thickness, color: baseColor);
            }
        }
        public DrawableText? MeasureCounter
        {
            get
            {
                if (staffSystem.EnumerateMeasures().First().IndexInScore == 0)
                {
                    return null;
                }

                var index = staffSystem.EnumerateMeasures().First().IndexInScore + 1;

                return new DrawableText(canvasLeft, canvasTop - 1, index.ToString(), 2, verticalAlignment: VerticalTextOrigin.Bottom, color: baseColor);
            }
        }




        public VisualStaffSystem(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, IVisualSystemMeasureFactory systemMeasureFactory, ColorARGB baseColor, ISelection<IUniqueScoreElement> selection, IScoreLayoutProvider scoreLayoutDictionary) : base(staffSystem, selection)
        {
            this.staffSystem = staffSystem;
            this.systemMeasureFactory = systemMeasureFactory;
            this.baseColor = baseColor;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.length = length;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
        }




        public IEnumerable<BaseContentWrapper> ConstructSystemMeasures()
        {
            var _canvasLeft = canvasLeft;

            //todo: calculate first measure left padding (key signature, time signature, clef)
            var firstMeasurePaddingLeft = 30;
            var lengthWithoutAdjustment = staffSystem.EnumerateMeasures().Select(m => scoreLayoutDictionary.ScoreMeasureLayout(m).Width).Sum() + firstMeasurePaddingLeft;
            var firstMeasure = true;
            foreach (var measure in staffSystem.EnumerateMeasures())
            {
                var measureLayout = scoreLayoutDictionary.ScoreMeasureLayout(measure);
                var measureWidth = measureLayout.Width;

                if (firstMeasure)
                {
                    measureWidth += firstMeasurePaddingLeft;
                }
                measureWidth = MathUtils.Map(measureWidth, 0, lengthWithoutAdjustment, 0, length);

                var systemMeasure = systemMeasureFactory.CreateContent(measure, staffSystem, _canvasLeft, canvasTop, measureWidth, firstMeasure, baseColor);
                yield return systemMeasure;

                _canvasLeft += measureWidth;
                firstMeasure = false;
            }
        }
        public IEnumerable<BaseContentWrapper> ConstructStaffGroups()
        {
            var heightOnCanvas = canvasTop;

            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var _staffGroup = new VisualStaffGroup(
                    staffGroup,
                    canvasLeft,
                    heightOnCanvas,
                    length,
                    baseColor,
                    scoreLayoutDictionary);
                yield return _staffGroup;

                heightOnCanvas += staffGroup.CalculateHeight(scoreLayoutDictionary);
                heightOnCanvas += scoreLayoutDictionary.StaffGroupLayout(staffGroup).DistanceToNext;
            }
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft, canvasLeft + length, canvasTop, canvasTop + staffSystem.CalculateHeight(scoreLayoutDictionary));
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return OpeningLine;
            yield return ClosingLine;

            if (MeasureCounter is DrawableText text)
            {
                yield return text;
            }

            if (EndOfPiece is DrawableLineVertical line)
            {
                yield return line;
            }
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            foreach (var group in ConstructStaffGroups())
            {
                yield return group;
            }

            foreach (var systemMeasure in ConstructSystemMeasures())
            {
                yield return systemMeasure;
            }

            yield return new SimpleGhost(this);
        }
    }
}
