using StudioLaValse.ScoreDocument.Models.V1;

namespace StudioLaValse.ScoreDocument.Models.V1.EndToEnd;

public class UpdateScoreDocumentRequest
{
    public required ScoreDocumentModel ScoreDocument { get; init; }
    public required ScoreDocumentMetaDataModel MetaData { get; init; }
}
