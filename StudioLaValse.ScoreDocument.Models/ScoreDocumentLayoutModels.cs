using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models;

public class ScoreDocumentLayoutModel : ScoreDocumentLayoutMembers
{

}

public class ScoreMeasureLayoutModel : ScoreMeasureLayoutMembers
{

}

public class InstrumentRibbonLayoutModel : InstrumentRibbonLayoutMembers
{

}

public class InstrumentMeasureLayoutModel : InstrumentMeasureLayoutMembers
{
    [Column(TypeName = "jsonb")]
    public required List<ClefChange>? IgnoredClefChanges { get; set; }
}

public class MeasureBlockLayoutModel : MeasureBlockLayoutMembers
{

}

public class ChordLayoutModel : ChordLayoutMembers
{

}

public class NoteLayoutModel : NoteLayoutMembers
{

}