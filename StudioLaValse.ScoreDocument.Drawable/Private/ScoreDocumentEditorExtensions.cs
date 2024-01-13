using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private
{
    internal static class ScoreDocumentEditorExtensions
    {
        public static IScoreDocumentEditor UseStateWatcher(this IScoreDocumentEditor scoreDocumentEditor, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new ScoreDocumentEditorWithStateWatcher(scoreDocumentEditor, notifyEntityChanged);
        }

        public static IMeasureBlockEditor UseStateWatcher(this IMeasureBlockEditor chordEditor, IInstrumentMeasure host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new MeasureBlockEditorWithStateWatcher(chordEditor, host, notifyEntityChanged);
        }

        public static IChordEditor UseStateWatcher(this IChordEditor chordEditor, IInstrumentMeasure host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new ChordEditorWithStateWatcher(chordEditor, host, notifyEntityChanged);
        }

        public static IInstrumentRibbonEditor UseStateWatcher(this IInstrumentRibbonEditor instrumentRibbon, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new InstrumentRibbonEditorWithStateWatcher(instrumentRibbon, notifyEntityChanged);
        }

        public static INoteEditor UseStateWatcher(this INoteEditor noteEditor, IInstrumentMeasure host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new NoteEditorWithStateWatcher(noteEditor, host, notifyEntityChanged);
        }

        public static IInstrumentMeasureEditor UseStateWatcher(this IInstrumentMeasureEditor measureEditor, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new RibbonMeasureEditorWithStateWatcher(measureEditor, notifyEntityChanged);
        }

        public static IScoreMeasureEditor UseStateWatcher(this IScoreMeasureEditor measureEditor, IScoreDocument host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new ScoreMeasureEditorWithStateWatcher(measureEditor, host, notifyEntityChanged);
        }

        public static IStaffEditor UseStateWatcher(this IStaffEditor staffEditor, IScoreDocument scoreDocument, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new StaffEditorWithLayoutStateWatcher(staffEditor, scoreDocument, notifyEntityChanged);
        }

        public static IStaffGroupEditor UseUseStateWatcherLayout(this IStaffGroupEditor staffGroup, IScoreDocument scoreDocument, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new StaffGroupEditorWithStateWatcher(staffGroup, scoreDocument, notifyEntityChanged);
        }

        public static IStaffSystemEditor UseStateWatcher(this IStaffSystemEditor staffSystem, IScoreDocument scoreDocument, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            return new StaffSystemEditorWithStateWatcher(staffSystem, scoreDocument, notifyEntityChanged);
        }
    }
}
