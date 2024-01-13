using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Drawable.Interaction.ContentWrappers;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Ghosts;
using StudioLaValse.ScoreDocument.Drawable.Interfaces;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.VisualParents
{
    public sealed class VisualSystemMeasure : BaseSelectableParent<IUniqueScoreElement>, IVisualScoreMeasure
    {
        private readonly IScoreMeasureReader scoreMeasure;
        private readonly IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory;
        private readonly IStaffSystemReader staffSystem;
        private readonly double width;
        private readonly ColorARGB color;
        private readonly double canvasLeft;
        private readonly double canvasTop;



        public double PaddingRight =>
            scoreMeasure.ReadLayout().PaddingRight + NextMeasureKeyPadding;
        public double PaddingLeft
        {
            get
            {
                var basePadding = scoreMeasure.ReadLayout().PaddingLeft;

                if (scoreMeasure.ReadLayout().IsNewSystem)
                {
                    var keySignature = scoreMeasure.ReadLayout().KeySignature;
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
                if (nextKeySignature.Equals(scoreMeasure.ReadLayout().KeySignature))
                {
                    return null;
                }

                return nextKeySignature;
            }
        }




        public VisualSystemMeasure(IScoreMeasureReader scoreMeasure, IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double width, ColorARGB color, ISelection<IUniqueScoreElement> selection, IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory) : base(scoreMeasure, selection)
        {
            this.scoreMeasure = scoreMeasure;
            this.visualInstrumentMeasureFactory = visualInstrumentMeasureFactory;
            this.canvasTop = canvasTop;
            this.staffSystem = staffSystem;
            this.width = width;
            this.color = color;
            this.canvasLeft = canvasLeft;
        }




        private IEnumerable<BaseContentWrapper> ConstructStaffGroupMeasures()
        {
            var measures = new List<BaseContentWrapper>();
            var _canvasTop = canvasTop;

            foreach (var staffGroup in staffSystem.ReadStaffGroups())
            {
                var ribbonMesaure = scoreMeasure.ReadMeasure(staffGroup.ReadContext().IndexInScore);
                var wrapper = visualInstrumentMeasureFactory.CreateContent(ribbonMesaure, staffGroup, _canvasTop, canvasLeft, width, PaddingLeft, PaddingRight, color);
                measures.Add(wrapper);

                _canvasTop += staffGroup.CalculateHeight();
                _canvasTop += staffGroup.ReadLayout().DistanceToNext;
            }

            return measures;
        }


        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft + PaddingLeft, canvasLeft + width - PaddingRight, canvasTop, canvasTop + staffSystem.CalculateHeight());
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
