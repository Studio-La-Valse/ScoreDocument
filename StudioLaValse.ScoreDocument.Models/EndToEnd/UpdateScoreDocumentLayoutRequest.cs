namespace StudioLaValse.ScoreDocument.Models.EndToEnd;

public class UpdateScoreDocumentLayoutRequest
{
    public required ScoreDocumentLayoutDictionary LayoutDictionary { get; init; }
    public required ScoreDocumentLayoutMetaDataModel MetaData { get; init; }
}
