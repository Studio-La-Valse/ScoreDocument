namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffGroupMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasTop;
        private readonly double canvasLeft;
        private readonly double width;
        private readonly double paddingLeft;
        private readonly double paddingRight;
        private readonly bool firstMeasure;
        private readonly ColorARGB color;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;
        private readonly IInstrumentMeasureReader source;



        public StaffGroupLayout Layout => scoreLayoutDictionary.StaffGroupLayout(staffGroup);
        public TimeSignature TimeSignature =>
            source.TimeSignature;
        public KeySignature KeySignature =>
            source.KeySignature;
        public KeySignature? InvalidatesNext
        {
            get
            {
                if (source.TryReadNext(out var next))
                {
                    var nextKeySignature = next.KeySignature;
                    if (nextKeySignature != KeySignature)
                    {
                        return nextKeySignature;
                    }
                }
                return null;
            }
        }
        public BaseDrawableElement LineLeft
        {
            get
            {
                return new DrawableLineVertical(canvasLeft, canvasTop, staffGroup.CalculateHeight(scoreLayoutDictionary), 0.1, color);
            }
        }
        public BaseDrawableElement LineRight
        {
            get
            {
                return new DrawableLineVertical(canvasLeft + width, canvasTop, staffGroup.CalculateHeight(scoreLayoutDictionary), 0.1, color);
            }
        }
        public double DrawableWidth =>
            width - paddingLeft - paddingRight;


        public VisualStaffGroupMeasure(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, bool firstMeasure, ColorARGB color, IVisualNoteGroupFactory visualNoteGroupFactory, ISelection<IUniqueScoreElement> selection, IScoreLayoutDictionary scoreLayoutDictionary) : base(source, selection)
        {
            this.staffGroup = staffGroup;
            this.canvasTop = canvasTop;
            this.canvasLeft = canvasLeft;
            this.width = width;
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
            this.firstMeasure = firstMeasure;
            this.color = color;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.source = source;
        }



        public IEnumerable<BaseContentWrapper> ConstructNoteGroups()
        {
            foreach (var voice in source.ReadVoices())
            {
                var chain = source.ReadBlockChainAt(voice);
                foreach (var element in ConstructNoteGroups(chain))
                {
                    yield return element;
                }
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructNoteGroups(IMeasureBlockChainReader blockChain)
        {
            var blocks = blockChain.ReadBlocks();
            foreach (var chordGroup in blocks)
            {
                var elements = chordGroup.ReadChords().SelectMany(c => c.ReadNotes());
                var anyOnStaff = elements.Any(ele =>
                {
                    var eleLayout = scoreLayoutDictionary.NoteLayout(ele);
                    return eleLayout.StaffIndex < Layout.NumberOfStaves;
                });
                if (!anyOnStaff)
                {
                    continue;
                }

                var firstChord = chordGroup.ReadChords().First();
                var firstChordLayout = scoreLayoutDictionary.ChordLayout(firstChord);
                var xParameter = MathUtils.Map((double)firstChord.Position.Decimal, 0, (double)source.TimeSignature.Decimal, 0, 1);
                var canvasLeft = XPositionFromParameter(xParameter + firstChordLayout.XOffset);
                var allowedSpace = MathUtils.Map((double)chordGroup.RythmicDuration.Decimal, 0, (double)source.TimeSignature.Decimal, 0, DrawableWidth);

                if (chordGroup.Grace)
                {
                    var graceSpacing = 0.1;
                    allowedSpace = chordGroup.ReadChords().Count() * graceSpacing;
                    canvasLeft -= allowedSpace;
                }

                var visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, source, canvasTop, canvasLeft, allowedSpace, color);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructStaffMeasures()
        {
            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
            {
                var staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                var instrumentMeasureLayout = scoreLayoutDictionary.InstrumentMeasureLayout(source);
                var measureClef = source.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                var lastClefChange = instrumentMeasureLayout.ClefChanges.LastOrDefault(c => c.StaffIndex == staff.IndexInStaffGroup)?.Clef ?? measureClef;

                source.TryReadNext(out var nextMeasure);
                var nextClefLayout = nextMeasure?.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                var invalidatingNextClef = nextClefLayout is null ?
                    null :
                    nextClefLayout.ClefSpecies == lastClefChange.ClefSpecies ?
                        null :
                        nextClefLayout;

                var el = new VisualStaffMeasure(
                    measureClef,
                    KeySignature,
                    source.MeasureIndex == 0 ? source.TimeSignature : null,
                    InvalidatesNext,
                    invalidatingNextClef,
                    instrumentMeasureLayout.ClefChanges.Where(c => c.StaffIndex == staff.IndexInStaffGroup),
                    firstMeasure,
                    canvasLeft,
                    width,
                    paddingLeft,
                    DrawableWidth,
                    _canvasTop,
                    staffLayout.LineSpacing);

                yield return el;

                _canvasTop += staff.CalculateHeight(scoreLayoutDictionary) + staffLayout.DistanceToNext;
            }
        }



        public double XPositionFromParameter(double parameter) =>
            canvasLeft + paddingLeft + DrawableWidth * parameter;



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + paddingLeft, canvasLeft + width - paddingRight, canvasTop, canvasTop + staffGroup.CalculateHeight(scoreLayoutDictionary));
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>()
            {
                LineLeft,
                LineRight
            };
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            var wrappers = new List<BaseContentWrapper>()
            {
                new SimpleGhost(this)
            };

            wrappers.AddRange(ConstructNoteGroups());
            wrappers.AddRange(ConstructStaffMeasures());

            return wrappers;
        }
        public override bool OnMouseMove(XY mousePosition)
        {
            var previousMouseOver = IsMouseOver;
            IsMouseOver = BoundingBox().Contains(mousePosition);
            return previousMouseOver != IsMouseOver;
        }
    }
}
