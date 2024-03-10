namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualSystemMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IScoreMeasureReader scoreMeasure;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;
        private readonly IStaffSystemReader staffSystem;
        private readonly double width;
        private readonly bool firstMeasure;
        private readonly ColorARGB color;
        private readonly double canvasLeft;
        private readonly double canvasTop;


        public ScoreMeasureLayout Layout => scoreLayoutDictionary.ScoreMeasureLayout(scoreMeasure);

        public double PaddingRight =>
            Layout.PaddingRight.Value + NextMeasureKeyPadding;
        public double PaddingLeft
        {
            get
            {
                double basePadding = Layout.PaddingLeft.Value;

                if (firstMeasure)
                {
                    KeySignature keySignature = scoreMeasure.KeySignature;
                    bool flats = keySignature.DefaultFlats;
                    int numberOfAccidentals = flats ?
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

                KeySignature keySignature = scoreMeasure.KeySignature;
                bool flats = keySignature.DefaultFlats;
                int numberOfAccidentals = flats ?
                    keySignature.EnumerateFlats().Count() :
                    keySignature.EnumerateSharps().Count();

                return 1 + numberOfAccidentals;
            }
        }

        public KeySignature? InvalidatesNext
        {
            get
            {
                if (!scoreMeasure.TryReadNext(out IScoreMeasureReader? nextMeasure))
                {
                    return null;
                }

                KeySignature nextKeySignature = nextMeasure.KeySignature;
                return nextKeySignature.Equals(scoreMeasure.KeySignature) ? null : nextKeySignature;
            }
        }




        public VisualSystemMeasure(IScoreMeasureReader scoreMeasure, IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double width, bool firstMeasure, ColorARGB color, ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, IScoreLayoutProvider scoreLayoutDictionary) : base(scoreMeasure, selection)
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
            double _canvasTop = canvasTop;

            foreach (IStaffGroupReader staffGroup in staffSystem.EnumerateStaffGroups())
            {
                IInstrumentMeasureReader ribbonMesaure = scoreMeasure.ReadMeasure(staffGroup.IndexInSystem);
                BaseContentWrapper wrapper = visualInstrumentMeasureFactory.CreateContent(ribbonMesaure, staffGroup, _canvasTop, canvasLeft, width, PaddingLeft, PaddingRight, firstMeasure, color);
                yield return wrapper;

                StaffGroupLayout staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
                _canvasTop += staffGroup.CalculateHeight(scoreLayoutDictionary);
                _canvasTop += staffGroupLayout.DistanceToNext.Value;
            }
        }


        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight, canvasTop, canvasTop + staffSystem.CalculateHeight(scoreLayoutDictionary));
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
            bool currentlyMouseOver = IsMouseOver;
            IsMouseOver = BoundingBox().Contains(mousePosition);
            return currentlyMouseOver != IsMouseOver;
        }
    }
}
