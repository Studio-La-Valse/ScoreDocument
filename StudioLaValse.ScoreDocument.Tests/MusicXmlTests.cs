using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.MusicXml;
using StudioLaValse.ScoreDocument.StyleTemplates;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.Tests;

[TestClass]
public class MusicXmlTests
{
    [TestMethod]
    public void TestChords()
    {
        using var stream = new FileStream("./Resources/Bach Sheep May Safely Graze.musicxml", FileMode.Open); 
        var xDocument = XDocument.Load(stream);
        var scoreDocumentModel = new ScoreDocumentModel() { Id = Guid.NewGuid(), InstrumentRibbons = [], ScoreMeasures = [] };

        var styleTemplate = ScoreDocumentStyleTemplate.Create();
        var commandManager = CommandManager.CommandManager.CreateGreedy();
        var notifyChanged = SceneManager<IUniqueScoreElement, int>.CreateObservable();
        var scoreDocument = Implementation.ScoreDocument.Create(styleTemplate, commandManager, notifyChanged, scoreDocumentModel);
        var scoreBuilder = Implementation.ScoreBuilder.Create(scoreDocument, commandManager, notifyChanged);
        scoreBuilder
            .Edit(editor =>
            {
                editor.BuildFromXml(xDocument);
            })
            .Build();


    }
}
