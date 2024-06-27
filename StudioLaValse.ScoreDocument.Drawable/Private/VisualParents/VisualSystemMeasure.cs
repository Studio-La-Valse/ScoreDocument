using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualSystemMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IScoreMeasure scoreMeasure;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreDocument scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IStaffSystem staffSystem;
        private readonly double width;
        private readonly double lineSpacing;
        private readonly double canvasLeft;
        private readonly double canvasTop;


        public IScoreMeasure Layout => 
            scoreMeasure;
        public double PaddingRight =>
            Layout.PaddingRight * ScoreScale + NextMeasureKeyPadding * ScoreScale;
        public double Height =>
            unitToPixelConverter.UnitsToPixels(staffSystem.CalculateHeight(lineSpacing, scoreLayoutDictionary));
        public double PaddingLeft =>
            Layout.PaddingLeft * ScoreScale;
        public double ScoreScale =>
            scoreLayoutDictionary.Scale;
        public double NextMeasureKeyPadding
        {
            get
            {
                if (InvalidatesNext is null)
                {
                    return 0;
                }

                var keySignature = scoreMeasure.KeySignature;
                var flats = keySignature.DefaultFlats;
                var numberOfAccidentals = flats ?
                    keySignature.EnumerateFlats().Count() :
                    keySignature.EnumerateSharps().Count();

                return 1 + numberOfAccidentals;
            }
        }
        public KeySignature? InvalidatesNext
        {
            get
            {
                if (!scoreMeasure.TryReadNext(out var nextMeasure))
                {
                    return null;
                }

                var nextKeySignature = nextMeasure.KeySignature;
                return nextKeySignature.Equals(scoreMeasure.KeySignature) ? null : nextKeySignature;
            }
        }





        public VisualSystemMeasure(IScoreMeasure scoreMeasure,
                                   IStaffSystem staffSystem,
                                   double canvasLeft,
                                   double canvasTop,
                                   double width,
                                   double lineSpacing,
                                   ISelection<IUniqueScoreElement> selection,
                                   IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory,
                                   IScoreDocument scoreLayoutDictionary,
                                   IUnitToPixelConverter unitToPixelConverter) : 
            base(scoreMeasure, selection)
        {
            this.scoreMeasure = scoreMeasure;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
            this.canvasTop = canvasTop;
            this.staffSystem = staffSystem;
            this.width = width;
            this.lineSpacing = lineSpacing;
            this.canvasLeft = canvasLeft;
        }




        private IEnumerable<BaseContentWrapper> ConstructStaffGroupMeasures()
        {
            var canvasTop = this.canvasTop;
            var positions = scoreMeasure
                .EnumeratePositions(scoreLayoutDictionary.Scale)
                .Remap(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight)
                .PositionsOnly(out var positionSpace);
            var scoreScale = scoreLayoutDictionary.Scale;
            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var ribbonMesaure = scoreMeasure.ReadMeasure(staffGroup.IndexInSystem);
                var wrapper = visualInstrumentMeasureFactory.CreateContent(ribbonMesaure, staffGroup, positions, canvasTop, canvasLeft, width, PaddingLeft, PaddingRight, lineSpacing, positionSpace);
                yield return wrapper;

                canvasTop += unitToPixelConverter.UnitsToPixels(staffGroup.CalculateHeight(lineSpacing, scoreLayoutDictionary));
                canvasTop += unitToPixelConverter.UnitsToPixels(staffGroup.DistanceToNext * scoreScale);
            }
        }


        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight, canvasTop, canvasTop + Height);
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield break;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            List<BaseContentWrapper> wrappers = new(ConstructStaffGroupMeasures())
            {
                new SimpleGhost(this)
            };

            return wrappers;
        }
        public override bool Respond(XY point)
        {
            return BoundingBox().Contains(point);
        }
        public override bool OnMouseMove(XY mousePosition)
        {
            var currentlyMouseOver = IsMouseOver;
            IsMouseOver = BoundingBox().Contains(mousePosition);
            return currentlyMouseOver != IsMouseOver;
        }
    }
}
