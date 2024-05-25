using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models;

public class ScoreDocumentStyleTemplateModel
{
    public required Guid Id { get; set; }

    public required PageStyleTemplateModel PageStyleTemplate { get; init; }

    public required ScoreMeasureStyleTemplateModel ScoreMeasureStyleTemplate { get; init; }

    public required InstrumentMeasureStyleTemplateModel InstrumentMeasureStyleTemplate { get; init; }

    public required InstrumentRibbonStyleTemplateModel InstrumentRibbonStyleTemplate { get; init; }

    public required ChordStyleTemplateModel ChordStyleTemplate { get; init; }

    public required NoteStyleTemplateModel NoteStyleTemplate { get; init; }

    public required MeasureBlockStyleTemplateModel MeasureBlockStyleTemplate { get; init; }

    public required StaffStyleTemplateModel StaffStyleTemplate { get; init; }

    public required StaffGroupStyleTemplateModel StaffGroupStyleTemplate { get; init; }

    public required StaffSystemStyleTemplateModel StaffSystemStyleTemplate { get; init; }



    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double Scale { get; init; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double HorizontalStaffLineThickness { get; init; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double VerticalStaffLineThickness { get; init; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double StemLineThickness { get; init; }

    [Range(0, double.MaxValue)]
    public required double FirstSystemIndent { get; init; }

    [Column(TypeName = "jsonb")]
    public required ColorARGB PageColor { get; init; }

    [Column(TypeName = "jsonb")]
    public required ColorARGB ForegroundColor { get; init; }

    [Column(TypeName = "jsonb")]
    public required Dictionary<Guid, double> InstrumentScales { get; init; }
}

public class StaffSystemStyleTemplateModel
{
    public required Guid Id { get; init; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }


    [Range(0, double.MaxValue)]
    public required double PaddingBottom { get; init; }
}

public class StaffGroupStyleTemplateModel
{
    public required Guid Id { get; init; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }


    [Range(0, double.MaxValue)]
    public required double DistanceToNext { get; init; }
}

public class StaffStyleTemplateModel
{
    public required Guid Id { get; init; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }


    [Range(0, double.MaxValue)]
    public required double DistanceToNext { get; init; }
}

public class MeasureBlockStyleTemplateModel
{
    public required Guid Id { get; init; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }


    public required double StemLength { get; init; }
    public required double BracketAngle { get; init; }
}

public class NoteStyleTemplateModel
{
    public required Guid Id { get; init; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }



    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double Scale { get; init; }
    public required int AccidentalDisplay { get; init; }
}

public class ChordStyleTemplateModel
{
    public required Guid Id { get; init; }



    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }
}

public class InstrumentRibbonStyleTemplateModel
{
    public required Guid Id { get; init; }



    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; init; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; init; }
}

public class InstrumentMeasureStyleTemplateModel
{
    public required Guid Id { get; set; }



    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; set; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }
}

public class ScoreMeasureStyleTemplateModel
{
    public required Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; set; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }



    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public required double Width { get; set; }

    [Range(0, double.MaxValue)]
    public required double PaddingLeft { get; set; }

    [Range(0, double.MaxValue)]
    public required double PaddingRight { get; set; }
}

public class PageStyleTemplateModel
{
    public required Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public required Guid ScoreDocumentStyleTemplateId { get; set; }
    public required ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }



    [Range(1, int.MaxValue)]
    public required int PageHeight { get; set; }

    [Range(1, int.MaxValue)]
    public required int PageWidth { get; set; }

    [Range(0, double.MaxValue)]
    public required double MarginTop { get; set; }

    [Range(0, double.MaxValue)]
    public required double MarginRight { get; set; }

    [Range(0, double.MaxValue)]
    public required double MarginLeft { get; set; }

    [Range(0, double.MaxValue)]
    public required double MarginBottom { get; set; }
}