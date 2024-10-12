using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudioLaValse.ScoreDocument.Models.Base;

public class ScoreDocumentLayoutMembers
{   

}

public class ScoreMeasureLayoutMembers
{   
    [Range(0, double.MaxValue)]
    public required double? PaddingBottom { get; set; }

    [Column(TypeName = "jsonb")]
    public required KeySignatureClass? KeySignature { get; set; }
}

public class InstrumentRibbonLayoutMembers
{   
    public required string? AbbreviatedName { get; set; }

    public required string? DisplayName { get; set; }

    [Range(1, int.MaxValue)]
    public required int? NumberOfStaves { get; set; }

    public required bool? Collapsed { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? Scale { get; set; }
}

public class InstrumentMeasureLayoutMembers
{    
    [Column(TypeName = "jsonb")]
    public required List<ClefChangeClass> ClefChanges { get; set; }

    [Column(TypeName = "jsonb")]
    public required Dictionary<int, double> StaffPaddingBottom { get; set; }

    public required bool? Collapsed { get; set; }

    [Range(1, int.MaxValue)]
    public required int? NumberOfStaves { get; set; }

    [Range(0, int.MaxValue)]
    public required double? PaddingBottom { get; set; }
}

public class MeasureBlockLayoutMembers
{    
    public required double? BeamAngle { get; set; }

    public required double? StemLength { get; set; }

    public required int? StemDirection { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? Scale { get; set; }
}

public class ChordLayoutMembers
{    
    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? SpaceRight { get; set; }
}

public class RestLayoutMembers
{
    [Range(0, int.MaxValue)]
    public required int? StaffIndex { get; set; }

    public required int? Line { get; set; }

    public required ColorARGBClass? Color { get; set; }
}

public class NoteLayoutMembers
{    
    public required int? ForceAccidental { get; set; }

    [Range(0, int.MaxValue)]
    public required int? StaffIndex { get; set; }

    public required ColorARGBClass? Color { get; set; }
}

public class GraceGroupLayoutMembers : MeasureBlockLayoutMembers
{
    public required bool? OccupySpace { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? ChordSpacing { get; set; }

    [Column(TypeName = "jsonb")]
    public required RythmicDurationClass? ChordDuration { get; set; }
}

public class GraceChordLayoutMembers
{

}

public class GraceNoteLayoutMembers
{
    public required int? ForceAccidental { get; set; }

    [Range(0, int.MaxValue)]
    public required int? StaffIndex { get; set; }

    public required ColorARGBClass? Color { get; set; }
}
