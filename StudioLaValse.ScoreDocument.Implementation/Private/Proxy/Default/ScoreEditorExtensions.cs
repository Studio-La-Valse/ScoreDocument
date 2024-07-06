using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal static class ScoreEditorExtensions
    {
        public static ScoreDocumentProxy ProxyAuthor(this ScoreDocumentCore score)
        {
            var selector = new AuthorLayoutSelector();
            return score.Proxy(selector);
        }

        public static ScoreDocumentProxy ProxyUser(this ScoreDocumentCore score)
        {
            var selector = new UserLayoutSelector();
            return score.Proxy(selector);
        }

        public static ScoreDocumentProxy Proxy(this ScoreDocumentCore score, ILayoutSelector layoutSelector)
        {
            return new ScoreDocumentProxy(score, layoutSelector);
        }

        public static InstrumentRibbonProxy Proxy(this InstrumentRibbon instrumentRibbon, ILayoutSelector layoutSelector)
        {
            return new InstrumentRibbonProxy(instrumentRibbon, layoutSelector);
        }

        public static ScoreMeasureProxy Proxy(this ScoreMeasure measureEditor, ILayoutSelector layoutSelector)
        {
            return new ScoreMeasureProxy(measureEditor, layoutSelector);
        }



        public static InstrumentMeasureProxy Proxy(this InstrumentMeasure measureEditor, ILayoutSelector layoutSelector)
        {
            return new InstrumentMeasureProxy(measureEditor, layoutSelector);
        }

        public static MeasureBlockChainProxy Proxy(this MeasureBlockChain measureEditor, ILayoutSelector layoutSelector)
        {
            return new MeasureBlockChainProxy(measureEditor, layoutSelector);
        }

        public static MeasureBlockProxy Proxy(this MeasureBlock chordGroup, ILayoutSelector layoutSelector)
        {
            return new MeasureBlockProxy(chordGroup, layoutSelector);
        }

        public static GraceGroupProxy Proxy(this GraceGroup noteEditor, ILayoutSelector layoutSelector)
        {
            return new GraceGroupProxy(noteEditor, layoutSelector);
        }

        public static ChordProxy Proxy(this Chord chordEditor, ILayoutSelector layoutSelector)
        {
            return new ChordProxy(chordEditor, layoutSelector);
        }



        public static GraceChordProxy Proxy(this GraceChord noteEditor, ILayoutSelector layoutSelector)
        {
            return new GraceChordProxy(noteEditor, layoutSelector);
        }

        public static NoteProxy Proxy(this Note noteEditor, ILayoutSelector layoutSelector)
        {
            return new NoteProxy(noteEditor, layoutSelector);
        }

        public static GraceNoteProxy Proxy(this GraceNote noteEditor, ILayoutSelector layoutSelector)
        {
            return new GraceNoteProxy(noteEditor, layoutSelector);
        }
    }
}
