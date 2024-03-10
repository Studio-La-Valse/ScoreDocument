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
            .Edit(editor =>
            {
                editor.Clear();
            })
            .Edit(editor =>
            {
                BlockChainXmlConverter blockConverter = new();
                ScorePartMeasureXmlConverter measureConverter = new(blockConverter);
                ScorePartXmlConverter partConverter = new(measureConverter);
                new ScoreDocumentXmlConverter(partConverter).Create(document, editor);
            });
    }
}
