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
        private readonly double globalLineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly ColorARGB color;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IInstrumentMeasureReader source;


        public double MeasureDividerLineThickness =>
            scoreLayoutDictionary.DocumentLayout().VerticalStaffLineThickness * scoreScale * instrumentScale;
        public StaffGroupLayout Layout =>
            scoreLayoutDictionary.StaffGroupLayout(staffGroup);
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
        public BaseDrawableElement LineLeft =>
            new DrawableLineVertical(canvasLeft, canvasTop, staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), MeasureDividerLineThickness, ColorARGB.Transparant);
        public BaseDrawableElement LineRight =>
            new DrawableLineVertical(canvasLeft + width, canvasTop, staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), MeasureDividerLineThickness, color);
        public double DrawableWidth =>
            width - paddingLeft - paddingRight;



        public VisualStaffGroupMeasure(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, double globalLineSpacing, double scoreScale, double instrumentScale, ColorARGB color, IVisualNoteGroupFactory visualNoteGroupFactory, ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary) : base(source, selection)
        {
            this.staffGroup = staffGroup;
            this.canvasTop = canvasTop;
            this.canvasLeft = canvasLeft;
            this.width = width;
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.color = color;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
            this.selection = selection;
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

                var visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, source, canvasTop, canvasLeft, allowedSpace, globalLineSpacing, color);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructStaffMeasures()
        {
            var scoreLayout = scoreLayoutDictionary.DocumentLayout();
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = 1d;
            if (scoreLayout.InstrumentScales.TryGetValue(staffGroup.Instrument, out var value))
            {
                instrumentScale = value;
            }

            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
            {
                var staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                var instrumentMeasureLayout = scoreLayoutDictionary.InstrumentMeasureLayout(source);
                var measureClef = source.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                var lastClefChange = instrumentMeasureLayout.ClefChanges.LastOrDefault(c => c.StaffIndex == staff.IndexInStaffGroup)?.Clef ?? measureClef;

                _ = source.TryReadNext(out var nextMeasure);
                var nextClefLayout = nextMeasure?.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                var invalidatingNextClef = nextClefLayout is null ?
                    null :
                    nextClefLayout.ClefSpecies == lastClefChange.ClefSpecies ?
                        null :
                        nextClefLayout;

                VisualStaffMeasure el = new(
                    staff,
                    measureClef,
                    InvalidatesNext,
                    invalidatingNextClef,
                    instrumentMeasureLayout.ClefChanges.Where(c => c.StaffIndex == staff.IndexInStaffGroup),
                    canvasLeft,
                    width,
                    paddingLeft,
                    DrawableWidth,
                    _canvasTop,
                    globalLineSpacing,
                    scoreScale,
                    instrumentScale);

                yield return el;

                _canvasTop += staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale) + staffLayout.DistanceToNext;
            }
        }



        public double XPositionFromParameter(double parameter)
        {
            return canvasLeft + paddingLeft + (DrawableWidth * parameter);
        }

        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + paddingLeft, canvasLeft + width - paddingRight, canvasTop, canvasTop + staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary));
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return
            [
                LineLeft,
                LineRight
            ];
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            List<BaseContentWrapper> wrappers =
            [
                new SimpleGhost(this), .. ConstructNoteGroups(), .. ConstructStaffMeasures()
            ];

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
