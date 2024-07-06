using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal class AuthorLayoutSelector : ILayoutSelector
    {
        public IChordLayout ChordLayout(Chord chord)
        {
            return chord.AuthorLayout;
        }

        public IGraceChordLayout GraceChordLayout(GraceChord graceChord)
        {
            return graceChord.AuthorLayout;
        }

        public IGraceGroupLayout GraceGroupLayout(GraceGroup graceGroup)
        {
            return graceGroup.AuthorLayout;
        }

        public IGraceNoteLayout GraceNoteLayout(GraceNote graceNote)
        {
            return graceNote.AuthorLayout;
        }

        public IInstrumentMeasureLayout InstrumentMeasureLayout(InstrumentMeasure instrumentMeasure)
        {
            return instrumentMeasure.AuthorLayout;
        }

        public IInstrumentRibbonLayout InstrumentRibbonLayout(InstrumentRibbon instrumentRibbon)
        {
            return instrumentRibbon.AuthorLayout;
        }

        public IMeasureBlockLayout MeasureBlockLayout(MeasureBlock measureBlock)
        {
            return measureBlock.AuthorLayout;
        }

        public INoteLayout NoteLayout(Note note)
        {
            return note.AuthorLayout;
        }

        public IScoreDocumentLayout ScoreDocumentLayout(ScoreDocumentCore scoreDocumentCore)
        {
            return scoreDocumentCore.AuthorLayout;
        }

        public IScoreMeasureLayout ScoreMeasureLayout(ScoreMeasure scoreMeasure)
        {
            return scoreMeasure.AuthorLayout;
        }
    }
}
