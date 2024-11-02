using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal class UserLayoutSelector : ILayoutSelector
    {
        public IChordLayout ChordLayout(Chord chord)
        {
            return chord.UserLayout;
        }

        public IGraceChordLayout GraceChordLayout(GraceChord graceChord)
        {
            return graceChord.UserLayout;
        }

        public IGraceGroupLayout GraceGroupLayout(GraceGroup graceGroup)
        {
            return graceGroup.UserLayout;
        }

        public IGraceNoteLayout GraceNoteLayout(GraceNote graceNote)
        {
            return graceNote.UserLayout;
        }

        public IInstrumentMeasureLayout InstrumentMeasureLayout(InstrumentMeasure instrumentMeasure)
        {
            return instrumentMeasure.UserLayout;
        }

        public IInstrumentRibbonLayout InstrumentRibbonLayout(InstrumentRibbon instrumentRibbon)
        {
            return instrumentRibbon.UserLayout;
        }

        public IMeasureBlockLayout MeasureBlockLayout(MeasureBlock measureBlock)
        {
            return measureBlock.UserLayout;
        }

        public INoteLayout NoteLayout(Note note)
        {
            return note.UserLayout;
        }

        public IRestLayout RestLayout(Chord chord)
        {
            return chord.UserRestLayout;
        }

        public IRestLayout RestLayout(GraceChord graceChord)
        {
            return graceChord.UserRestLayout;
        }

        public IScoreDocumentLayout ScoreDocumentLayout(ScoreDocumentCore scoreDocumentCore)
        {
            return scoreDocumentCore.UserLayout;
        }

        public IScoreMeasureLayout ScoreMeasureLayout(ScoreMeasure scoreMeasure)
        {
            return scoreMeasure.UserLayout;
        }
    }
}
