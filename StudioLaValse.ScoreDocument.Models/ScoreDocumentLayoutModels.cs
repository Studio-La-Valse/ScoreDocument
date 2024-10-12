using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Models.Classes;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models;
public class ScoreDocumentLayoutMetaDataModel
{
    public required Guid Id { get; set; }
    public required Guid ScoreDocumentLayoutId { get; set; }
    public required string OwnerEmail { get; set; }
    public required bool IsPublic { get; set; }
}

public class ScoreDocumentLayoutDictionary 
{
    public required ScoreDocumentLayoutModel ScoreDocumentLayout { get; set; }
    public required ICollection<ScoreMeasureLayoutModel> ScoreMeasureLayouts { get; set; }
    public required ICollection<InstrumentRibbonLayoutModel> InstrumentRibbonLayouts { get; set; }
    public required ICollection<InstrumentMeasureLayoutModel> InstrumentMeasureLayouts { get; set; }
    public required ICollection<MeasureBlockLayoutModel> MeasureBlockLayouts { get; set; }
    public required ICollection<ChordLayoutModel> ChordLayouts { get; set; }
    public required ICollection<NoteLayoutModel> NoteLayouts { get; set; }
    public required ICollection<GraceGroupLayoutModel> GraceGroupLayouts { get; set; }
    public required ICollection<GraceChordLayoutModel> GraceChordLayouts { get; set; }
    public required ICollection<GraceNoteLayoutModel> GraceNoteLayouts { get; set; }

    public static ScoreDocumentLayoutDictionary Create(Guid scoreDocumentId)
    {
        return new ScoreDocumentLayoutDictionary()
        {
            ScoreDocumentLayout = ScoreDocumentLayoutModel.Create(scoreDocumentId),
            ChordLayouts = [],
            GraceChordLayouts = [],
            GraceGroupLayouts = [],
            GraceNoteLayouts = [],
            InstrumentMeasureLayouts = [],
            InstrumentRibbonLayouts = [],
            MeasureBlockLayouts = [],
            NoteLayouts = [],
            ScoreMeasureLayouts = [],
        };
    }
}

public class ScoreDocumentLayoutModel : ScoreDocumentLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid ScoreDocumentId { get; set; }

    public static ScoreDocumentLayoutModel Create(Guid scoreDocumentId)
    {
        return new ScoreDocumentLayoutModel()
        {
            Id = Guid.NewGuid(),
            ScoreDocumentId = scoreDocumentId
        };
    }
}

public class InstrumentRibbonLayoutModel : InstrumentRibbonLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid InstrumentRibbonId { get; set; }

    public static InstrumentRibbonLayoutModel Create(Guid instrumentRibbonid)
    {
        return new InstrumentRibbonLayoutModel()
        {
            Id = Guid.NewGuid(),
            InstrumentRibbonId = instrumentRibbonid,
            AbbreviatedName = null,
            Collapsed = null,
            DisplayName = null,
            NumberOfStaves = null,
            Scale = null,
        };
    }

    public bool HasFieldSet()
    {
        if (AbbreviatedName is not null)
        {
            return true;
        }

        if (DisplayName is not null)
        {
            return true;
        }

        if (NumberOfStaves.HasValue)
        {
            return true;
        }

        if (Collapsed.HasValue)
        {
            return true;
        }

        if (Scale.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class ScoreMeasureLayoutModel : ScoreMeasureLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid ScoreMeasureId { get; set; }
  
    public static ScoreMeasureLayoutModel Create(Guid scoreMeasureId)
    {
        return new ScoreMeasureLayoutModel()
        {
            Id = Guid.NewGuid(),
            ScoreMeasureId = scoreMeasureId,
            KeySignature = null,
            PaddingBottom = null,
        };
    }

    public bool HasFieldSet()
    {
        if (KeySignature is not null)
        {
            return true;
        }

        if (PaddingBottom.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class InstrumentMeasureLayoutModel : InstrumentMeasureLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid InstrumentMeasureId { get; set; }
    

    [Column(TypeName = "jsonb")]
    public required List<ClefChangeClass> IgnoredClefChanges { get; set; }


    public static InstrumentMeasureLayoutModel Create(Guid instrumentMeasureId)
    {
        return new InstrumentMeasureLayoutModel()
        {
            Id = Guid.NewGuid(),
            InstrumentMeasureId = instrumentMeasureId,
            ClefChanges = [],
            Collapsed = null,
            IgnoredClefChanges = [],
            NumberOfStaves = null,
            PaddingBottom = null,
            StaffPaddingBottom = []
        };
    }

    public bool HasFieldSet()
    {
        if (IgnoredClefChanges.Any())
        {
            return true;
        }

        if (ClefChanges.Any())
        {
            return true;
        }

        if (StaffPaddingBottom.Any())
        {
            return true;
        }

        if (Collapsed.HasValue)
        {
            return true;
        }

        if (NumberOfStaves.HasValue)
        {
            return true;
        }

        if (PaddingBottom.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class MeasureBlockLayoutModel : MeasureBlockLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid MeasureBlockId { get; set; }

    public static MeasureBlockLayoutModel Create(Guid measureBlockId)
    {
        return new MeasureBlockLayoutModel()
        {
            Id = Guid.NewGuid(),
            MeasureBlockId = measureBlockId,
            BeamAngle = null,
            StemDirection = null,
            StemLength = null,
            Scale = null,
        };
    }

    public bool HasFieldSet()
    {
        if (BeamAngle.HasValue)
        {
            return true;
        }

        if (StemLength.HasValue)
        {
            return true;
        }

        if (StemDirection.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class ChordLayoutModel : ChordLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid ChordId { get; set; }
    
    public static ChordLayoutModel Create(Guid chordId)
    {
        return new ChordLayoutModel()
        {
            Id = Guid.NewGuid(),
            ChordId = chordId,
            SpaceRight = null,
        };
    }

    public bool HasFieldSet()
    {
        if (SpaceRight.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class NoteLayoutModel : NoteLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid NoteId { get; set; }

    public static NoteLayoutModel Create(Guid noteId)
    {
        return new NoteLayoutModel()
        {
            Id = Guid.NewGuid(),
            NoteId = noteId,
            ForceAccidental = null,
            StaffIndex = null,
            Color = null,
        };
    }

    public bool HasFieldSet()
    {
        if (ForceAccidental.HasValue)
        {
            return true;
        }

        if (StaffIndex.HasValue)
        {
            return true;
        }

        if (ForceAccidental.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class GraceGroupLayoutModel : GraceGroupLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid GraceGroupId { get; set; }

    public static GraceGroupLayoutModel Create(Guid graceGroupId)
    {
        return new GraceGroupLayoutModel()
        {
            Id = Guid.NewGuid(),
            GraceGroupId = graceGroupId,
            BeamAngle = null,
            ChordDuration = null,
            ChordSpacing = null,
            OccupySpace = null,
            StemDirection = null,
            StemLength = null,
            Scale = null
        };
    }

    public bool HasFieldSet()
    {
        if (BeamAngle.HasValue)
        {
            return true;
        }

        if (StemLength.HasValue)
        {
            return true;
        }

        if (StemDirection.HasValue)
        {
            return true;
        }

        if (OccupySpace.HasValue)
        {
            return true;
        }

        if (ChordSpacing.HasValue)
        {
            return true;
        }

        if (ChordDuration is not null)
        {
            return true;
        }

        if (Scale.HasValue)
        {
            return true;
        }

        return false;
    }
}

public class GraceChordLayoutModel : GraceChordLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid GraceChordId { get; set; }
    
    public static GraceChordLayoutModel Create(Guid graceChordId)
    {
        return new GraceChordLayoutModel()
        {
            Id = Guid.NewGuid(),
            GraceChordId = graceChordId
        };
    }

    public bool HasFieldSet()
    {
        return false;
    }
}

public class GraceNoteLayoutModel : GraceNoteLayoutMembers
{
    public required Guid Id { get; set; }
    public required Guid GraceNoteId { get; set; }

    public static GraceNoteLayoutModel Create(Guid graceNoteId)
    {
        return new GraceNoteLayoutModel()
        {
            Id = Guid.NewGuid(),
            GraceNoteId = graceNoteId,
            ForceAccidental = null,
            StaffIndex = null,
            Color = null,
        };
    }

    public bool HasFieldSet()
    {
        if (ForceAccidental.HasValue)
        {
            return true;
        }

        if (StaffIndex.HasValue)
        {
            return true;
        }

        if (Color is not null)
        {
            return true;
        }

        return false;
    }
}