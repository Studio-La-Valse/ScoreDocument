using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal static class ScoreEditorExtensions
    {
        public static ScoreDocumentProxy ProxyAuthor(this ScoreDocumentCore score, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            var selector = new AuthorLayoutSelector();
            return score.Proxy(commandManager, notifyEntityChanged, selector);
        }

        public static ScoreDocumentProxy ProxyUser(this ScoreDocumentCore score, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            var selector = new UserLayoutSelector();
            return score.Proxy(commandManager, notifyEntityChanged, selector);
        }

        public static ScoreDocumentProxy Proxy(this ScoreDocumentCore score, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new ScoreDocumentProxy(score, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static InstrumentRibbonProxy Proxy(this InstrumentRibbon instrumentRibbon, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new InstrumentRibbonProxy(instrumentRibbon, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static ScoreMeasureProxy Proxy(this ScoreMeasure measureEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new ScoreMeasureProxy(measureEditor, commandManager, notifyEntityChanged, layoutSelector);
        }



        public static InstrumentMeasureProxy Proxy(this InstrumentMeasure measureEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new InstrumentMeasureProxy(measureEditor, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static MeasureBlockChainProxy Proxy(this MeasureBlockChain measureEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new MeasureBlockChainProxy(measureEditor, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static MeasureBlockProxy Proxy(this MeasureBlock chordGroup, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new MeasureBlockProxy(chordGroup, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static GraceGroupProxy Proxy(this GraceGroup noteEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new GraceGroupProxy(noteEditor, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static ChordProxy Proxy(this Chord chordEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new ChordProxy(chordEditor, commandManager, notifyEntityChanged, layoutSelector);
        }



        public static GraceChordProxy Proxy(this GraceChord noteEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new GraceChordProxy(noteEditor, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static NoteProxy Proxy(this Note noteEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new NoteProxy(noteEditor, commandManager, notifyEntityChanged, layoutSelector);
        }

        public static GraceNoteProxy Proxy(this GraceNote noteEditor, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            return new GraceNoteProxy(noteEditor, commandManager, notifyEntityChanged, layoutSelector);
        }
    }
}
