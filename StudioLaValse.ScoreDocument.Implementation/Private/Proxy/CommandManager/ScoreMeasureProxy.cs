using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

internal class ScoreMeasureProxy(ScoreMeasure source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector) : IScoreMeasure
{
    private readonly ScoreMeasure source = source;
    private readonly ICommandManager commandManager = commandManager;
    private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged = notifyEntityChanged;
    private readonly ILayoutSelector layoutSelector = layoutSelector;

    public int IndexInScore => source.IndexInScore;

    public TimeSignature TimeSignature => source.TimeSignature;

    public int Id => source.Id;

    public Guid Guid => source.Guid;

    public bool IsLastInScore => source.IsLastInScore;

    public IScoreMeasureLayout Layout => layoutSelector.ScoreMeasureLayout(source);

    public TemplateProperty<KeySignature> KeySignature => Layout.KeySignature.WithRerender(notifyEntityChanged, source.HostDocument, commandManager);

    public TemplateProperty<double?> PaddingBottom => Layout.PaddingBottom.WithRerender(notifyEntityChanged, source.HostDocument, commandManager);

    public ReadonlyTemplateProperty<double> PaddingLeft => Layout.PaddingLeft;

    public ReadonlyTemplateProperty<double> PaddingRight => Layout.PaddingRight;

    public ReadonlyTemplateProperty<double> Scale => Layout.Scale;


    public bool TryReadNext([NotNullWhen(true)] out IScoreMeasure? next)
    {
        _ = source.TryReadNext(out var _next);
        next = _next?.Proxy(commandManager, notifyEntityChanged, layoutSelector);
        return next != null;
    }

    public bool TryReadPrevious([NotNullWhen(true)] out IScoreMeasure? previous)
    {
        _ = source.TryReadPrevious(out var _previous);
        previous = _previous?.Proxy(commandManager, notifyEntityChanged, layoutSelector);
        return previous != null;
    }

    public IInstrumentMeasure ReadMeasure(int ribbonIndex)
    {
        return source.GetMeasureCore(ribbonIndex).Proxy(commandManager, notifyEntityChanged, layoutSelector);
    }

    public IEnumerable<IInstrumentMeasure> ReadMeasures()
    {
        return source.EnumerateMeasuresCore().Select(e => e.Proxy(commandManager, notifyEntityChanged, layoutSelector));
    }

    public IEnumerable<IScoreElement> EnumerateChildren()
    {
        return ReadMeasures();
    }

    public bool Equals(IUniqueScoreElement? other)
    {
        if (other is null)
        {
            return false;
        }

        return other.Id == Id;
    }
}
