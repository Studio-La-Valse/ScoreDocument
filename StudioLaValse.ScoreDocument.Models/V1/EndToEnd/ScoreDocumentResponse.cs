using StudioLaValse.ScoreDocument.Models.V1;

namespace StudioLaValse.ScoreDocument.Models.V1.EndToEnd;

public class ScoreDocumentResponse
{
    public required ScoreDocumentModel ScoreDocument { get; init; }
    public required ScoreDocumentMetaDataModel MetaData { get; init; }
}
