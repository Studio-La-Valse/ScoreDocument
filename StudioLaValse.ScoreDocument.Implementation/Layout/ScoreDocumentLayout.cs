using ColorARGB = StudioLaValse.ScoreDocument.Templates.ColorARGB;
using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class ScoreDocumentLayout : IScoreDocumentLayout
    {
        public ReadonlyTemplateProperty<string> GlyphFamily { get; }
        public ReadonlyTemplateProperty<double> Scale { get; }
        public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness { get; }
        public ReadonlyTemplateProperty<double> VerticalStaffLineThickness { get; }
        public ReadonlyTemplateProperty<double> StemLineThickness { get; }
        public ReadonlyTemplateProperty<double> FirstSystemIndent { get; }

        public ReadonlyTemplateProperty<ColorARGB> PageColor { get; }
        public ReadonlyTemplateProperty<ColorARGB> PageForegroundColor { get; }
        public ReadonlyTemplateProperty<double> PageMarginBottom { get; }
        public ReadonlyTemplateProperty<double> PageMarginLeft { get; }
        public ReadonlyTemplateProperty<double> PageMarginRight { get; }
        public ReadonlyTemplateProperty<double> PageMarginTop { get; }
        public ReadonlyTemplateProperty<int> PageHeight { get; }
        public ReadonlyTemplateProperty<int> PageWidth { get; }

        public ReadonlyTemplateProperty<double> StaffSystemPaddingBottom { get; }
        public ReadonlyTemplateProperty<double> StaffGroupPaddingBottom { get; }
        public ReadonlyTemplateProperty<double> StaffPaddingBottom { get; }


        protected ScoreDocumentLayout(ScoreDocumentStyleTemplate styleTemplate)
        {
            GlyphFamily = new ReadonlyTemplatePropertyFromFunc<string>(() => "Bravura");
            Scale = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.Scale);
            HorizontalStaffLineThickness = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.HorizontalStaffLineThickness);
            VerticalStaffLineThickness = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.VerticalStaffLineThickness);
            StemLineThickness = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.StemLineThickness);
            FirstSystemIndent = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.FirstSystemIndent);
            PageForegroundColor = new ReadonlyTemplatePropertyFromFunc<ColorARGB>(() => styleTemplate.PageStyleTemplate.ForegroundColor);
            PageColor = new ReadonlyTemplatePropertyFromFunc<ColorARGB>(() => styleTemplate.PageStyleTemplate.PageColor);
            PageMarginBottom = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.PageStyleTemplate.MarginBottom);
            PageMarginLeft = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.PageStyleTemplate.MarginLeft);
            PageMarginRight = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.PageStyleTemplate.MarginRight);
            PageMarginTop = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.PageStyleTemplate.MarginTop);
            PageHeight = new ReadonlyTemplatePropertyFromFunc<int>(() => styleTemplate.PageStyleTemplate.PageHeight);
            PageWidth = new ReadonlyTemplatePropertyFromFunc<int>(() => styleTemplate.PageStyleTemplate.PageWidth);
            StaffSystemPaddingBottom = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.StaffSystemStyleTemplate.DistanceToNext);
            StaffGroupPaddingBottom = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.StaffGroupStyleTemplate.DistanceToNext);
            StaffPaddingBottom = new ReadonlyTemplatePropertyFromFunc<double>(() => styleTemplate.StaffStyleTemplate.DistanceToNext);
        }

        public void Restore()
        {

        }
        public void ApplyMemento(ScoreDocumentLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }
        }
        public void ApplyMemento(ScoreDocumentLayoutModel? memento)
        {
            ApplyMemento(memento as ScoreDocumentLayoutMembers);
        }
    }

    public class AuthorScoreDocumentLayout : ScoreDocumentLayout, ILayout<ScoreDocumentLayoutMembers>
    {
        public AuthorScoreDocumentLayout(ScoreDocumentStyleTemplate styleTemplate) : base(styleTemplate)
        {

        }

        public ScoreDocumentLayoutMembers GetMemento()
        {
            return new ScoreDocumentLayoutMembers()
            {

            };
        }
    }

    public class UserScoreDocumentLayout : ScoreDocumentLayout, ILayout<ScoreDocumentLayoutModel>
    {
        public Guid Id { get; }


        public UserScoreDocumentLayout(ScoreDocumentStyleTemplate styleTemplate, Guid id) : base(styleTemplate)
        {
            Id = id;
        }



        public ScoreDocumentLayoutModel GetMemento()
        {
            return new ScoreDocumentLayoutModel()
            {
                Id = Id,
            };
        }
    }
}