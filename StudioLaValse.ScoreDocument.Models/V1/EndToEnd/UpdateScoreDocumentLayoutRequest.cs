using StudioLaValse.ScoreDocument.Models.V1;

namespace StudioLaValse.ScoreDocument.Models.V1.EndToEnd;

public class UpdateScoreDocumentLayoutRequest
{
    public required ScoreDocumentLayoutDictionary LayoutDictionary { get; init; }
    public required ScoreDocumentLayoutMetaDataModel MetaData { get; init; }
}
