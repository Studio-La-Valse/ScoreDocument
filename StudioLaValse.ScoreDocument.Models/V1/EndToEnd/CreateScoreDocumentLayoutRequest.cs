using StudioLaValse.ScoreDocument.Models.V1;

namespace StudioLaValse.ScoreDocument.Models.V1.EndToEnd;

public class CreateScoreDocumentLayoutRequest
{
    public required ScoreDocumentLayoutDictionary ScoreDocumentLayout { get; init; }
    public required ScoreDocumentLayoutMetaDataModel MetaData { get; init; }
}