using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout.Private.Editors;
using StudioLaValse.ScoreDocument.Layout.Private.Readers;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private
{
    internal static class ScoreDocumentExtensions
    {
        public static IScoreDocumentEditor UseLayout(this IScoreDocumentEditor scoreDocumentEditor, IScoreLayoutDictionary dictionary)
        {
            return new ScoreDocumentEditorWithLayoutDictionary(scoreDocumentEditor, dictionary);
        }

        public static IChordEditor UseLayout(this IChordEditor chordEditor, IScoreLayoutDictionary dictionary)
        {
            return new ChordEditorWithLayoutDictionary(chordEditor, dictionary);
        }

        public static IInstrumentRibbonEditor UseLayout(this IInstrumentRibbonEditor instrumentRibbon, IScoreLayoutDictionary dictionary)
        {
            return new InstrumentRibbonEditorWithLayoutDictionary(instrumentRibbon, dictionary);
        }

        public static IMeasureBlockChainEditor UseLayout(this IMeasureBlockChainEditor instrumentRibbon, IScoreLayoutDictionary dictionary)
        {
            return new MeasureBlockChainWithLayoutDictionary(instrumentRibbon, dictionary);
        }

        public static IMeasureBlockEditor UseLayout(this IMeasureBlockEditor measureBlock, IScoreLayoutDictionary dictionary)
        {
            return new MeasureBlockWithLayoutDictionary(measureBlock, dictionary);
        }

        public static INoteEditor UseLayout(this INoteEditor noteEditor, IScoreLayoutDictionary dictionary)
        {
            return new NoteEditorWithLayoutDictionary(noteEditor, dictionary);
        }

        public static IInstrumentMeasureEditor UseLayout(this IInstrumentMeasureEditor measureEditor, IScoreLayoutDictionary dictionary)
        {
            return new RibbonMeasureEditorWithLayoutDictionary(measureEditor, dictionary);
        }

        public static IScoreMeasureEditor UseLayout(this IScoreMeasureEditor measureEditor, IScoreLayoutDictionary dictionary)
        {
            return new ScoreMeasureEditorWithLayoutDictionary(measureEditor, dictionary);
        }

        public static IStaffEditor UseLayout(this IStaffEditor staffEditor, IScoreLayoutDictionary dictionary)
        {
            return new StaffEditorWithLayoutDictionary(staffEditor, dictionary);
        }

        public static IStaffGroupEditor UseLayout(this IStaffGroupEditor staffGroup, IScoreLayoutDictionary dictionary)
        {
            return new StaffGroupEditorWithLayoutDictionary(staffGroup, dictionary);
        }

        public static IStaffSystemEditor UseLayout(this IStaffSystemEditor staffSystem, IScoreLayoutDictionary dictionary)
        {
            return new StaffSystemEditorWithLayoutDictionary(staffSystem, dictionary);
        }





        public static IScoreDocumentReader UseLayout(this IScoreDocumentReader scoreDocumentReader, IScoreLayoutDictionary dictionary)
        {
            return new ScoreDocumentReaderWithLayoutDictionary(scoreDocumentReader, dictionary);
        }

        public static IChordReader UseLayout(this IChordReader chordReader, IScoreLayoutDictionary dictionary)
        {
            return new ChordReaderWithLayoutDictionary(chordReader, dictionary);
        }

        public static IMeasureBlockReader UseLayout(this IMeasureBlockReader chordGroupReader, IScoreLayoutDictionary dictionary)
        {
            return new ChordGroupReaderWithLayoutDictionary(chordGroupReader, dictionary);
        }

        public static IMeasureBlockChainReader UseLayout(this IMeasureBlockChainReader chordGroupReader, IScoreLayoutDictionary dictionary)
        {
            return new MeasureBlockChainReaderWithLayoutDictionary(chordGroupReader, dictionary);
        }

        public static IInstrumentRibbonReader UseLayout(this IInstrumentRibbonReader instrumentRibbon, IScoreLayoutDictionary dictionary)
        {
            return new InstrumentRibbonReaderWithLayoutDictionary(instrumentRibbon, dictionary);
        }

        public static INoteReader UseLayout(this INoteReader noteReader, IScoreLayoutDictionary dictionary)
        {
            return new NoteReaderWithLayoutDictionary(noteReader, dictionary);
        }

        public static IInstrumentMeasureReader UseLayout(this IInstrumentMeasureReader measureReader, IScoreLayoutDictionary dictionary)
        {
            return new RibbonMeasureReaderWithLayoutDictionary(measureReader, dictionary);
        }

        public static IScoreMeasureReader UseLayout(this IScoreMeasureReader measureReader, IScoreLayoutDictionary dictionary)
        {
            return new ScoreMeasureReaderWithLayoutDictionary(measureReader, dictionary);
        }

        public static IStaffReader UseLayout(this IStaffReader staffReader, IScoreLayoutDictionary dictionary)
        {
            return new StaffReaderWithLayoutDictionary(staffReader, dictionary);
        }

        public static IStaffGroupReader UseLayout(this IStaffGroupReader staffGroup, IScoreLayoutDictionary dictionary)
        {
            return new StaffGroupReaderWithLayoutDictionary(staffGroup, dictionary);
        }

        public static IStaffSystemReader UseLayout(this IStaffSystemReader staffSystem, IScoreLayoutDictionary dictionary)
        {
            return new StaffSystemReaderWithLayoutDictionary(staffSystem, dictionary);
        }
    }
}
