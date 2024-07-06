using StudioLaValse.ScoreDocument.Implementation.Private.Extensions;
using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Memento;

internal static class ScoreDocumentModelExtensions
{
    public static ScoreDocumentMemento Join(this ScoreDocumentModel scoreDocumentModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new ScoreDocumentMemento()
        {
            Id = scoreDocumentModel.Id,
            Layout = scoreDocumentLayoutDictionary.ScoreDocumentLayout,
            InstrumentRibbons = [],
            ScoreMeasures = []
        };

        foreach(var instrumentRibbon in scoreDocumentModel.InstrumentRibbons)
        {
            var layout = scoreDocumentLayoutDictionary.InstrumentRibbonLayouts.FirstOrDefault(l => l.InstrumentRibbonId == instrumentRibbon.Id);
            layout ??= InstrumentRibbonLayoutModel.Create(instrumentRibbon.Id);

            memento.InstrumentRibbons.Add(instrumentRibbon.Join(layout));
        }

        foreach(var scoreMeasure in scoreDocumentModel.ScoreMeasures)
        {
            var scoreMeasureLayout = scoreDocumentLayoutDictionary.ScoreMeasureLayouts.FirstOrDefault(l => l.ScoreMeasureId == scoreMeasure.Id);
            scoreMeasureLayout ??= ScoreMeasureLayoutModel.Create(scoreMeasure.Id);

            memento.ScoreMeasures.Add(scoreMeasure.Join(scoreMeasureLayout, scoreDocumentLayoutDictionary));
        }

        return memento;
    }

    public static InstrumentRibbonMemento Join(this InstrumentRibbonModel model, InstrumentRibbonLayoutModel layoutModel)
    {
        return new InstrumentRibbonMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            AbbreviatedName = model.AbbreviatedName,
            Collapsed = model.Collapsed,
            DisplayName = model.DisplayName,
            IndexInScore = model.IndexInScore,
            Instrument = model.Instrument,
            NumberOfStaves = model.NumberOfStaves,
            Scale = model.Scale
        };
    }

    public static ScoreMeasureMemento Join(this ScoreMeasureModel model, ScoreMeasureLayoutModel layoutModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new ScoreMeasureMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            IndexInScore = model.IndexInScore,
            KeySignature = model.KeySignature,
            TimeSignature = model.TimeSignature,
            PaddingBottom = model.PaddingBottom,
            InstrumentMeasures = []
        };

        foreach(var instrumentMeasure in model.InstrumentMeasures)
        {
            var instrumentMeasureLayout = scoreDocumentLayoutDictionary.InstrumentMeasureLayouts.FirstOrDefault(l => l.InstrumentMeasureId == instrumentMeasure.Id);
            instrumentMeasureLayout ??= InstrumentMeasureLayoutModel.Create(instrumentMeasure.Id);

            memento.InstrumentMeasures.Add(instrumentMeasure.Join(instrumentMeasureLayout, scoreDocumentLayoutDictionary)); 
        }

        return memento;
    }

    public static InstrumentMeasureMemento Join(this InstrumentMeasureModel model, InstrumentMeasureLayoutModel layoutModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new InstrumentMeasureMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            PaddingBottom = model.PaddingBottom,
            ClefChanges = model.ClefChanges,
            Collapsed = model.Collapsed,
            InstrumentRibbonIndex = model.InstrumentRibbonIndex,
            NumberOfStaves = model.NumberOfStaves,
            ScoreMeasureIndex = model.ScoreMeasureIndex,
            StaffPaddingBottom = model.StaffPaddingBottom,
            MeasureBlocks = []
        };

        foreach (var measureBlock in model.MeasureBlocks)
        {
            var instrumentMeasureLayout = scoreDocumentLayoutDictionary.MeasureBlockLayouts.FirstOrDefault(l => l.MeasureBlockId == measureBlock.Id);
            instrumentMeasureLayout ??= MeasureBlockLayoutModel.Create(measureBlock.Id);

            memento.MeasureBlocks.Add(measureBlock.Join(instrumentMeasureLayout, scoreDocumentLayoutDictionary));
        }

        return memento;
    }

    public static MeasureBlockMemento Join(this MeasureBlockModel model, MeasureBlockLayoutModel layoutModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new MeasureBlockMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            BeamAngle = model.BeamAngle,
            Position = model.Position,
            RythmicDuration = model.RythmicDuration,
            StemDirection = model.StemDirection,
            StemLength = model.StemLength,
            Voice = model.Voice,
            Chords = []
        };

        foreach (var chord in model.Chords)
        {
            var instrumentMeasureLayout = scoreDocumentLayoutDictionary.ChordLayouts.FirstOrDefault(l => l.ChordId == chord.Id);
            instrumentMeasureLayout ??= ChordLayoutModel.Create(chord.Id);

            memento.Chords.Add(chord.Join(instrumentMeasureLayout, scoreDocumentLayoutDictionary));
        }

        return memento;
    }

    public static ChordMemento Join(this ChordModel model, ChordLayoutModel layoutModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new ChordMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            Position = model.Position,
            RythmicDuration = model.RythmicDuration,
            Notes = [],
            XOffset = model.XOffset,
            SpaceRight = model.SpaceRight,
            GraceGroup = null
        };

        foreach (var note in model.Notes)
        {
            var instrumentMeasureLayout = scoreDocumentLayoutDictionary.NoteLayouts.FirstOrDefault(l => l.NoteId == note.Id);
            instrumentMeasureLayout ??= NoteLayoutModel.Create(note.Id);

            memento.Notes.Add(note.Join(instrumentMeasureLayout));
        }

        if(model.GraceGroup is not null)
        {
            var graceGroupLayout = scoreDocumentLayoutDictionary.GraceGroupLayouts.FirstOrDefault(l => l.GraceGroupId == model.GraceGroup.Id);
            graceGroupLayout ??= GraceGroupLayoutModel.Create(model.GraceGroup.Id);

            memento.GraceGroup = model.GraceGroup.Join(graceGroupLayout, scoreDocumentLayoutDictionary);
        }

        return memento;
    }

    public static NoteMemento Join(this NoteModel model, NoteLayoutModel layoutModel)
    {
        var memento = new NoteMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            ForceAccidental = model.ForceAccidental,
            Pitch = model.Pitch,
            Scale = model.Scale,
            StaffIndex = model.StaffIndex,
            XOffset = model.XOffset,
        };

        return memento;
    }

    public static GraceGroupMemento Join(this GraceGroupModel model, GraceGroupLayoutModel layoutModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new GraceGroupMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            BeamAngle = model.BeamAngle,
            StemDirection = model.StemDirection,
            StemLength = model.StemLength,
            Chords = [],
            ChordDuration = model.ChordDuration,
            ChordSpacing = model.ChordSpacing,
            OccupySpace = model.OccupySpace,
        };

        foreach (var chord in model.Chords)
        {
            var instrumentMeasureLayout = scoreDocumentLayoutDictionary.GraceChordLayouts.FirstOrDefault(l => l.GraceChordId == chord.Id);
            instrumentMeasureLayout ??= GraceChordLayoutModel.Create(chord.Id);

            memento.Chords.Add(chord.Join(instrumentMeasureLayout, scoreDocumentLayoutDictionary));
        }

        return memento;
    }

    public static GraceChordMemento Join(this GraceChordModel model, GraceChordLayoutModel layoutModel, ScoreDocumentLayoutDictionary scoreDocumentLayoutDictionary)
    {
        var memento = new GraceChordMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            IndexInGroup = model.IndexInGroup,
            Notes = [],
        };

        foreach (var note in model.Notes)
        {
            var instrumentMeasureLayout = scoreDocumentLayoutDictionary.GraceNoteLayouts.FirstOrDefault(l => l.GraceNoteId == note.Id);
            instrumentMeasureLayout ??= GraceNoteLayoutModel.Create(note.Id);

            memento.Notes.Add(note.Join(instrumentMeasureLayout));
        }

        return memento;
    }

    public static GraceNoteMemento Join(this GraceNoteModel model, GraceNoteLayoutModel layoutModel)
    {
        var memento = new GraceNoteMemento()
        {
            Id = model.Id,
            Layout = layoutModel,
            ForceAccidental = model.ForceAccidental,
            Pitch = model.Pitch,
            StaffIndex = model.StaffIndex,
        };

        return memento;
    }

    public static ScoreDocumentLayoutDictionary ExtractLayout(this ScoreDocumentMemento scoreDocumentMemento)
    {
        var dictionary = new ScoreDocumentLayoutDictionary()
        {
            ScoreDocumentLayout = scoreDocumentMemento.Layout,
            ChordLayouts = [],
            GraceChordLayouts = [],
            GraceGroupLayouts = [],
            GraceNoteLayouts = [],
            InstrumentMeasureLayouts = [],
            InstrumentRibbonLayouts = scoreDocumentMemento.InstrumentRibbons.Select(e => e.Layout).Where(l => l.HasFieldSet()).ToList(),
            MeasureBlockLayouts = [],
            NoteLayouts = [],
            ScoreMeasureLayouts = scoreDocumentMemento.ScoreMeasures.Select(e => e.Layout).Where(l => l.HasFieldSet()).ToList(),
        };

        foreach(var scoreMeasure in scoreDocumentMemento.ScoreMeasures)
        {
            scoreMeasure.InstrumentMeasures.Select(l => l.Layout).Where(l => l.HasFieldSet()).ForEach(dictionary.InstrumentMeasureLayouts.Add);

            foreach(var instrumentMeasure in scoreMeasure.InstrumentMeasures)
            {
                instrumentMeasure.MeasureBlocks.Select(l => l.Layout).Where(l => l.HasFieldSet()).ForEach(dictionary.MeasureBlockLayouts.Add);

                foreach(var measureBlock in instrumentMeasure.MeasureBlocks)
                {
                    measureBlock.Chords.Select(l => l.Layout).Where(l => l.HasFieldSet()).ForEach(dictionary.ChordLayouts.Add);

                    foreach(var chord in measureBlock.Chords)
                    {
                        chord.Notes.Select(l => l.Layout).Where(l => l.HasFieldSet()).ForEach(dictionary.NoteLayouts.Add);

                        var graceGroup = chord.GraceGroup;
                        if(graceGroup is not null)
                        {
                            if (graceGroup.Layout.HasFieldSet())
                            {
                                dictionary.GraceGroupLayouts.Add(graceGroup.Layout);
                            }

                            graceGroup.Chords.Select(l => l.Layout).Where(l => l.HasFieldSet()).ForEach(dictionary.GraceChordLayouts.Add);

                            foreach(var graceChord in graceGroup.Chords)
                            {
                                graceChord.Notes.Select(l => l.Layout).Where(l => l.HasFieldSet()).ForEach(dictionary.GraceNoteLayouts.Add);
                            }
                        }
                    }
                }
            }
        }

        return dictionary;
    }
}


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

