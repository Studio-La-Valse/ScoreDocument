using StudioLaValse.ScoreDocument.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models.Entities;


public class ScoreDocumentEntity : ScoreDocumentMembers
{
    [ForeignKey(nameof(ScoreMeasureEntity.ScoreDocumentId))]
    public required List<ScoreMeasureEntity> ScoreMeasures { get; set; }

    [ForeignKey(nameof(InstrumentRibbonEntity.ScoreDocumentId))]
    public required List<InstrumentRibbonEntity> InstrumentRibbons { get; set; }

    [ForeignKey(nameof(ScoreDocumentLayoutEntity.ScoreDocumentId))]
    public required List<ScoreDocumentLayoutEntity> Layouts { get; set; }
}

public class ScoreMeasureEntity : ScoreMeasureMembers
{
    public required Guid ScoreDocumentId { get; set; }



    [ForeignKey(nameof(InstrumentMeasureEntity.ScoreMeasureId))]
    public required List<InstrumentMeasureEntity> InstrumentMeasures { get; set; }

    [ForeignKey(nameof(ScoreMeasureLayoutEntity.ScoreMeasureId))]
    public required List<ScoreMeasureLayoutEntity> Layouts { get; set; }
}

public class InstrumentRibbonEntity : InstrumentRibbonMembers
{
    public required Guid ScoreDocumentId { get; set; }



    [ForeignKey(nameof(InstrumentRibbonLayoutEntity.InstrumentRibbonId))]
    public required List<InstrumentRibbonLayoutEntity> Layouts { get; set; }
}

public class InstrumentMeasureEntity : InstrumentMeasureMembers
{
    public required Guid ScoreMeasureId { get; set; }



    [ForeignKey(nameof(MeasureBlockEntity.InstrumentMeasureId))]
    public required List<MeasureBlockEntity> MeasureBlocks { get; set; }

    [ForeignKey(nameof(InstrumentMeasureLayoutEntity.InstrumentMeasureId))]
    public required List<InstrumentMeasureLayoutEntity> Layouts { get; set; }
}

public class MeasureBlockEntity : MeasureBlockMembers
{
    public required Guid InstrumentMeasureId { get; set; }



    [ForeignKey(nameof(ChordEntity.MeasureBlockId))]
    public required List<ChordEntity> Chords { get; set; }

    [ForeignKey(nameof(MeasureBlockLayoutEntity.MeasureBlockId))]
    public required List<MeasureBlockLayoutEntity> Layouts { get; set; }
}

public class ChordEntity : ChordMembers
{
    public required Guid MeasureBlockId { get; set; }



    [ForeignKey(nameof(NoteEntity.ChordId))]
    public required List<NoteEntity> Notes { get; set; }

    [ForeignKey(nameof(ChordLayoutEntity.ChordId))]
    public required List<ChordLayoutEntity> Layouts { get; set; }
}

public class NoteEntity : NoteMembers
{
    public required Guid ChordId { get; set; }



    [ForeignKey(nameof(NoteLayoutEntity.NoteId))]
    public required List<NoteLayoutEntity> Layouts { get; set; }
}
