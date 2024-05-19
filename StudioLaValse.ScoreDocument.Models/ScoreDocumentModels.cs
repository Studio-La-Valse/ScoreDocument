using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Models
{
    public class ScoreDocumentModel : ScoreDocumentMembers
    {
        public required List<ScoreMeasureModel> ScoreMeasures { get; set; }

        public required List<InstrumentRibbonModel> InstrumentRibbons { get; set; }

        public required ScoreDocumentLayoutModel? Layout { get; set; }

        public static ScoreDocumentModel Create()
        {
            return new ScoreDocumentModel()
            {
                Id = Guid.NewGuid(),
                InstrumentRibbons = [],
                InstrumentScales = [],
                ScoreMeasures = [],
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
        public required List<InstrumentMeasureModel> InstrumentMeasures { get; set; }

        public required ScoreMeasureLayoutModel? Layout { get; set; }
    }

    public class InstrumentRibbonModel : InstrumentRibbonMembers
    {
        public required InstrumentRibbonLayoutModel? Layout { get; set; }
    }

    public class InstrumentMeasureModel : InstrumentMeasureMembers
    {
        public required List<MeasureBlockModel> MeasureBlocks { get; set; }

        public required InstrumentMeasureLayoutModel? Layout { get; set; }
    }

    public class MeasureBlockModel : MeasureBlockMembers
    {
        public required List<ChordModel> Chords { get; set; }

        public required MeasureBlockLayoutModel? Layout { get; set; }
    }

    public class ChordModel : ChordMembers
    {
        public required List<NoteModel> Notes { get; set; }

        public required ChordLayoutModel? Layout { get; set; }
    }

    public class NoteModel : NoteMembers
    {
        public required NoteLayoutModel? Layout { get; set; }
    }
}