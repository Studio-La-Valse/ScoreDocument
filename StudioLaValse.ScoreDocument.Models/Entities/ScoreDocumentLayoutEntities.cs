using StudioLaValse.ScoreDocument.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudioLaValse.ScoreDocument.Models.Entities;

public class ScoreDocumentLayoutEntity : ScoreDocumentLayoutMembers
{
    public required Guid ScoreDocumentId { get; set; }


    [ForeignKey(nameof(ScoreMeasureLayoutEntity.ScoreDocumentLayoutId))]
    public required List<ScoreMeasureLayoutEntity> ScoreMeasureLayouts { get; set; }

    [ForeignKey(nameof(InstrumentRibbonLayoutEntity.ScoreDocumentLayoutId))]
    public required List<InstrumentRibbonLayoutEntity> InstrumentRibbonLayouts { get; set; }

    [ForeignKey(nameof(InstrumentMeasureLayoutEntity.ScoreDocumentLayoutId))]
    public required List<InstrumentMeasureLayoutEntity> InstrumentMeasureLayouts { get; set; }

    [ForeignKey(nameof(MeasureBlockLayoutEntity.ScoreDocumentLayoutId))]
    public required List<MeasureBlockLayoutEntity> MeasureBlockLayouts { get; set; }

    [ForeignKey(nameof(ChordLayoutEntity.ScoreDocumentLayoutId))]
    public required List<ChordLayoutEntity> ChordLayouts { get; set; }

    [ForeignKey(nameof(NoteLayoutEntity.ScoreDocumentLayoutId))]
    public required List<NoteLayoutEntity> NoteLayouts { get; set; }
}

public class ScoreMeasureLayoutEntity : ScoreMeasureLayoutMembers
{
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required Guid ScoreMeasureId { get; set; }
}

public class InstrumentRibbonLayoutEntity : InstrumentRibbonLayoutMembers
{
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required Guid InstrumentRibbonId { get; set; }
}

public class InstrumentMeasureLayoutEntity : InstrumentMeasureLayoutMembers
{
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required Guid InstrumentMeasureId { get; set; }
}

public class MeasureBlockLayoutEntity : MeasureBlockLayoutMembers
{
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required Guid MeasureBlockId { get; set; }
}

public class ChordLayoutEntity : ChordLayoutMembers
{
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required Guid ChordId { get; set; }
}

public class NoteLayoutEntity : NoteLayoutMembers
{
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required Guid NoteId { get; set; }
}
