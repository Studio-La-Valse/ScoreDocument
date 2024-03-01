using StudioLaValse.ScoreDocument.MusicXml.Private;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml;

/// <summary>
/// 
/// </summary>
public static class ScoreEditorExtensions
{

    /// <summary>
    /// 
    /// </summary>
    public static IScoreBuilder BuildFromXml(this IScoreBuilder builder, XDocument document)
    {
        return builder.EditFromXml(document).Build();
    }


    /// <summary>
    /// 
    /// </summary>
    public static IScoreBuilder EditFromXml(this IScoreBuilder builder, XDocument document)
    {
        return builder
            .Edit(e =>
            {
                e.Clear();
            })
            .Edit((e, l) =>
            {
                var blockConverter = new BlockChainXmlConverter();
                var measureConverter = new ScorePartMeasureXmlConverter(blockConverter);
                var partConverter = new ScorePartXmlConverter(measureConverter);
                new ScoreDocumentXmlConverter(partConverter).Create(document, e, l);
            });
    }
}
