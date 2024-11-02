using StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;
using StudioLaValse.ScoreDocument.Models.V1;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation;

public static class ScoreBuilder
{
    public static IScoreBuilder Create(IScoreDocument scoreDocument, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
    {
        return new ScoreBuilderProxy(scoreDocument, commandManager, notifyEntityChanged);
    }
    public static IScoreBuilder Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ScoreDocumentModel scoreDocumentModel, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var scoreDocument = ScoreDocument.Create(scoreDocumentStyleTemplate, scoreDocumentModel, positionDictionaryBuilder);
        return new ScoreBuilderProxy(scoreDocument, commandManager, notifyEntityChanged);
    }
    public static IScoreBuilder Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ScoreDocumentModel scoreDocumentModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutModel, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var scoreDocument = ScoreDocument.Create(scoreDocumentStyleTemplate, scoreDocumentModel, scoreDocumentLayoutModel, positionDictionaryBuilder);
        return new ScoreBuilderProxy(scoreDocument, commandManager, notifyEntityChanged);
    }
}
