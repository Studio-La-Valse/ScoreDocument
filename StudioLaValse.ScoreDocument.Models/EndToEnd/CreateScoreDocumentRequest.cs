using StudioLaValse.ScoreDocument.Models;

namespace StudioLaValse.ScoreDocument.Models.EndToEnd;

public class CreateScoreDocumentRequest
{
    public required ScoreDocumentModel ScoreDocument { get; init; }
    public required ScoreDocumentMetaDataModel MetaData { get; init; }
}
