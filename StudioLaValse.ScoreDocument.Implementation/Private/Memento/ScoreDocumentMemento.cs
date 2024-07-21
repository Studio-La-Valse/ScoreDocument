using StudioLaValse.ScoreDocument.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Memento;


internal class ScoreDocumentMemento : ScoreDocumentMembers
{
    public required Guid Id { get; set; }

    public required List<ScoreMeasureMemento> ScoreMeasures { get; set; }

    public required List<InstrumentRibbonMemento> InstrumentRibbons { get; set; }

    public required ScoreDocumentLayoutModel Layout { get; init; }
}

internal class ScoreMeasureMemento : ScoreMeasureMembers
{
    public required Guid Id { get; set; }

    public required List<InstrumentMeasureMemento> InstrumentMeasures { get; set; }

    public required ScoreMeasureLayoutModel Layout { get; init; }
}

internal class InstrumentRibbonMemento : InstrumentRibbonMembers
{
    public required Guid Id { get; set; }

    public required InstrumentRibbonLayoutModel Layout { get; init; }
}

internal class InstrumentMeasureMemento : InstrumentMeasureMembers
{
    public required Guid Id { get; set; }

    public required List<MeasureBlockMemento> MeasureBlocks { get; set; }

    public required InstrumentMeasureLayoutModel Layout { get; init; }

    [Range(0, int.MaxValue)]
    public required int ScoreMeasureIndex { get; set; }

    [Range(0, int.MaxValue)]
    public required int InstrumentRibbonIndex { get; set; }
}

internal class MeasureBlockMemento : MeasureBlockMembers
{
    public required Guid Id { get; set; }

    public required List<ChordMemento> Chords { get; set; }

    public required MeasureBlockLayoutModel Layout { get; init; }
}

internal class GraceGroupMemento : GraceGroupMembers
{
    public required Guid Id { get; set; }

    public required List<GraceChordMemento> Chords { get; set; }

    public required GraceGroupLayoutModel Layout { get; init; }
}

internal class ChordMemento : ChordMembers
{
    public required Guid Id { get; set; }

    public required List<NoteMemento> Notes { get; set; }

    public required GraceGroupMemento? GraceGroup { get; set; }

    public required ChordLayoutModel Layout { get; init; }
}

internal class GraceChordMemento : GraceChordMembers
{
    public required Guid Id { get; set; }

    public required List<GraceNoteMemento> Notes { get; set; }

    public required GraceChordLayoutModel Layout { get; init; }
}

internal class NoteMemento : NoteMembers
{
    public required Guid Id { get; set; }

    public required NoteLayoutModel Layout { get; init; }
}

internal class GraceNoteMemento : GraceNoteMembers
{
    public required Guid Id { get; set; }

    public required GraceNoteLayoutModel Layout { get; init; }
}

