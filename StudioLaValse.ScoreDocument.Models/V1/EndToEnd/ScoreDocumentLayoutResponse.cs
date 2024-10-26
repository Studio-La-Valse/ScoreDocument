using StudioLaValse.ScoreDocument.Models.V1;

namespace StudioLaValse.ScoreDocument.Models.V1.EndToEnd;

public class ScoreDocumentLayoutResponse
{
    public required ScoreDocumentLayoutModel ScoreDocumentLayout { get; init; }
    public required ScoreDocumentLayoutMetaDataModel MetaData { get; init; }
}
