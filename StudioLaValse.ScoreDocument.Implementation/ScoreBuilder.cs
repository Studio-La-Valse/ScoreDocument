using StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

namespace StudioLaValse.ScoreDocument.Implementation;

public static class ScoreBuilder
{
    public static IScoreBuilder Create(IScoreDocument scoreDocument, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
    {
        return new ScoreBuilderProxy(scoreDocument, commandManager, notifyEntityChanged);
    }
    public static IScoreBuilder Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ScoreDocumentModel scoreDocumentModel)
    {
        var scoreDocument = ScoreDocument.Create(scoreDocumentStyleTemplate, scoreDocumentModel);
        return new ScoreBuilderProxy(scoreDocument, commandManager, notifyEntityChanged);
    }
    public static IScoreBuilder Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ScoreDocumentModel scoreDocumentModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutModel)
    {
        var scoreDocument = ScoreDocument.Create(scoreDocumentStyleTemplate, scoreDocumentModel, scoreDocumentLayoutModel);
        return new ScoreBuilderProxy(scoreDocument, commandManager, notifyEntityChanged);
    }
}
