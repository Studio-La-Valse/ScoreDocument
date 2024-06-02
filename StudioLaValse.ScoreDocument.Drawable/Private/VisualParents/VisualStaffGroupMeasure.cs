using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
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
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly ColorARGB color;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IInstrumentMeasureReader source;


        public double MeasureDividerLineThickness =>
            scoreLayoutDictionary.VerticalStaffLineThickness * scoreScale * instrumentScale;
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
        public BaseDrawableElement LineLeft =>
            new DrawableLineVertical(canvasLeft, canvasTop, staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), MeasureDividerLineThickness, ColorARGB.Transparant);
        public BaseDrawableElement LineRight =>
            new DrawableLineVertical(canvasLeft + width, canvasTop, staffGroup.CalculateHeight(globalLineSpacing, scoreLayoutDictionary), MeasureDividerLineThickness, color);
        public double DrawableWidth =>
            width - paddingLeft - paddingRight;



        public VisualStaffGroupMeasure(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, IReadOnlyDictionary<Position, double> positions, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, double globalLineSpacing, double scoreScale, double instrumentScale, ColorARGB color, IVisualNoteGroupFactory visualNoteGroupFactory, ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary) : base(source, selection)
        {
            this.staffGroup = staffGroup;
            this.positions = positions;
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
                foreach (var element in ConstructNoteGroups(chain, this.positions))
                {
                    yield return element;
                }
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructNoteGroups(IMeasureBlockChainReader blockChain, IReadOnlyDictionary<Position, double> positions)
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

                var visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, source, positions, canvasTop, globalLineSpacing, color);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructStaffMeasures()
        {
            var scoreLayout = scoreLayoutDictionary;
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;

            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.EnumerateStaves(Layout.NumberOfStaves))
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
