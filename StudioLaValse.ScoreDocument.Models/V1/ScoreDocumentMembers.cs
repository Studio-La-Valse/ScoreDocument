using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models.V1;
public class ScoreDocumentMembers : ScoreDocumentLayoutMembers
{

}

public class ScoreMeasureMembers : ScoreMeasureLayoutMembers
{
    public required TimeSignatureClass TimeSignature { get; set; }

    [Range(0, int.MaxValue)]
    public required int IndexInScore { get; set; }
}

public class InstrumentRibbonMembers : InstrumentRibbonLayoutMembers
{
    public required InstrumentClass Instrument { get; set; }

    [Range(0, int.MaxValue)]
    public required int IndexInScore { get; set; }
}

public class InstrumentMeasureMembers : InstrumentMeasureLayoutMembers
{

}

public class MeasureBlockMembers : MeasureBlockLayoutMembers
{
    [Range(0, int.MaxValue)]
    public required int Voice { get; set; }

    public required RythmicDurationClass RythmicDuration { get; set; }

    public required PositionClass Position { get; set; }
}

public class GraceGroupMembers : GraceGroupLayoutMembers
{

}

public class ChordMembers : ChordLayoutMembers
{
    public required RythmicDurationClass RythmicDuration { get; set; }

    public required PositionClass Position { get; set; }
}

public class GraceChordMembers : GraceChordLayoutMembers
{
    public required int IndexInGroup { get; set; }
}

public class NoteMembers : NoteLayoutMembers
{
    public required PitchClass Pitch { get; set; }
}

public class GraceNoteMembers : GraceNoteLayoutMembers
{
    public required PitchClass Pitch { get; set; }
}