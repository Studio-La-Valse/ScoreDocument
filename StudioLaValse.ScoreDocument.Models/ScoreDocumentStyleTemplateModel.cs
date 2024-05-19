﻿using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models;

public class ScoreDocumentStyleTemplateModel
{
    public Guid Id { get; set; }

    public PageStyleTemplateModel PageStyleTemplate { get; set; }

    public ScoreMeasureStyleTemplateModel ScoreMeasureStyleTemplate { get; set; }

    public InstrumentMeasureStyleTemplateModel InstrumentMeasureStyleTemplate { get; set; }

    public InstrumentRibbonStyleTemplateModel InstrumentRibbonStyleTemplate { get; set; }

    public ChordStyleTemplateModel ChordStyleTemplate { get; set; }

    public NoteStyleTemplateModel NoteStyleTemplate { get; set; }

    public MeasureBlockStyleTemplateModel MeasureBlockStyleTemplate { get; set; }

    public StaffStyleTemplateModel StaffStyleTemplate { get; set; }

    public StaffGroupStyleTemplateModel StaffGroupStyleTemplate { get; set; }

    public StaffSystemStyleTemplateModel StaffSystemStyleTemplate { get; set; }



    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public double Scale { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public double HorizontalStaffLineThickness { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public double VerticalStaffLineThickness { get; set; }

    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public double StemLineThickness { get; set; }

    [Range(0, double.MaxValue)]
    public double FirstSystemIndent { get; set; }

    [Column(TypeName = "jsonb")]
    public ColorARGB PageColor { get; set; }

    [Column(TypeName = "jsonb")]
    public ColorARGB ForegroundColor { get; set; }

    [Column(TypeName = "jsonb")]
    public Dictionary<Guid, double> InstrumentScales { get; set; }
}

public class StaffSystemStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }


    [Range(0, double.MaxValue)]
    public double PaddingBottom { get; set; }
}

public class StaffGroupStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }


    [Range(0, double.MaxValue)]
    public double DistanceToNext { get; set; }
}

public class StaffStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }


    [Range(0, double.MaxValue)]
    public double DistanceToNext { get; set; }
}

public class MeasureBlockStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }


    public double StemLength { get; set; }
    public double BracketAngle { get; set; }
}

public class NoteStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }



    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public double Scale { get; set; }
    public int AccidentalDisplay { get; set; }
}

public class ChordStyleTemplateModel
{
    public Guid Id { get; set; }



    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }
}

public class InstrumentRibbonStyleTemplateModel
{
    public Guid Id { get; set; }



    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }
}

public class InstrumentMeasureStyleTemplateModel
{
    public Guid Id { get; set; }



    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }
}

public class ScoreMeasureStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }



    [Range(Constants.GreaterThanZero, double.MaxValue)]
    public double Width { get; set; }

    [Range(0, double.MaxValue)]
    public double PaddingLeft { get; set; }

    [Range(0, double.MaxValue)]
    public double PaddingRight { get; set; }
}

public class PageStyleTemplateModel
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(ScoreDocumentStyleTemplate))]
    public Guid ScoreDocumentStyleTemplateId { get; set; }
    public ScoreDocumentStyleTemplateModel ScoreDocumentStyleTemplate { get; set; }



    [Range(1, int.MaxValue)]
    public int PageHeight { get; set; }

    [Range(1, int.MaxValue)]
    public int PageWidth { get; set; }

    [Range(0, double.MaxValue)]
    public double MarginTop { get; set; }

    [Range(0, double.MaxValue)]
    public double MarginRight { get; set; }

    [Range(0, double.MaxValue)]
    public double MarginLeft { get; set; }

    [Range(0, double.MaxValue)]
    public double MarginBottom { get; set; }
}