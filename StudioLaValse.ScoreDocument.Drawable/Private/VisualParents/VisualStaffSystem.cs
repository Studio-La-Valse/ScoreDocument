using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffSystem : BaseContentWrapper
    {
        private readonly IStaffSystemReader staffSystem;
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double globalLineSpacing;
        private readonly double canvasTop;
        private readonly ColorARGB baseColor;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;


        public double VerticalLineThickness =>
            scoreLayoutDictionary.VerticalStaffLineThickness * ScoreScale;
        public double HorizontalLineThickness =>
            scoreLayoutDictionary.HorizontalStaffLineThickness * ScoreScale;
        public DrawableLineVertical OpeningLine =>
            new(canvasLeft, canvasTop, staffSystem.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), VerticalLineThickness, color: baseColor);
        public DrawableLineVertical ClosingLine
        {
            get
            {
                var isLast = staffSystem.EnumerateMeasures().Last().IsLastInScore;
                var x = isLast ? canvasLeft + length - 1 : canvasLeft + length;

                return new DrawableLineVertical(x, canvasTop, staffSystem.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), VerticalLineThickness, color: baseColor);
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

                return new DrawableLineVertical(canvasLeft + length, canvasTop - (HorizontalLineThickness / 2), staffSystem.CalculateHeight(globalLineSpacing, scoreLayoutDictionary) + HorizontalLineThickness, thickness: thickness, color: baseColor);
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
        public double ScoreScale =>
            scoreLayoutDictionary.Scale;



        public VisualStaffSystem(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, double globalLineSpacing, IVisualSystemMeasureFactory systemMeasureFactory, ColorARGB baseColor, ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.staffSystem = staffSystem;
            this.systemMeasureFactory = systemMeasureFactory;
            this.baseColor = baseColor;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.length = length;
            this.globalLineSpacing = globalLineSpacing;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
        }



        public double CalculateOpeningPadding()
        {
            var firstMeasure = staffSystem.EnumerateMeasures().First();
            var keySignature = firstMeasure.KeySignature;
            var spaceForClef = (VisualStaff.SpaceUntilClef * ScoreScale) + (VisualStaff.ClefSpacing * ScoreScale);
            var spaceForKeySignature = (keySignature.DefaultFlats ? keySignature.NumberOfFlats() : keySignature.NumberOfSharps()) * VisualStaff.KeySignatureGlyphSpacing * ScoreScale;
            var spaceForTimeSignature = firstMeasure.IndexInScore == 0 ? VisualStaff.TimeSignatureSpacing * ScoreScale : 0;

            var reservedSpace = spaceForClef + spaceForKeySignature + spaceForTimeSignature;
            return reservedSpace;
        }
        public IEnumerable<BaseContentWrapper> ConstructSystemMeasures()
        {
            var lengthWithoutAdjustment = staffSystem.EnumerateMeasures().Select(m => m.ReadLayout().Width).Sum();
            var totalLength = length;
            var paddingStart = CalculateOpeningPadding();
            var availableLength = totalLength - paddingStart;

            var _canvasLeft = canvasLeft + paddingStart;

            foreach (var measure in staffSystem.EnumerateMeasures())
            {
                var measureLayout = measure.ReadLayout();
                var measureWidth = measureLayout.Width;

                measureWidth = MathUtils.Map(measureWidth, 0, lengthWithoutAdjustment, 0, availableLength);

                var systemMeasure = systemMeasureFactory.CreateContent(measure, staffSystem, _canvasLeft, canvasTop, measureWidth, globalLineSpacing, baseColor);
                yield return systemMeasure;

                _canvasLeft += measureWidth;
            }
        }
        public IEnumerable<BaseContentWrapper> ConstructStaffGroups()
        {
            var heightOnCanvas = canvasTop;

            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var instrumentScale = scoreLayoutDictionary.GetInstrumentScale(staffGroup.Instrument);

                VisualStaffGroup _staffGroup = new(
                    staffGroup,
                    canvasLeft,
                    heightOnCanvas,
                    length,
                    globalLineSpacing,
                    ScoreScale,
                    instrumentScale,
                    baseColor,
                    scoreLayoutDictionary);
                yield return _staffGroup;

                heightOnCanvas += staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary);
                heightOnCanvas += staffGroup.ReadLayout().DistanceToNext;
            }
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft, canvasLeft + length, canvasTop, canvasTop + staffSystem.CalculateHeight(globalLineSpacing, scoreLayoutDictionary));
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
        }
    }
}
