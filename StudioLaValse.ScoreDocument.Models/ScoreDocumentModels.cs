using StudioLaValse.ScoreDocument.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Models
{
    public class ScoreDocumentMetaDataModel
    {
        public required Guid ScoreDocumentId { get; set; }
        public required bool IsPublic { get; set; }
        public required string ComposerFullName { get; set; }
        public required string Title { get; set; }
        public required string Subtitle { get; set; }
        public required int CompositionYear { get; set; }
        public required int? CompositionYearStart { get; set; }
        public required int? CompositionMonth { get; set; }
        public required DateTime CreationDate { get; set; }
        public required DateTime LastEditDate { get; set; }
    }

    public class ScoreDocumentModel : ScoreDocumentMembers
    {
        public required Guid Id { get; set; }

        public required List<ScoreMeasureModel> ScoreMeasures { get; set; }

        public required List<InstrumentRibbonModel> InstrumentRibbons { get; set; }
    }

    public class ScoreMeasureModel : ScoreMeasureMembers
    {
        public required Guid Id { get; set; }

        public required List<InstrumentMeasureModel> InstrumentMeasures { get; set; }
    }

    public class InstrumentRibbonModel : InstrumentRibbonMembers
    {
        public required Guid Id { get; set; }
    }

    public class InstrumentMeasureModel : InstrumentMeasureMembers
    {
        public required Guid Id { get; set; }

        public required List<MeasureBlockModel> MeasureBlocks { get; set; }

        [Range(0, int.MaxValue)]
        public required int ScoreMeasureIndex { get; set; }

        [Range(0, int.MaxValue)]
        public required int InstrumentRibbonIndex { get; set; }
    }

    public class MeasureBlockModel : MeasureBlockMembers
    {
        public required Guid Id { get; set; }

        public required List<ChordModel> Chords { get; set; }
    }

    public class GraceGroupModel : GraceGroupMembers
    {
        public required Guid Id { get; set; }

        public required List<GraceChordModel> Chords { get; set; }
    }

    public class ChordModel : ChordMembers
    {
        public required Guid Id { get; set; }

        public required List<NoteModel> Notes { get; set; }

        public required GraceGroupModel? GraceGroup { get; set; }
    }

    public class GraceChordModel : GraceChordMembers
    {
        public required Guid Id { get; set; }

        public required List<GraceNoteModel> Notes { get; set; }
    }

    public class NoteModel : NoteMembers
    {
        public required Guid Id { get; set; }
    }

    public class GraceNoteModel : GraceNoteMembers
    {
        public required Guid Id { get; set; }
    }
}