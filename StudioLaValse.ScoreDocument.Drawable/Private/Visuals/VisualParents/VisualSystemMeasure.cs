using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents
{
    internal sealed class VisualSystemMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IScoreMeasureReader scoreMeasure;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;
        private readonly IStaffSystem staffSystem;
        private readonly double width;
        private readonly bool firstMeasure;
        private readonly ColorARGB color;
        private readonly double canvasLeft;
        private readonly double canvasTop;


        public ScoreMeasureLayout Layout => scoreLayoutDictionary.GetOrDefault(scoreMeasure);

        public double PaddingRight =>
            Layout.PaddingRight + NextMeasureKeyPadding;
        public double PaddingLeft
        {
            get
            {
                var basePadding = Layout.PaddingLeft;

                if (Layout.IsNewSystem)
                {
                    var keySignature = scoreMeasure.KeySignature;
                    var flats = keySignature.DefaultFlats;
                    var numberOfAccidentals = flats ?
                        keySignature.EnumerateFlats().Count() :
                        keySignature.EnumerateSharps().Count();
                    basePadding -= 3;
                    basePadding += numberOfAccidentals * VisualStaff.KeySignatureGlyphSpacing;
                    basePadding += VisualStaff.ClefSpacing;

                    if (scoreMeasure.IndexInScore == 0)
                    {
                        basePadding += VisualStaff.KeySignatureSpacing;
                    }
                }

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
                if (nextKeySignature.Equals(scoreMeasure.KeySignature))
                {
                    return null;
                }

                return nextKeySignature;
            }
        }




        public VisualSystemMeasure(IScoreMeasureReader scoreMeasure, IStaffSystem staffSystem, double canvasLeft, double canvasTop, double width, bool firstMeasure, ColorARGB color, ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, IScoreLayoutDictionary scoreLayoutDictionary) : base(scoreMeasure, selection)
        {
            this.scoreMeasure = scoreMeasure;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.canvasTop = canvasTop;
            this.staffSystem = staffSystem;
            this.width = width;
            this.firstMeasure = firstMeasure;
            this.color = color;
            this.canvasLeft = canvasLeft;
        }




        private IEnumerable<BaseContentWrapper> ConstructStaffGroupMeasures()
        {
            var _canvasTop = canvasTop;

            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var ribbonMesaure = scoreMeasure.ReadMeasure(staffGroup.IndexInSystem);
                var wrapper = visualInstrumentMeasureFactory.CreateContent(ribbonMesaure, staffGroup, _canvasTop, canvasLeft, width, PaddingLeft, PaddingRight, firstMeasure, color);
                yield return wrapper;

                var staffGroupLayout = scoreLayoutDictionary.GetOrDefault(staffGroup);
                _canvasTop += staffGroup.CalculateHeight(scoreLayoutDictionary);
                _canvasTop += staffGroupLayout.DistanceToNext;
            }
        }


        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight, canvasTop, canvasTop + staffSystem.CalculateHeight(scoreLayoutDictionary));
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var list = new List<BaseDrawableElement>();

            return list;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            var wrappers = new List<BaseContentWrapper>(ConstructStaffGroupMeasures())
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
