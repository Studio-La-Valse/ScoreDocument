using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models.Base;
public class ScoreDocumentMembers : ScoreDocumentLayoutMembers
{

}

public class ScoreMeasureMembers : ScoreMeasureLayoutMembers
{
    [Column(TypeName = "jsonb")]
    public required TimeSignatureClass TimeSignature { get; set; }

    [Range(0, int.MaxValue)]
    public required int IndexInScore { get; set; }
}

public class InstrumentRibbonMembers : InstrumentRibbonLayoutMembers
{
    [Column(TypeName = "jsonb")]
    public required InstrumentClass Instrument { get; set; }

    [Range(0, int.MaxValue)]
    public required int IndexInScore { get; set; }
}

public class InstrumentMeasureMembers : InstrumentMeasureLayoutMembers
{
    [Range(0, int.MaxValue)]
    public required int ScoreMeasureIndex { get; set; }

    [Range(0, int.MaxValue)]
    public required int InstrumentRibbonIndex { get; set; }
}

public class MeasureBlockMembers : MeasureBlockLayoutMembers
{
    [Range(0, int.MaxValue)]
    public required int Voice { get; set; }

    [Column(TypeName = "jsonb")]
    public required RythmicDurationClass RythmicDuration { get; set; }

    [Column(TypeName = "jsonb")]
    public required PositionClass Position { get; set; }
}

public class ChordMembers : ChordLayoutMembers
{
    [Column(TypeName = "jsonb")]
    public required RythmicDurationClass RythmicDuration { get; set; }

    [Column(TypeName = "jsonb")]
    public required PositionClass Position { get; set; }
}

public class NoteMembers : NoteLayoutMembers
{
    [Column(TypeName = "jsonb")]
    public required PitchClass Pitch { get; set; }
}