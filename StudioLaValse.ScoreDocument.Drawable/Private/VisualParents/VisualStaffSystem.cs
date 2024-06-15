using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffSystem : BaseContentWrapper
    {
        private readonly IStaffSystemReader staffSystem;
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double globalLineSpacing;
        private readonly double canvasTop;


        public double VerticalLineThickness =>
            unitToPixelConverter.UnitsToPixels(scoreLayoutDictionary.VerticalStaffLineThickness * ScoreScale);
        public double HorizontalLineThickness =>
            unitToPixelConverter.UnitsToPixels(scoreLayoutDictionary.HorizontalStaffLineThickness * ScoreScale);
        public double Height =>
            unitToPixelConverter.UnitsToPixels(staffSystem.CalculateHeight(globalLineSpacing, scoreLayoutDictionary));
        public DrawableLineVertical OpeningLine =>
            new(canvasLeft, canvasTop, Height, VerticalLineThickness, scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
        public DrawableLineVertical ClosingLine
        {
            get
            {
                var isLast = staffSystem.EnumerateMeasures().Last().IsLastInScore;
                var x = isLast ? canvasLeft + length - 1 : canvasLeft + length;

                return new DrawableLineVertical(x, canvasTop, Height, VerticalLineThickness, color: scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
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

                return new DrawableLineVertical(canvasLeft + length, canvasTop - (HorizontalLineThickness / 2), Height + HorizontalLineThickness, thickness: thickness, color: scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
            }
        }
        public DrawableText? MeasureCounter
        {
            get
            {
                if (!staffSystem.EnumerateMeasures().Any())
                {
                    return null;
                }

                if (staffSystem.EnumerateMeasures().First().IndexInScore == 0)
                {
                    return null;
                }

                var index = staffSystem.EnumerateMeasures().First().IndexInScore + 1;

                return new DrawableText(canvasLeft, canvasTop - 1, index.ToString(), 2, verticalAlignment: VerticalTextOrigin.Bottom, color: scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
            }
        }
        public double ScoreScale =>
            scoreLayoutDictionary.Scale;



        public VisualStaffSystem(IStaffSystemReader staffSystem,
                                 double canvasLeft,
                                 double canvasTop,
                                 double length,
                                 double globalLineSpacing,
                                 IVisualSystemMeasureFactory systemMeasureFactory,
                                 IScoreDocumentLayout scoreLayoutDictionary,
                                 IUnitToPixelConverter unitToPixelConverter)
        {
            this.staffSystem = staffSystem;
            this.systemMeasureFactory = systemMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
            this.length = length;
            this.globalLineSpacing = globalLineSpacing;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
        }



        public double CalculateOpeningPadding()
        {
            if (!staffSystem.EnumerateMeasures().Any())
            {
                throw new UnreachableException();   
            }

            var firstMeasure = staffSystem.EnumerateMeasures().First();
            var keySignature = firstMeasure.ReadLayout().KeySignature;
            var spaceForClef = (VisualStaff.SpaceUntilClef * ScoreScale) + (VisualStaff.ClefSpacing * ScoreScale);
            var spaceForKeySignature = (keySignature.DefaultFlats ? keySignature.NumberOfFlats() : keySignature.NumberOfSharps()) * VisualStaff.KeySignatureGlyphSpacing * ScoreScale;
            var spaceForTimeSignature = firstMeasure.IndexInScore == 0 ? VisualStaff.TimeSignatureSpacing * ScoreScale : 0;

            var reservedSpace = spaceForClef + spaceForKeySignature + spaceForTimeSignature;
            return reservedSpace;
        }
        public IEnumerable<BaseContentWrapper> ConstructSystemMeasures()
        {
            var scoreScale = scoreLayoutDictionary.Scale;
            var approximateSystemLength = staffSystem.EnumerateMeasures().Select(m => unitToPixelConverter.UnitsToPixels(m.ApproximateWidth(scoreScale))).Sum();
            var paddingStart = CalculateOpeningPadding();
            var availableLength = length - paddingStart;

            var _canvasLeft = canvasLeft + paddingStart;

            foreach (var measure in staffSystem.EnumerateMeasures())
            {
                var measureWidth = unitToPixelConverter.UnitsToPixels(measure.ApproximateWidth(scoreScale)).Map(0, approximateSystemLength, 0, availableLength);

                var systemMeasure = systemMeasureFactory.CreateContent(measure, staffSystem, _canvasLeft, canvasTop, measureWidth, globalLineSpacing);
                yield return systemMeasure;

                _canvasLeft += measureWidth;
            }
        }
        public IEnumerable<BaseContentWrapper> ConstructStaffGroups()
        {
            var heightOnCanvas = canvasTop;

            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;

                VisualStaffGroup _staffGroup = new(
                    staffGroup,
                    canvasLeft,
                    heightOnCanvas,
                    length,
                    globalLineSpacing,
                    ScoreScale,
                    instrumentScale,
                    scoreLayoutDictionary,
                    unitToPixelConverter);
                yield return _staffGroup;

                heightOnCanvas += unitToPixelConverter.UnitsToPixels(staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary));
                heightOnCanvas += unitToPixelConverter.UnitsToPixels(staffGroup.ReadLayout().DistanceToNext);
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
