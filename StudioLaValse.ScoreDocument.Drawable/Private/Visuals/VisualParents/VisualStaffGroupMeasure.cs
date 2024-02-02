using StudioLaValse.ScoreDocument.Editor;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text.RegularExpressions;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents
{
    internal sealed class VisualStaffGroupMeasure : BaseSelectableParent<IUniqueScoreElement>
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasTop;
        private readonly double canvasLeft;
        private readonly double width;
        private readonly double paddingLeft;
        private readonly double paddingRight;
        private readonly ColorARGB color;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly IInstrumentMeasureReader source;




        public TimeSignature TimeSignature =>
            source.TimeSignature;
        public KeySignature KeySignature =>
            source.ReadMeasureContext().ReadLayout().KeySignature;
        public KeySignature? InvalidatesNext
        {
            get
            {
                if (source.ReadMeasureContext().TryReadNext(out var next))
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
        public BaseDrawableElement LineLeft
        {
            get
            {
                return new DrawableLineVertical(canvasLeft, canvasTop, staffGroup.CalculateHeight(), 0.1, color);
            }
        }
        public BaseDrawableElement LineRight
        {
            get
            {
                return new DrawableLineVertical(canvasLeft + width, canvasTop, staffGroup.CalculateHeight(), 0.1, color);
            }
        }
        public double DrawableWidth =>
            width - paddingLeft - paddingRight;


        public VisualStaffGroupMeasure(IInstrumentMeasureReader source, IStaffGroupReader staffGroup, double canvasTop, double canvasLeft, double width, double paddingLeft, double paddingRight, ColorARGB color, IVisualNoteGroupFactory visualNoteGroupFactory, ISelection<IUniqueScoreElement> selection) : base(source, selection)
        {
            this.staffGroup = staffGroup;
            this.canvasTop = canvasTop;
            this.canvasLeft = canvasLeft;
            this.width = width;
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
            this.color = color;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
            this.source = source;
        }



        public IEnumerable<BaseContentWrapper> ConstructNoteGroups()
        {
            foreach (var voice in source.EnumerateVoices())
            {
                var chain = source.ReadBlockChainAt(voice);
                foreach(var element in ConstructNoteGroups(chain))
                {
                    yield return element;
                }   
            }
        }

        public IEnumerable<BaseContentWrapper> ConstructNoteGroups(IMeasureBlockChainReader blockChain)
        {
            var blocks = blockChain.ReadBlocks();
            foreach(var chordGroup in blocks)
            {
                var elements = chordGroup.ReadChords().SelectMany(c => c.ReadNotes());
                var anyOnStaff = elements.Any(ele => ele.ReadLayout().StaffIndex < staffGroup.ReadLayout().NumberOfStaves);
                if (!anyOnStaff)
                {
                    continue;
                }

                var firstChord = chordGroup.ReadChords().First();
                var xParameter = MathUtils.Map((double)firstChord.Position.Decimal, 0, (double)source.TimeSignature.Decimal, 0, 1);
                var canvasLeft = XPositionFromParameter(xParameter + firstChord.ReadLayout().XOffset);
                var allowedSpace = MathUtils.Map((double)chordGroup.RythmicDuration.Decimal, 0, (double)source.TimeSignature.Decimal, 0, DrawableWidth);

                if (chordGroup.Grace)
                {
                    var graceSpacing = 0.1;
                    allowedSpace = chordGroup.ReadChords().Count() * graceSpacing;
                    canvasLeft -= allowedSpace;
                }

                var visualNoteGroup = visualNoteGroupFactory.Build(chordGroup, staffGroup, canvasTop, canvasLeft, allowedSpace, color);
                yield return visualNoteGroup;
            }
        }

        public IEnumerable<VisualStaffMeasure> ConstructStaffMeasures()
        {
            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.ReadStaves())
            {
                var stafflayout = staff.ReadLayout();
                var measureClef = source.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var lastClefChange = source.ReadLayout().ClefChanges.LastOrDefault(c => c.StaffIndex == staff.IndexInStaffGroup)?.Clef ?? measureClef;

                source.TryReadNext(out var nextMeasure);
                var nextClefLayout = nextMeasure?.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
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
                    source.ReadLayout().ClefChanges.Where(c => c.StaffIndex == staff.IndexInStaffGroup),
                    source.ReadMeasureContext().ReadLayout().IsNewSystem,
                    canvasLeft,
                    width,
                    paddingLeft,
                    DrawableWidth,
                    _canvasTop,
                    stafflayout.LineSpacing);

                yield return el;

                _canvasTop += 4 * stafflayout.LineSpacing + stafflayout.DistanceToNext;
            }
        }



        public double XPositionFromParameter(double parameter) =>
            canvasLeft + paddingLeft + DrawableWidth * parameter;



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + paddingLeft, canvasLeft + width - paddingRight, canvasTop, canvasTop + staffGroup.CalculateHeight());
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
