using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;
using StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;
using StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default;
using StudioLaValse.ScoreDocument.Models.V1;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation;

public static class ScoreDocument
{
    private static ScoreDocumentCore CreateCore(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ScoreDocumentModel scoreDocumentModel, ScoreDocumentLayoutDictionary? scoreDocumentLayoutModel = null)
    {
        var keyGenerator = new RandomIntGeneratorFactory().CreateKeyGenerator();
        var instrumentMeasureFactory = new InstrumentMeasureFactory(keyGenerator);
        var contentTable = new ScoreContentTable(instrumentMeasureFactory, scoreDocumentStyleTemplate);

        var primaryLayout = new AuthorScoreDocumentLayout(scoreDocumentStyleTemplate);
        var secondaryLayout = new UserScoreDocumentLayout(scoreDocumentStyleTemplate, Guid.NewGuid());
        var scoreDocument = new ScoreDocumentCore(contentTable, scoreDocumentStyleTemplate, primaryLayout, secondaryLayout, keyGenerator, scoreDocumentModel.Id);
       
        if (scoreDocumentLayoutModel is not null)
        {
            var memento = scoreDocumentModel.Join(scoreDocumentLayoutModel);    
            scoreDocument.ApplyMemento(memento);
        }
        else
        {
            scoreDocument.ApplyModel(scoreDocumentModel);
        }

        return scoreDocument;
    }
    public static IScoreDocument Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var memento = new ScoreDocumentModel()
        {
            Id = Guid.NewGuid(),
            InstrumentRibbons = [],
            ScoreMeasures = [],
        };
        return Create(scoreDocumentStyleTemplate, memento, positionDictionaryBuilder);
    }
    public static IScoreDocument Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ScoreDocumentModel scoreDocumentModel, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var scoreDocument = CreateCore(scoreDocumentStyleTemplate, scoreDocumentModel);
        return scoreDocument.ProxyAuthor(positionDictionaryBuilder); 
    }
    public static IScoreDocument Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ScoreDocumentModel scoreDocumentModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutModel, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var scoreDocument = CreateCore(scoreDocumentStyleTemplate, scoreDocumentModel, scoreDocumentLayoutModel);
        return scoreDocument.ProxyUser(positionDictionaryBuilder);
    }

    public static IScoreDocument Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ScoreDocumentModel scoreDocumentModel, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var scoreDocument = CreateCore(scoreDocumentStyleTemplate, scoreDocumentModel);
        return scoreDocument.ProxyAuthor(commandManager, notifyEntityChanged, positionDictionaryBuilder);
    }
    public static IScoreDocument Create(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ScoreDocumentModel scoreDocumentModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutModel, IPositionDictionaryBuilder positionDictionaryBuilder)
    {
        var scoreDocument = CreateCore(scoreDocumentStyleTemplate, scoreDocumentModel, scoreDocumentLayoutModel);
        return scoreDocument.ProxyUser(commandManager, notifyEntityChanged, positionDictionaryBuilder);
    }
}
