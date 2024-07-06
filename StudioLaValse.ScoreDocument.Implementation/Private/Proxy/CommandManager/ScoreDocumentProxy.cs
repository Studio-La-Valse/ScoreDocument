using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;
using ColorARGB = StudioLaValse.ScoreDocument.StyleTemplates.ColorARGB;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

internal class ScoreDocumentProxy(ScoreDocumentCore score, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector) : IScoreDocument
{
    private readonly ScoreDocumentCore score = score;
    private readonly ICommandManager commandManager = commandManager;
    private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged = notifyEntityChanged;
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
        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var command = new MementoCommand<ScoreDocumentCore, ScoreDocumentMemento>(score, s => s.AddInstrumentRibbon(instrument)).ThenInvalidate(notifyEntityChanged, score);
        transaction.Enqueue(command);
    }

    public void RemoveInstrumentRibbon(int indexInScore)
    {
        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var command = new MementoCommand<ScoreDocumentCore, ScoreDocumentMemento>(score, s => s.RemoveInstrumentRibbon(indexInScore)).ThenInvalidate(notifyEntityChanged, score);
        transaction.Enqueue(command);
    }

    public void AppendScoreMeasure(TimeSignature? timeSignature = null)
    {
        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var command = new MementoCommand<ScoreDocumentCore, ScoreDocumentMemento>(score, s => s.AppendScoreMeasure(timeSignature)).ThenInvalidate(notifyEntityChanged, score);
        transaction.Enqueue(command);
    }

    public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
    {
        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var command = new MementoCommand<ScoreDocumentCore, ScoreDocumentMemento>(score, s => s.InsertScoreMeasure(index, timeSignature)).ThenInvalidate(notifyEntityChanged, score);
        transaction.Enqueue(command);
    }

    public void RemoveScoreMeasure(int index)
    {
        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var command = new MementoCommand<ScoreDocumentCore, ScoreDocumentMemento>(score, s => s.RemoveScoreMeasure(index)).ThenInvalidate(notifyEntityChanged, score);
        transaction.Enqueue(command);
    }

    public void Clear()
    {
        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var command = new MementoCommand<ScoreDocumentCore, ScoreDocumentMemento>(score, s => s.Clear()).ThenInvalidate(notifyEntityChanged, score);
        transaction.Enqueue(command);
    }

    public IEnumerable<IScoreMeasure> ReadScoreMeasures()
    {
        return score.EnumerateMeasuresCore().Select(e => e.Proxy(commandManager, notifyEntityChanged, layoutSelector));
    }

    public IEnumerable<IInstrumentRibbon> ReadInstrumentRibbons()
    {
        return score.EnumerateRibbonsCore().Select(e => e.Proxy(commandManager, notifyEntityChanged, layoutSelector));
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
        return score.GetInstrumentRibbonCore(indexInScore).Proxy(commandManager, notifyEntityChanged, layoutSelector);
    }

    public IScoreMeasure ReadScoreMeasure(int indexInScore)
    {
        return score.GetScoreMeasureCore(indexInScore).Proxy(commandManager, notifyEntityChanged, layoutSelector);
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
