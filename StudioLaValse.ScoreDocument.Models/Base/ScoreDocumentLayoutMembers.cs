using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudioLaValse.ScoreDocument.Models.Base;

public class ScoreDocumentLayoutMembers
{   
    [Range(0, double.MaxValue)]
    public required double? FirstSystemIndent { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? HorizontalStaffLineThickness { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? VerticalStaffLineThickness { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? Scale { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? StemLineThickness { get; set; }

    [Range(0, 1)]
    public required double? ChordPositionFactor { get; set; }

    [Column(TypeName = "jsonb")]
    public required ColorARGBClass? PageColor { get; set; }

    [Column(TypeName = "jsonb")]
    public required ColorARGBClass? ForegroundColor { get; set; }
}

public class ScoreMeasureLayoutMembers
{   
    [Range(0, double.MaxValue)]
    public required double? PaddingRight { get; set; }

    [Range(0, double.MaxValue)]
    public required double? PaddingLeft { get; set; }

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

    public required StemDirection? StemDirection { get; set; }
}

public class GraceGroupLayoutMembers : MeasureBlockLayoutMembers
{
    public required bool? OccupySpace { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? ChordSpacing { get; set; }

    public required RythmicDurationClass? ChordDuration { get; set; }
}

public class ChordLayoutMembers
{    
    public required double? XOffset { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? SpaceRight { get; set; }
}

public class GraceChordLayoutMembers
{

}

public class NoteLayoutMembers
{    
    public required int? ForceAccidental { get; set; }

    [Range(0, int.MaxValue)]
    public required int? StaffIndex { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? Scale { get; set; }

    public required double? XOffset { get; set; }
}

public class GraceNoteLayoutMembers
{
    public required int? ForceAccidental { get; set; }

    [Range(0, int.MaxValue)]
    public required int? StaffIndex { get; set; }
}
