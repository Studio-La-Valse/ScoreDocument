namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal static class ScoreEditorExtensions
    {
        public static IScoreDocumentEditor UseTransaction(this IScoreDocumentEditor editor, ICommandManager commandManager)
        {
            return new ScoreDocumentEditorWithTransaction(editor, commandManager);
        }

        public static IInstrumentRibbonEditor UseTransaction(this IInstrumentRibbonEditor instrumentRibbon, ICommandManager commandManager)
        {
            return new InstrumentRibbonWithTransaction(instrumentRibbon, commandManager);
        }

        public static IScoreMeasureEditor UseTransaction(this IScoreMeasureEditor measureEditor, ICommandManager commandManager)
        {
            return new ScoreMeasureEditorWithCommandManager(measureEditor, commandManager);
        }

        public static IInstrumentMeasureEditor UseTransaction(this IInstrumentMeasureEditor measureEditor, ICommandManager commandManager)
        {
            return new RibbonMeasureEditorWithCommandManager(measureEditor, commandManager);
        }

        public static IMeasureBlockChainEditor UseTransaction(this IMeasureBlockChainEditor measureEditor, ICommandManager commandManager)
        {
            return new MeasureBlockChainWithCommandManager(measureEditor, commandManager);
        }

        public static IMeasureBlockEditor UseTransaction(this IMeasureBlockEditor chordGroup, ICommandManager commandManager)
        {
            return new ChordGroupEditorWithCommandManager(chordGroup, commandManager);
        }

        public static IChordEditor UseTransaction(this IChordEditor chordEditor, ICommandManager commandManager)
        {
            return new ChordEditorWithCommandManager(chordEditor, commandManager);
        }

        public static INoteEditor UseTransaction(this INoteEditor noteEditor, ICommandManager commandManager)
        {
            return new NoteEditorWithCommandManager(noteEditor, commandManager);
        }

        public static IStaffEditor UseTransaction(this IStaffEditor staffEditor, ICommandManager commandManager)
        {
            return new StaffEditorWithCommandManager(staffEditor, commandManager);
        }

        public static IStaffGroupEditor UseTransaction(this IStaffGroupEditor staffGroup, ICommandManager commandManager)
        {
            return new StaffGroupEditorWithCommandManager(staffGroup, commandManager);
        }

        public static IStaffSystemEditor UseTransaction(this IStaffSystemEditor staffSystem, ICommandManager commandManager)
        {
            return new StaffSystemEditorWithCommandManager(staffSystem, commandManager);
        }
    }
}
