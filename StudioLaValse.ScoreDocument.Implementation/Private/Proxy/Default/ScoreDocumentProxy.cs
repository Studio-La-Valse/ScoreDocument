using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Models;
using ColorARGB = StudioLaValse.ScoreDocument.StyleTemplates.ColorARGB;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default;

internal class ScoreDocumentProxy(ScoreDocumentCore score, ILayoutSelector layoutSelector) : IScoreDocument
{
    private readonly ScoreDocumentCore score = score;
    private readonly ILayoutSelector layoutSelector = layoutSelector;


    public IScoreDocumentLayout Layout => layoutSelector.ScoreDocumentLayout(score);



    public int NumberOfMeasures => score.NumberOfMeasures;

    public int NumberOfInstruments => score.NumberOfInstruments;

    public int Id => score.Id;



    public ReadonlyTemplateProperty<string> GlyphFamily => Layout.GlyphFamily;

    public ReadonlyTemplateProperty<double> FirstSystemIndent => Layout.FirstSystemIndent;

    public ReadonlyTemplateProperty<double> HorizontalStaffLineThickness => Layout.HorizontalStaffLineThickness;

    public ReadonlyTemplateProperty<double> Scale => Layout.Scale;

    public ReadonlyTemplateProperty<double> StemLineThickness => Layout.StemLineThickness;

    public ReadonlyTemplateProperty<double> VerticalStaffLineThickness => Layout.VerticalStaffLineThickness;



    public ReadonlyTemplateProperty<ColorARGB> PageColor => Layout.PageColor;

    public ReadonlyTemplateProperty<ColorARGB> PageForegroundColor => Layout.PageForegroundColor;

    public ReadonlyTemplateProperty<double> PageMarginBottom => Layout.PageMarginBottom;

    public ReadonlyTemplateProperty<double> PageMarginLeft => Layout.PageMarginLeft;

    public ReadonlyTemplateProperty<double> PageMarginRight => Layout.PageMarginRight;

    public ReadonlyTemplateProperty<double> PageMarginTop => Layout.PageMarginTop;

    public ReadonlyTemplateProperty<int> PageHeight => Layout.PageHeight;

    public ReadonlyTemplateProperty<int> PageWidth => Layout.PageWidth;



    public ReadonlyTemplateProperty<double> StaffSystemPaddingBottom => Layout.StaffSystemPaddingBottom;

    public ReadonlyTemplateProperty<double> StaffGroupPaddingBottom => Layout.StaffGroupPaddingBottom;

    public ReadonlyTemplateProperty<double> StaffPaddingBottom => Layout.StaffPaddingBottom;


    public void AddInstrumentRibbon(Instrument instrument)
    {
        score.AddInstrumentRibbon(instrument);
    }

    public void RemoveInstrumentRibbon(int indexInScore)
    {
        score.RemoveInstrumentRibbon(indexInScore);
    }

    public void AppendScoreMeasure(TimeSignature? timeSignature = null)
    {
        score.AppendScoreMeasure(timeSignature);
    }

    public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
    {
        score.InsertScoreMeasure(index, timeSignature);
    }

    public void RemoveScoreMeasure(int index)
    {
        score.RemoveScoreMeasure(index);
    }

    public void Clear()
    {
        score.Clear();
    }

    public IEnumerable<IScoreMeasure> ReadScoreMeasures()
    {
        return score.EnumerateMeasuresCore().Select(e => e.Proxy(layoutSelector));
    }

    public IEnumerable<IInstrumentRibbon> ReadInstrumentRibbons()
    {
        return score.EnumerateRibbonsCore().Select(e => e.Proxy(layoutSelector));
    }

    public IEnumerable<IScoreElement> EnumerateChildren()
    {
        foreach (var ribbon in ReadInstrumentRibbons())
        {
            yield return ribbon;
        }

        foreach (var measure in ReadScoreMeasures())
        {
            yield return measure;
        }

        foreach (var page in this.ReadPages())
        {
            yield return page;
        }
    }

    public IInstrumentRibbon ReadInstrumentRibbon(int indexInScore)
    {
        return score.GetInstrumentRibbonCore(indexInScore).Proxy(layoutSelector);
    }

    public IScoreMeasure ReadScoreMeasure(int indexInScore)
    {
        return score.GetScoreMeasureCore(indexInScore).Proxy(layoutSelector);
    }

    public bool Equals(IUniqueScoreElement? other)
    {
        return other is not null && other.Id == Id;
    }

    public ScoreDocumentModel Freeze()
    {
        return score.GetModel();
    }

    public ScoreDocumentLayoutDictionary FreezeLayout()
    {
        return score.GetLayoutDictionary();
    }
}
