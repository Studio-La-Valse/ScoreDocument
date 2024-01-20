using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualStaffGroup : BaseContentWrapper
    {
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasLeft;
        private readonly double length;
        private readonly ColorARGB color;
        private readonly double canvasTop;




        public DrawableText ID
        {
            get
            {
                var braceLeft = Brace?.TopLeftX ?? canvasLeft;
                var text = FirstMeasure.MeasureIndex == 0 ? staffGroup.ReadContext().ReadLayout().DisplayName : staffGroup.ReadContext().ReadLayout().AbbreviatedName;
                var height = staffGroup.CalculateHeight();
                var id = new DrawableText(braceLeft - 2, canvasTop + height / 2, text, 2, color, HorizontalTextOrigin.Right, VerticalTextOrigin.Center);

                return id;
            }
        }
        public DrawableScoreGlyph? Brace
        {
            get
            {
                if (staffGroup.ReadStaves().FirstOrDefault() is null)
                {
                    return null;
                }

                if (staffGroup.ReadContext().ReadLayout().Collapsed)
                {
                    return null;
                }

                if (staffGroup.CalculateHeight() < 0.1)
                {
                    return null;
                }

                var heightOfGroup = staffGroup.CalculateHeight();
                var heightOfOneStaff = 4 * 1.2;
                var scale = heightOfGroup / heightOfOneStaff;

                var glyph = GlyphLibrary.Brace;
                glyph.Scale = scale;

                return new DrawableScoreGlyph(canvasLeft - glyph.Width - 0.1, canvasTop, glyph, color);
            }
        }
        public IInstrumentMeasureReader FirstMeasure =>
            staffGroup.ReadMeasures().First();



        public VisualStaffGroup(IStaffGroupReader staffGroup, double canvasLeft, double canvasTop, double length, ColorARGB color)
        {
            this.staffGroup = staffGroup;
            this.canvasLeft = canvasLeft;
            this.length = length;
            this.color = color;
            this.canvasTop = canvasTop;
        }



        public IEnumerable<VisualStaff> ConstructStaves()
        {
            var _canvasTop = canvasTop;
            foreach (var staff in staffGroup.ReadStaves())
            {
                var clef = FirstMeasure.OpeningClefAtOrDefault(staff.IndexInStaffGroup);
                var keySignature = FirstMeasure.ReadMeasureContext().ReadLayout().KeySignature;
                var newStaff = new VisualStaff(
                    staff,
                    canvasLeft,
                    _canvasTop,
                    length,
                    clef,
                    keySignature,
                    color);

                yield return newStaff;

                _canvasTop += 4 * 1.2;
                _canvasTop += staff.ReadLayout().DistanceToNext;
            }
        }




        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var elements = new List<BaseDrawableElement>();

            if (staffGroup.ReadContext().ReadLayout().Collapsed)
            {
                elements.Add(new DrawableLineHorizontal(canvasTop, canvasLeft, length, 0.1, color));
            }
            else
            {
                if (Brace != null)
                {
                    elements.Add(Brace);
                }

                elements.Add(ID);
            }

            return elements;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return ConstructStaves();
        }
    }
}
