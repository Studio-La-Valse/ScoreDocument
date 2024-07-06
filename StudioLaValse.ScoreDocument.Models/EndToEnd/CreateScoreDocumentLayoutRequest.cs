namespace StudioLaValse.ScoreDocument.Models.EndToEnd;

public class CreateScoreDocumentLayoutRequest
{
    public required ScoreDocumentLayoutDictionary ScoreDocumentLayout { get; init; }
    public required ScoreDocumentLayoutMetaDataModel MetaData { get; init; }
}