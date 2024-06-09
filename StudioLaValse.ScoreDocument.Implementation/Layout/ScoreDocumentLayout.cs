using StudioLaValse.ScoreDocument.Layout.Templates;
using ColorARGB = StudioLaValse.ScoreDocument.Layout.Templates.ColorARGB;
using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class ScoreDocumentLayout
    {
        public abstract ValueTemplateProperty<double> _Scale { get; }
        public abstract ValueTemplateProperty<double> _HorizontalStaffLineThickness { get; }
        public abstract ValueTemplateProperty<double> _VerticalStaffLineThickness { get; }
        public abstract ValueTemplateProperty<double> _StemLineThickness { get; }
        public abstract ValueTemplateProperty<double> _FirstSystemIndent { get; }
        public abstract ValueTemplateProperty<ColorARGB> _PageColor { get; }
        public abstract ValueTemplateProperty<ColorARGB> _PageForegroundColor { get; }
        public abstract ValueTemplateProperty<double> _ChordPositionFactor { get; }


        public double Scale
        {
            get
            {
                return _Scale.Value;
            }
            set
            {
                _Scale.Value = value;
            }
        }
        public double HorizontalStaffLineThickness
        {
            get
            {
                return _HorizontalStaffLineThickness.Value;
            }
            set
            {
                _HorizontalStaffLineThickness.Value = value;
            }
        }
        public double VerticalStaffLineThickness
        {
            get
            {
                return _VerticalStaffLineThickness.Value;
            }
            set
            {
                _VerticalStaffLineThickness.Value = value;
            }
        }
        public double StemLineThickness
        {
            get
            {
                return _StemLineThickness.Value;
            }
            set
            {
                _StemLineThickness.Value = value;
            }
        }
        public double FirstSystemIndent
        {
            get
            {
                return _FirstSystemIndent.Value;
            }
            set
            {
                _FirstSystemIndent.Value = value;
            }
        }
        public double ChordPositionFactor
        {
            get
            {
                return _ChordPositionFactor.Value;
            }
            set
            {
                _ChordPositionFactor.Value = value;
            }
        }
        public ColorARGB PageColor
        {
            get
            {
                return _PageColor.Value;
            }
            set
            {
                _PageColor.Value = value;
            }
        }
        public ColorARGB PageForegroundColor
        {
            get
            {
                return _PageForegroundColor.Value;
            }
            set
            {
                _PageForegroundColor.Value = value;
            }
        }



        public void Restore()
        {
            _Scale.Reset();
            _HorizontalStaffLineThickness.Reset();
            _VerticalStaffLineThickness.Reset();
            _StemLineThickness.Reset();
            _FirstSystemIndent.Reset();
            _PageColor.Reset();
            _PageForegroundColor.Reset();
            _ChordPositionFactor.Reset();
        }
        public void ApplyMemento(ScoreDocumentLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _Scale.Field = memento.Scale;
            _HorizontalStaffLineThickness.Field = memento.HorizontalStaffLineThickness;
            _VerticalStaffLineThickness.Field = memento.HorizontalStaffLineThickness;
            _StemLineThickness.Field = memento.StemLineThickness;
            _FirstSystemIndent.Field = memento.FirstSystemIndent;
            _ChordPositionFactor.Field = memento.ChordPositionFactor;
            _PageColor.Field = memento.PageColor?.Convert();
            _PageForegroundColor.Field = memento.ForegroundColor?.Convert();
        }
        public void ApplyMemento(ScoreDocumentLayoutModel? memento)
        {
            ApplyMemento(memento as ScoreDocumentLayoutMembers);
        }
    }

    public class AuthorScoreDocumentLayout : ScoreDocumentLayout, IScoreDocumentLayout, ILayout<ScoreDocumentLayoutMembers>
    {
        private readonly ScoreDocumentStyleTemplate styleTemplate;

        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<double> _HorizontalStaffLineThickness { get; }
        public override ValueTemplateProperty<double> _VerticalStaffLineThickness { get; }
        public override ValueTemplateProperty<double> _StemLineThickness { get; }
        public override ValueTemplateProperty<double> _FirstSystemIndent { get; }
        public override ValueTemplateProperty<double> _ChordPositionFactor { get; }
        public override ValueTemplateProperty<ColorARGB> _PageColor { get; }
        public override ValueTemplateProperty<ColorARGB> _PageForegroundColor { get; }


        public double PageMarginBottom => styleTemplate.PageStyleTemplate.MarginBottom;
        public double PageMarginLeft => styleTemplate.PageStyleTemplate.MarginLeft;
        public double PageMarginRight => styleTemplate.PageStyleTemplate.MarginRight;
        public double PageMarginTop => styleTemplate.PageStyleTemplate.MarginTop;
        public int PageHeight => styleTemplate.PageStyleTemplate.PageHeight;
        public int PageWidth => styleTemplate.PageStyleTemplate.PageWidth;

        public double StaffSystemPaddingBottom => styleTemplate.StaffSystemStyleTemplate.DistanceToNext;
        public double StaffGroupPaddingBottom => styleTemplate.StaffGroupStyleTemplate.DistanceToNext;
        public double StaffPaddingBottom => styleTemplate.StaffStyleTemplate.DistanceToNext;


        public AuthorScoreDocumentLayout(ScoreDocumentStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;

            _Scale = new ValueTemplateProperty<double>(() => styleTemplate.Scale);
            _HorizontalStaffLineThickness = new ValueTemplateProperty<double>(() => styleTemplate.HorizontalStaffLineThickness);
            _VerticalStaffLineThickness = new ValueTemplateProperty<double>(() => styleTemplate.VerticalStaffLineThickness);
            _StemLineThickness = new ValueTemplateProperty<double>(() => styleTemplate.StemLineThickness);
            _FirstSystemIndent = new ValueTemplateProperty<double>(() => styleTemplate.FirstSystemIndent);
            _ChordPositionFactor = new ValueTemplateProperty<double>(() => styleTemplate.ChordPositionFactor);
            _PageForegroundColor = new ValueTemplateProperty<ColorARGB>(() => styleTemplate.PageStyleTemplate.ForegroundColor);
            _PageColor = new ValueTemplateProperty<ColorARGB>(() => styleTemplate.PageStyleTemplate.PageColor);
        }

        public ScoreDocumentLayoutMembers GetMemento()
        {
            return new ScoreDocumentLayoutMembers()
            {
                Scale = _Scale.Field,
                HorizontalStaffLineThickness = _HorizontalStaffLineThickness.Field,
                VerticalStaffLineThickness = _VerticalStaffLineThickness.Field,
                StemLineThickness = _StemLineThickness.Field,
                FirstSystemIndent = _FirstSystemIndent.Field,
                ChordPositionFactor = _ChordPositionFactor.Field,
                PageColor = _PageColor.Field?.Convert(),
                ForegroundColor = _PageForegroundColor.Field?.Convert()
            };
        }
    }

    public class UserScoreDocumentLayout : ScoreDocumentLayout, IScoreDocumentLayout, ILayout<ScoreDocumentLayoutModel>
    {
        private readonly AuthorScoreDocumentLayout authorLayout;

        public override ValueTemplateProperty<double> _Scale { get; }
        public override ValueTemplateProperty<double> _HorizontalStaffLineThickness { get; }
        public override ValueTemplateProperty<double> _VerticalStaffLineThickness { get; }
        public override ValueTemplateProperty<double> _StemLineThickness { get; }
        public override ValueTemplateProperty<double> _FirstSystemIndent { get; }
        public override ValueTemplateProperty<double> _ChordPositionFactor { get; }
        public override ValueTemplateProperty<ColorARGB> _PageColor { get; }
        public override ValueTemplateProperty<ColorARGB> _PageForegroundColor { get; }

        public Guid Id { get; }

        public double PageMarginBottom => authorLayout.PageMarginBottom;
        public double PageMarginLeft => authorLayout.PageMarginLeft;
        public double PageMarginRight => authorLayout.PageMarginRight;
        public double PageMarginTop => authorLayout.PageMarginTop;
        public int PageHeight => authorLayout.PageHeight;
        public int PageWidth => authorLayout.PageWidth;

        public double StaffSystemPaddingBottom => authorLayout.StaffSystemPaddingBottom;
        public double StaffGroupPaddingBottom => authorLayout.StaffGroupPaddingBottom;
        public double StaffPaddingBottom => authorLayout.StaffPaddingBottom;


        public UserScoreDocumentLayout(AuthorScoreDocumentLayout authorLayout, Guid id)
        {
            this.authorLayout = authorLayout;

            _Scale = new ValueTemplateProperty<double>(() => authorLayout.Scale);
            _HorizontalStaffLineThickness = new ValueTemplateProperty<double>(() => authorLayout.HorizontalStaffLineThickness);
            _VerticalStaffLineThickness = new ValueTemplateProperty<double>(() => authorLayout.VerticalStaffLineThickness);
            _StemLineThickness = new ValueTemplateProperty<double>(() => authorLayout.StemLineThickness);
            _FirstSystemIndent = new ValueTemplateProperty<double>(() => authorLayout.FirstSystemIndent);
            _ChordPositionFactor = new ValueTemplateProperty<double>(() => authorLayout.ChordPositionFactor);
            _PageForegroundColor = new ValueTemplateProperty<ColorARGB>(() => authorLayout.PageForegroundColor);
            _PageColor = new ValueTemplateProperty<ColorARGB>(() => authorLayout.PageColor);

            Id = id;
        }



        public ScoreDocumentLayoutModel GetMemento()
        {
            return new ScoreDocumentLayoutModel()
            {
                Id = Id,
                Scale = _Scale.Field,
                HorizontalStaffLineThickness = _HorizontalStaffLineThickness.Field,
                VerticalStaffLineThickness = _VerticalStaffLineThickness.Field,
                StemLineThickness = _StemLineThickness.Field,
                FirstSystemIndent = _FirstSystemIndent.Field,
                ChordPositionFactor = _ChordPositionFactor.Field,
                PageColor = _PageColor.Field?.Convert(),
                ForegroundColor = _PageForegroundColor.Field?.Convert()
            };
        }
    }
}