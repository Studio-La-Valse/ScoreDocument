using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffGroupMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly IReadOnlyDictionary<Position, double> positions;
        private readonly double canvasTop;
        private readonly double canvasLeft;
        private readonly double width;
        private readonly double paddingLeft;
        private readonly double paddingRight;
        private readonly double globalLineSpacing;
        private readonly double positionSpace;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IInstrumentMeasureReader source;


        public double MeasureDividerLineThickness =>
            unitToPixelConverter.UnitsToPixels(scoreLayoutDictionary.VerticalStaffLineThickness * scoreScale * instrumentScale);
        public double Height =>
           unitToPixelConverter.UnitsToPixels(staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary)); 
        public IStaffGroupLayout Layout =>
            staffGroup.ReadLayout();
        public KeySignature KeySignature =>
            source.ReadLayout().KeySignature;
       

        public KeySignature? InvalidatesNext
        {
            get
            {
                if (source.TryReadNext(out var next))
                {
                    var nextKeySignature = next.ReadLayout().KeySignature;
                    if (nextKeySignature != KeySignature)
                    {
                        return nextKeySignature;
                    }
                }
                return null;
            }
        }
        public BaseDrawableElement LineRight =>
            new DrawableLineVertical(canvasLeft + width, canvasTop, Height, MeasureDividerLineThickness, scoreLayoutDictionary.PageForegroundColor.FromPrimitive());
        public double DrawableWidth =>
            width - paddingLeft - paddingRight;



        public VisualStaffGroupMeasure(IInstrumentMeasureReader source,
                                       IStaffGroupReader staffGroup,
                                       IReadOnlyDictionary<Position, double> positions,
                                       double canvasTop,
                                       double canvasLeft,
                                       double width,
                                       double paddingLeft,
                                       double paddingRight,
                                       double globalLineSpacing,
                                       double positionSpace,
                                       double scoreScale,
                                       double instrumentScale,
                                       IVisualNoteGroupFactory visualNoteGroupFactory,
                                       ISelection<IUniqueScoreElement> selection,
                                       IScoreDocumentLayout scoreLayoutDictionary,
                                       IUnitToPixelConverter unitToPixelConverter) : base(source, selection)
        {
            this.staffGroup = staffGroup;
            this.positions = positions;
            this.canvasTop = canvasTop;
            this.canvasLeft = canvasLeft;
            this.width = width;
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
            this.globalLineSpacing = globalLineSpacing;
            this.positionSpace = positionSpace;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
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
                    var eleLayout = ele.ReadLayout();
                    return eleLayout.StaffIndex < Layout.NumberOfStaves;
                });
                if (!anyOnStaff)
                {
                    continue;
                }

                var visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, source, positions, canvasTop, globalLineSpacing, positionSpace);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructStaffMeasures()
        {
            var scoreLayout = scoreLayoutDictionary;
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;

            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves())
            {
                var staffLayout = staff.ReadLayout();
                var instrumentMeasureLayout = source.ReadLayout();
                var measureClef = source.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var lastClefChange = instrumentMeasureLayout.ClefChanges.LastOrDefault(c => c.StaffIndex == staff.IndexInStaffGroup)?.Clef ?? measureClef;

                _ = source.TryReadNext(out var nextMeasure);
                var nextClefLayout = nextMeasure?.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
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
                    instrumentScale,
                    unitToPixelConverter);

                yield return el;

                _canvasTop += unitToPixelConverter.UnitsToPixels(staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale));
                _canvasTop += unitToPixelConverter.UnitsToPixels(staffLayout.DistanceToNext);
            }
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + paddingLeft, canvasLeft + width - paddingRight, canvasTop, canvasTop + Height);
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return
            [
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
