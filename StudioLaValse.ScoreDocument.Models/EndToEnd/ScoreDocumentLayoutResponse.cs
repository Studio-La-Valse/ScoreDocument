namespace StudioLaValse.ScoreDocument.Models.EndToEnd;

public class ScoreDocumentLayoutResponse
{
    public required ScoreDocumentLayoutModel ScoreDocumentLayout { get; init; }
    public required ScoreDocumentLayoutMetaDataModel MetaData { get; init; }
}
