using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualStaffGroupMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IStaffGroup staffGroup;
        private readonly IReadOnlyDictionary<Position, double> positions;
        private readonly double canvasTop;
        private readonly double canvasLeft;
        private readonly double width;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly IInstrumentMeasure source;


        public double Scale =>
            staffGroup.InstrumentRibbon.Scale;
        public double MeasureDividerLineThickness =>
            staffGroup.VerticalStaffLineThickness * Scale;
        public double Height =>
            staffGroup.CalculateHeight();
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
        public BaseDrawableElement LineRight =>
            new DrawableLineVertical(canvasLeft + width, canvasTop, Height, MeasureDividerLineThickness, staffGroup.Color.Value.FromPrimitive());
        public double DrawableWidth =>
            width - source.PaddingLeft - source.PaddingRight;



        public VisualStaffGroupMeasure(IInstrumentMeasure source,
                                       IStaffGroup staffGroup,
                                       IReadOnlyDictionary<Position, double> positions,
                                       double canvasTop,
                                       double canvasLeft,
                                       double width,
                                       IGlyphLibrary glyphLibrary,
                                       IVisualNoteGroupFactory visualNoteGroupFactory,
                                       ISelection<IUniqueScoreElement> selection) : base(source, selection)
        {
            this.staffGroup = staffGroup;
            this.positions = positions;
            this.canvasTop = canvasTop;
            this.canvasLeft = canvasLeft;
            this.width = width;
            this.glyphLibrary = glyphLibrary;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
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

        public IEnumerable<BaseContentWrapper> ConstructNoteGroups(IMeasureBlockChain blockChain)
        {
            var blocks = blockChain.ReadBlocks();
            
            foreach (var chordGroup in blocks)
            {
                var elements = chordGroup.ReadNotes();
                var anyOnStaff = elements.Any(ele =>
                {
                    var eleLayout = ele;
                    return eleLayout.StaffIndex < staffGroup.NumberOfStaves;
                });
                if (!anyOnStaff)
                {
                    continue;
                }

                var visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, source, positions, canvasTop);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructStaffMeasures()
        {
            var instrumentScale = staffGroup.InstrumentRibbon.Scale;

            foreach (var (staff, canvasTop) in staffGroup.EnumerateFromTop(this.canvasTop))
            {
                var staffLayout = staff;
                var instrumentMeasureLayout = source;
                var clefChanges = instrumentMeasureLayout.EnumerateClefChanges().ToArray();
                var measureClef = source.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var lastClefChange = clefChanges.LastOrDefault(c => c.StaffIndex == staff.IndexInStaffGroup)?.Clef ?? measureClef;

                _ = source.TryReadNext(out var nextMeasure);
                var nextClefLayout = nextMeasure?.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var invalidatingNextClef = nextClefLayout is null ?
                    null :
                    nextClefLayout.ClefSpecies == lastClefChange.ClefSpecies ?
                        null :
                        nextClefLayout;

                clefChanges = clefChanges
                    .Where(c => c.StaffIndex == staff.IndexInStaffGroup)
                    .ToArray();

                var el = new VisualStaffMeasure(
                    staff,
                    measureClef,
                    InvalidatesNext,
                    invalidatingNextClef,
                    clefChanges,
                    positions,
                    canvasLeft,
                    width,
                    canvasTop,
                    glyphLibrary);

                yield return el;
            }
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + source.PaddingLeft, canvasLeft + width - source.PaddingRight, canvasTop, canvasTop + Height);
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return LineRight;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            List<BaseContentWrapper> wrappers =
            [
                new SimpleGhost(this), .. ConstructNoteGroups(), .. ConstructStaffMeasures()
            ];

            foreach (var item in wrappers)
            {
                yield return item;
            }
        }
        public override bool OnMouseMove(XY mousePosition)
        {
            var previousMouseOver = IsMouseOver;
            IsMouseOver = BoundingBox().Contains(mousePosition);
            return previousMouseOver != IsMouseOver;
        }
    }
}
