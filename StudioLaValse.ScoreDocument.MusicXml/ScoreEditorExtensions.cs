using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.MusicXml.Private;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml;

/// <summary>
/// Extensions to the score document editor.
/// </summary>
public static class ScoreEditorExtensions
{
    /// <summary>
    /// Generate score document content from the provided x document. Layout will be applied to the provided layout document.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="document"></param>
    /// <returns></returns>
    public static IScoreDocument BuildFromXml(this IScoreDocument builder, XDocument document)
    {
        return builder.EditFromXml(document);
    }


    /// <summary>
    /// Generate score document content from the provided x document. Layout will be applied to the provided layout document.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="document"></param>
    /// <returns></returns>
    public static IScoreDocument EditFromXml(this IScoreDocument builder, XDocument document)
    {
        builder.Clear();

        BlockChainXmlConverter blockConverter = new();
        ScorePartMeasureXmlConverter measureConverter = new(blockConverter);
        ScorePartXmlConverter partConverter = new(measureConverter);
        new ScoreDocumentXmlConverter(partConverter).Create(document, builder);

        return builder;
    }
}
