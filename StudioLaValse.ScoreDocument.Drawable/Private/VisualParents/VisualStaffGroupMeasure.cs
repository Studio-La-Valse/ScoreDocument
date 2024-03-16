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
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
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
                if (source.TryReadNext(out IInstrumentMeasureReader? next))
                {
                    KeySignature nextKeySignature = next.KeySignature;
                    if (nextKeySignature != KeySignature)
                    {
                        return nextKeySignature;
                    }
                }
                return null;
            }
        }
        public BaseDrawableElement LineLeft => new DrawableLineVertical(canvasLeft, canvasTop, staffGroup.CalculateHeight(scoreLayoutDictionary), 0.1, color);
        public BaseDrawableElement LineRight => new DrawableLineVertical(canvasLeft + width, canvasTop, staffGroup.CalculateHeight(scoreLayoutDictionary), 0.1, color);
        public double DrawableWidth =>
            width - paddingLeft - paddingRight;


        public VisualStaffGroupMeasure(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, bool firstMeasure, ColorARGB color, IVisualNoteGroupFactory visualNoteGroupFactory, ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary) : base(source, selection)
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
            foreach (int voice in source.ReadVoices())
            {
                IMeasureBlockChainReader chain = source.ReadBlockChainAt(voice);
                foreach (BaseContentWrapper element in ConstructNoteGroups(chain))
                {
                    yield return element;
                }
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructNoteGroups(IMeasureBlockChainReader blockChain)
        {
            IEnumerable<IMeasureBlockReader> blocks = blockChain.ReadBlocks();
            foreach (IMeasureBlockReader chordGroup in blocks)
            {
                IEnumerable<INoteReader> elements = chordGroup.ReadChords().SelectMany(c => c.ReadNotes());
                bool anyOnStaff = elements.Any(ele =>
                {
                    NoteLayout eleLayout = scoreLayoutDictionary.NoteLayout(ele);
                    return eleLayout.StaffIndex < Layout.NumberOfStaves;
                });
                if (!anyOnStaff)
                {
                    continue;
                }

                IChordReader firstChord = chordGroup.ReadChords().First();
                ChordLayout firstChordLayout = scoreLayoutDictionary.ChordLayout(firstChord);
                double xParameter = MathUtils.Map((double)firstChord.Position.Decimal, 0, (double)source.TimeSignature.Decimal, 0, 1);
                double canvasLeft = XPositionFromParameter(xParameter + firstChordLayout.XOffset);
                double allowedSpace = MathUtils.Map((double)chordGroup.RythmicDuration.Decimal, 0, (double)source.TimeSignature.Decimal, 0, DrawableWidth);

                if (chordGroup.Grace)
                {
                    double graceSpacing = 0.1;
                    allowedSpace = chordGroup.ReadChords().Count() * graceSpacing;
                    canvasLeft -= allowedSpace;
                }

                BaseContentWrapper visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, source, canvasTop, canvasLeft, allowedSpace, color);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructStaffMeasures()
        {
            double _canvasTop = canvasTop;
            var lineSpacing = Layout.LineSpacing;
            foreach (IStaffReader staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
            {
                StaffLayout staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                InstrumentMeasureLayout instrumentMeasureLayout = scoreLayoutDictionary.InstrumentMeasureLayout(source);
                Clef measureClef = source.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                Clef lastClefChange = instrumentMeasureLayout.ClefChanges.LastOrDefault(c => c.StaffIndex == staff.IndexInStaffGroup)?.Clef ?? measureClef;

                _ = source.TryReadNext(out IInstrumentMeasureReader? nextMeasure);
                Clef? nextClefLayout = nextMeasure?.OpeningClefAtOrDefault(staff.IndexInStaffGroup, scoreLayoutDictionary);
                Clef? invalidatingNextClef = nextClefLayout is null ?
                    null :
                    nextClefLayout.ClefSpecies == lastClefChange.ClefSpecies ?
                        null :
                        nextClefLayout;

                VisualStaffMeasure el = new(
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
                    lineSpacing);

                yield return el;

                _canvasTop += staff.CalculateHeight(lineSpacing) + staffLayout.DistanceToNext;
            }
        }



        public double XPositionFromParameter(double parameter)
        {
            return canvasLeft + paddingLeft + DrawableWidth * parameter;
        }

        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + paddingLeft, canvasLeft + width - paddingRight, canvasTop, canvasTop + staffGroup.CalculateHeight(scoreLayoutDictionary));
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
            bool previousMouseOver = IsMouseOver;
            IsMouseOver = BoundingBox().Contains(mousePosition);
            return previousMouseOver != IsMouseOver;
        }
    }
}
