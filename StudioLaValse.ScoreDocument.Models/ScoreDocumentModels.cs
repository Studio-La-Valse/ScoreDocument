using StudioLaValse.ScoreDocument.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioLaValse.ScoreDocument.Models
{
    public class ScoreDocumentMetaDataModel
    {
        public required Guid Id { get; set; }
        public required Guid ScoreDocumentId { get; set; }
        public required string OwnerEmail { get; set; }
        public required bool IsPublic { get; set; }
    }
    public class ScoreDocumentModel : ScoreDocumentMembers
    {
        public required Guid Id { get; set; }

        public required List<ScoreMeasureModel> ScoreMeasures { get; set; }

        public required List<InstrumentRibbonModel> InstrumentRibbons { get; set; }

        public required ScoreDocumentLayoutModel? Layout { get; set; }

        public static ScoreDocumentModel Create()
        {
            return new ScoreDocumentModel()
            {
                Id = Guid.NewGuid(),
                InstrumentRibbons = [],
                ScoreMeasures = [],
                ChordPositionFactor = null,
                FirstSystemIndent = null,
                ForegroundColor = null,
                HorizontalStaffLineThickness = null,
                PageColor = null,
                Scale = null,
                StemLineThickness = null,
                VerticalStaffLineThickness = null,
                Layout = null
            };
        }
    }

    public class ScoreMeasureModel : ScoreMeasureMembers
    {
        public required Guid Id { get; set; }

        public required List<InstrumentMeasureModel> InstrumentMeasures { get; set; }

        public required ScoreMeasureLayoutModel? Layout { get; set; }
    }

    public class InstrumentRibbonModel : InstrumentRibbonMembers
    {
        public required Guid Id { get; set; }

        public required InstrumentRibbonLayoutModel? Layout { get; set; }
    }

    public class InstrumentMeasureModel : InstrumentMeasureMembers
    {
        public required Guid Id { get; set; }

        public required List<MeasureBlockModel> MeasureBlocks { get; set; }

        public required InstrumentMeasureLayoutModel? Layout { get; set; }
    }

    public class MeasureBlockModel : MeasureBlockMembers
    {
        public required Guid Id { get; set; }

        public required List<ChordModel> Chords { get; set; }

        public required MeasureBlockLayoutModel? Layout { get; set; }
    }

    public class ChordModel : ChordMembers
    {
        public required Guid Id { get; set; }

        public required List<NoteModel> Notes { get; set; }

        public required ChordLayoutModel? Layout { get; set; }
    }

    public class NoteModel : NoteMembers
    {
        public required Guid Id { get; set; }

        public required NoteLayoutModel? Layout { get; set; }
    }
}