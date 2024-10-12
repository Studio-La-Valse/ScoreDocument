using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffSystem : BaseContentWrapper
    {
        private readonly IStaffSystem staffSystem;
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly double canvasTop;


        public double VerticalLineThickness =>
            staffSystem.VerticalStaffLineThickness * staffSystem.Scale;
        public double HorizontalLineThickness =>
            staffSystem.HorizontalStaffLineThickness * staffSystem.Scale;
        public double Height =>
            staffSystem.CalculateHeight();
        public DrawableLineVertical OpeningLine =>
            new(canvasLeft, canvasTop, Height, VerticalLineThickness, staffSystem.Color.Value.FromPrimitive());
        public DrawableLineVertical ClosingLine
        {
            get
            {
                var isLast = staffSystem.EnumerateMeasures().Last().IsLastInScore;
                var x = isLast ? canvasLeft + length - 1 : canvasLeft + length;

                return new DrawableLineVertical(x, canvasTop, Height, VerticalLineThickness, staffSystem.Color.Value.FromPrimitive());
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

                return new DrawableLineVertical(canvasLeft + length, canvasTop - (HorizontalLineThickness / 2), Height + HorizontalLineThickness, thickness, staffSystem.Color.Value.FromPrimitive());
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

                return new DrawableText(
                    canvasLeft,
                    canvasTop - 1,
                    index.ToString(),
                    2.5,
                    staffSystem.Color.Value.FromPrimitive(),
                    HorizontalTextOrigin.Left,
                    VerticalTextOrigin.Bottom,
                    new FontFamilyCore("Arial"));
            }
        }



        public VisualStaffSystem(IStaffSystem staffSystem,
                                 double canvasLeft,
                                 double canvasTop,
                                 double length,
                                 IGlyphLibrary glyphLibrary,
                                 IVisualSystemMeasureFactory systemMeasureFactory)
        {
            this.staffSystem = staffSystem;
            this.systemMeasureFactory = systemMeasureFactory;
            this.length = length;
            this.glyphLibrary = glyphLibrary;
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
            var keySignature = firstMeasure.KeySignature.Value;
            var spaceForClef = (VisualStaff.SpaceUntilClef * staffSystem.Scale) + (VisualStaff.ClefSpacing * staffSystem.Scale);
            var spaceForKeySignature = (keySignature.DefaultFlats ? keySignature.NumberOfFlats() : keySignature.NumberOfSharps()) * VisualStaff.KeySignatureGlyphSpacing * staffSystem.Scale;
            var spaceForTimeSignature = firstMeasure.IndexInScore == 0 ? VisualStaff.TimeSignatureSpacing * staffSystem.Scale : 0;

            var reservedSpace = spaceForClef + spaceForKeySignature + spaceForTimeSignature;
            return reservedSpace;
        }
        public IEnumerable<BaseContentWrapper> ConstructSystemMeasures()
        {
            var approximateSystemLength = staffSystem.EnumerateMeasures().Select(m => m.ApproximateWidth()).Sum();
            var paddingStart = CalculateOpeningPadding();
            var availableLength = length - paddingStart;

            var _canvasLeft = canvasLeft + paddingStart;

            foreach (var measure in staffSystem.EnumerateMeasures())
            {
                var measureWidth = measure.ApproximateWidth().Map(0, approximateSystemLength, 0, availableLength);

                var systemMeasure = systemMeasureFactory.CreateContent(measure, staffSystem, _canvasLeft, canvasTop, measureWidth);
                yield return systemMeasure;

                _canvasLeft += measureWidth;
            }
        }
        public IEnumerable<BaseContentWrapper> ConstructStaffGroups()
        {
            foreach (var (staffGroup, heightOnCanvas) in staffSystem.EnumerateFromTop(canvasTop))
            {
                VisualStaffGroup _staffGroup = new(
                    staffGroup,
                    canvasLeft,
                    heightOnCanvas,
                    length,
                    glyphLibrary);
                yield return _staffGroup;
            }
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft, canvasLeft + length, canvasTop, canvasTop + staffSystem.CalculateHeight());
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
