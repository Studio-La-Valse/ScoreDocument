using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualSystemMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IScoreMeasure scoreMeasure;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IStaffSystem staffSystem;
        private readonly double width;
        private readonly double canvasLeft;
        private readonly double canvasTop;


        public double Scale =>
            scoreMeasure.Scale;
        public IScoreMeasure Layout => 
            scoreMeasure;
        public double PaddingRight =>
            Layout.PaddingRight * Scale + NextMeasureKeyPadding * Scale;
        public double Height =>
            staffSystem.CalculateHeight();
        public double PaddingLeft =>
            Layout.PaddingLeft * Scale;
        public double NextMeasureKeyPadding
        {
            get
            {
                if (InvalidatesNext is null)
                {
                    return 0;
                }

                var keySignature = scoreMeasure.KeySignature.Value;
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

                var nextKeySignature = nextMeasure.KeySignature.Value;
                return nextKeySignature.Equals(scoreMeasure.KeySignature) ? null : nextKeySignature;
            }
        }





        public VisualSystemMeasure(IScoreMeasure scoreMeasure,
                                   IStaffSystem staffSystem,
                                   double canvasLeft,
                                   double canvasTop,
                                   double width,
                                   ISelection<IUniqueScoreElement> selection,
                                   IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory) : 
            base(scoreMeasure, selection)
        {
            this.scoreMeasure = scoreMeasure;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.canvasTop = canvasTop;
            this.staffSystem = staffSystem;
            this.width = width;
            this.canvasLeft = canvasLeft;
        }




        private IEnumerable<BaseContentWrapper> ConstructStaffGroupMeasures()
        {
            var positions = scoreMeasure
                .EnumeratePositions()
                .Remap(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight)
                .PositionsOnly(out var positionSpace);
            foreach (var (staffGroup, canvasTop) in staffSystem.EnumerateFromTop(this.canvasTop))
            {
                if (staffGroup.Visibility != Visibility.Visible)
                {
                    continue;
                }
                var ribbonMesaure = scoreMeasure.ReadMeasure(staffGroup.IndexInSystem);
                var wrapper = visualInstrumentMeasureFactory.CreateContent(ribbonMesaure, staffGroup, positions, canvasTop, canvasLeft, width);
                yield return wrapper;
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
