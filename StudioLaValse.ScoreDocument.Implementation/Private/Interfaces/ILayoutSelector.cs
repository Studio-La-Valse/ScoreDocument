namespace StudioLaValse.ScoreDocument.Implementation.Private.Interfaces
{
    internal interface ILayoutSelector
    {
        IChordLayout ChordLayout(Chord chord);
        IRestLayout RestLayout(Chord chord);
        IGraceChordLayout GraceChordLayout(GraceChord graceChord);
        IRestLayout RestLayout(GraceChord graceChord);
        IGraceGroupLayout GraceGroupLayout(GraceGroup graceGroup);
        IGraceNoteLayout GraceNoteLayout(GraceNote graceNote);
        IInstrumentMeasureLayout InstrumentMeasureLayout(InstrumentMeasure instrumentMeasure);
        IInstrumentRibbonLayout InstrumentRibbonLayout(InstrumentRibbon instrumentRibbon);
        IMeasureBlockLayout MeasureBlockLayout(MeasureBlock measureBlock);
        INoteLayout NoteLayout(Note note);
        IScoreDocumentLayout ScoreDocumentLayout(ScoreDocumentCore scoreDocumentCore);
        IScoreMeasureLayout ScoreMeasureLayout(ScoreMeasure scoreMeasure);
    }
}
