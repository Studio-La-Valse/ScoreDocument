using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Drawable.Interaction.ContentWrappers;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Drawable.Text;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Ghosts;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.VisualParents
{
    public sealed class VisualStaffSystem : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IStaffSystemReader staffSystem;
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double canvasTop;
        private readonly ColorARGB baseColor;



        public DrawableLineVertical OpeningLine =>
            new DrawableLineVertical(canvasLeft, canvasTop, staffSystem.CalculateHeight(), 0.1, color: baseColor);
        public DrawableLineVertical ClosingLine
        {
            get
            {
                var isLast = staffSystem.ReadMeasures().Last().IsLastInScore;

                var x = isLast ? canvasLeft + length - 1 : canvasLeft + length;

                return new DrawableLineVertical(x, canvasTop, staffSystem.CalculateHeight(), 0.1, color: baseColor);
            }
        }
        public DrawableLineVertical? EndOfPiece
        {
            get
            {
                var isLast = staffSystem.ReadMeasures().Last().IsLastInScore;

                if (!isLast)
                    return null;

                var thickness = isLast ? 0.5 : 0.1;

                return new DrawableLineVertical(canvasLeft + length, canvasTop - 0.05, staffSystem.CalculateHeight() + 0.1, thickness: thickness, color: baseColor);
            }
        }
        public DrawableText? MeasureCounter
        {
            get
            {
                if (staffSystem.ReadMeasures().First().IndexInScore == 0)
                    return null;

                var index = staffSystem.ReadMeasures().First().IndexInScore + 1;

                return new DrawableText(canvasLeft, canvasTop - 1, index.ToString(), 2, verticalAlignment: VerticalTextOrigin.Bottom, color: baseColor);
            }
        }




        public VisualStaffSystem(
            IStaffSystemReader staffSystem,
            double canvasLeft,
            double canvasTop,
            double length,
            IVisualSystemMeasureFactory systemMeasureFactory,
            ColorARGB baseColor,
            ISelection<IUniqueScoreElement> selection) : base(staffSystem, selection)
        {
            this.staffSystem = staffSystem;
            this.systemMeasureFactory = systemMeasureFactory;
            this.baseColor = baseColor;
            this.length = length;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
        }




        public IEnumerable<BaseContentWrapper> ConstructSystemMeasures()
        {
            var _canvasLeft = canvasLeft;

            //todo: calculate first measure left padding (key signature, time signature, clef)
            var firstMeasurePaddingLeft = 30;
            var lengthWithoutAdjustment = staffSystem.ReadMeasures().Select(m => m.ReadLayout().Width).Sum() + firstMeasurePaddingLeft;

            foreach (var measure in staffSystem.ReadMeasures())
            {
                var measureWidth = measure.ReadLayout().Width;
                if (measure.ReadLayout().IsNewSystem)
                {
                    measureWidth += firstMeasurePaddingLeft;
                }
                measureWidth = MathUtils.Map(measureWidth, 0, lengthWithoutAdjustment, 0, length);

                var systemMeasure = systemMeasureFactory.CreateContent(measure, staffSystem, _canvasLeft, canvasTop, measureWidth, baseColor);
                yield return systemMeasure;

                _canvasLeft += measureWidth;
            }
        }
        public IEnumerable<BaseContentWrapper> ConstructStaffGroups()
        {
            var heightOnCanvas = canvasTop;

            foreach (var staffGroup in staffSystem.ReadStaffGroups())
            {
                var _staffGroup = new VisualStaffGroup(
                    staffGroup,
                    canvasLeft,
                    heightOnCanvas,
                    length,
                    baseColor);
                yield return _staffGroup;

                heightOnCanvas += staffGroup.CalculateHeight();
                heightOnCanvas += staffGroup.ReadLayout().DistanceToNext;
            }
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft, canvasLeft + length, canvasTop, canvasTop + staffSystem.CalculateHeight());
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var elements = new List<BaseDrawableElement>
            {
                OpeningLine,
                ClosingLine
            };

            if (MeasureCounter is DrawableText text)
            {
                elements.Add(text);
            }

            if (EndOfPiece is DrawableLineVertical line)
            {
                elements.Add(line);
            }

            return elements;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            var elements = new List<BaseContentWrapper>();

            foreach (var group in ConstructStaffGroups())
            {
                elements.Add(group);
            }

            foreach (var systemMeasure in ConstructSystemMeasures())
            {
                elements.Add(systemMeasure);
            }

            elements.Add(new SimpleGhost(this));

            return elements;
        }
    }
}
