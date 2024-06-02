using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models;
public class ScoreDocumentLayoutMetaDataModel
{
    public required Guid Id { get; set; }
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required string OwnerEmail { get; set; }
    public required bool IsPublic { get; set; }
}

public class ScoreDocumentLayoutModel : ScoreDocumentLayoutMembers
{
    public required Guid Id { get; set; }
}

public class ScoreMeasureLayoutModel : ScoreMeasureLayoutMembers
{
    public required Guid Id { get; set; }
}

public class InstrumentRibbonLayoutModel : InstrumentRibbonLayoutMembers
{
    public required Guid Id { get; set; }
}

public class InstrumentMeasureLayoutModel : InstrumentMeasureLayoutMembers
{
    public required Guid Id { get; set; }


    [Column(TypeName = "jsonb")]
    public required List<ClefChangeClass> IgnoredClefChanges { get; set; }
}

public class MeasureBlockLayoutModel : MeasureBlockLayoutMembers
{
    public required Guid Id { get; set; }
}

public class ChordLayoutModel : ChordLayoutMembers
{
    public required Guid Id { get; set; }
}

public class NoteLayoutModel : NoteLayoutMembers
{
    public required Guid Id { get; set; }
}