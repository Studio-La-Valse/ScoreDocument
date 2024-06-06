using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualSystemMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IScoreMeasureReader scoreMeasure;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IStaffSystemReader staffSystem;
        private readonly double width;
        private readonly double lineSpacing;
        private readonly ColorARGB color;
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly double canvasLeft;
        private readonly double canvasTop;


        public IScoreMeasureLayout Layout => 
            scoreMeasure.ReadLayout();
        public double PaddingRight =>
            Layout.PaddingRight + NextMeasureKeyPadding;
        public double PaddingLeft
        {
            get
            {
                var basePadding = Layout.PaddingLeft;

                return basePadding;
            }
        }
        public double NextMeasureKeyPadding
        {
            get
            {
                if (InvalidatesNext is null)
                {
                    return 0;
                }

                var keySignature = scoreMeasure.ReadLayout().KeySignature;
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

                var nextKeySignature = nextMeasure.ReadLayout().KeySignature;
                return nextKeySignature.Equals(scoreMeasure.ReadLayout().KeySignature) ? null : nextKeySignature;
            }
        }





        public VisualSystemMeasure(IScoreMeasureReader scoreMeasure, IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double width, double lineSpacing, ColorARGB color, ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, IScoreDocumentLayout scoreLayoutDictionary) : base(scoreMeasure, selection)
        {
            this.scoreMeasure = scoreMeasure;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.canvasTop = canvasTop;
            this.staffSystem = staffSystem;
            this.width = width;
            this.lineSpacing = lineSpacing;
            this.color = color;
            this.selection = selection;
            this.canvasLeft = canvasLeft;
        }




        private IEnumerable<BaseContentWrapper> ConstructStaffGroupMeasures()
        {
            var _canvasTop = canvasTop;
            var positions = scoreMeasure
                .EnumeratePositions()
                .Remap(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight)
                .PositionsOnly();

            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var ribbonMesaure = scoreMeasure.ReadMeasure(staffGroup.IndexInSystem);
                var wrapper = visualInstrumentMeasureFactory.CreateContent(ribbonMesaure, staffGroup, positions, _canvasTop, canvasLeft, width, PaddingLeft, PaddingRight, lineSpacing, color);
                yield return wrapper;

                var staffGroupLayout = staffGroup.ReadLayout();
                _canvasTop += staffGroup.CalculateHeight(lineSpacing, scoreLayoutDictionary);
                _canvasTop += staffGroupLayout.DistanceToNext;
            }
        }


        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight, canvasTop, canvasTop + staffSystem.CalculateHeight(lineSpacing, scoreLayoutDictionary));
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            List<BaseDrawableElement> list = [];

            return list;
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
