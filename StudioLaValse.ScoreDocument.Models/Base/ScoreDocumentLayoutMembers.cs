using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudioLaValse.ScoreDocument.Models.Base;

public class ScoreDocumentLayoutMembers
{
    public required Guid Id { get; set; }
    
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

    [Column(TypeName = "jsonb")]
    public required ColorARGB? PageColor { get; set; }

    [Column(TypeName = "jsonb")]
    public required ColorARGB? ForegroundColor { get; set; }

    [Column(TypeName = "jsonb")]
    public required Dictionary<Guid, double>? InstrumentScales { get; set; }
}

public class ScoreMeasureLayoutMembers
{
    public required Guid Id { get; set; }
    
    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? Width { get; set; }

    [Range(0, double.MaxValue)]
    public required double? PaddingRight { get; set; }

    [Range(0, double.MaxValue)]
    public required double? PaddingLeft { get; set; }

    [Range(0, double.MaxValue)]
    public required double? PaddingBottom { get; set; }

    [Column(TypeName = "jsonb")]
    public required KeySignature? KeySignature { get; set; }
}

public class InstrumentRibbonLayoutMembers
{
    public required Guid Id { get; set; }
    
    public required string? AbbreviatedName { get; set; }

    public required string? DisplayName { get; set; }

    [Range(1, int.MaxValue)]
    public required int? NumberOfStaves { get; set; }

    public required bool? Collapsed { get; set; }
}

public class InstrumentMeasureLayoutMembers
{
    public required Guid Id { get; set; }
    
    [Column(TypeName = "jsonb")]
    public required List<ClefChange>? ClefChanges { get; set; }

    [Column(TypeName = "jsonb")]
    public required List<ClefChange>? IgnoredClefChanges { get; set; }

    public required bool? Collapsed { get; set; }

    [Range(1, int.MaxValue)]
    public required int? NumberOfStaves { get; set; }

    [Range(0, int.MaxValue)]
    public required double? PaddingBottom { get; set; }

    [Column(TypeName = "jsonb")]
    public required Dictionary<int, double>? StaffPaddingBottom { get; set; }
}

public class MeasureBlockLayoutMembers
{
    public required Guid Id { get; set; }
    
    public required double? BeamAngle { get; set; }

    public required double? StemLength { get; set; }
}

public class ChordLayoutMembers
{
    public required Guid Id { get; set; }
    
    public required double? XOffset { get; set; }
}

public class NoteLayoutMembers
{
    public required Guid Id { get; set; }
    
    public required int? ForceAccidental { get; set; }

    [Range(0, int.MaxValue)]
    public required int? StaffIndex { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double? Scale { get; set; }

    public required double? XOffset { get; set; }
}
