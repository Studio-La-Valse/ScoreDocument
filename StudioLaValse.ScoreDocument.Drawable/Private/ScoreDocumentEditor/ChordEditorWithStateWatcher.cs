using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class ChordEditorWithStateWatcher : IChordEditor
    {
        private readonly IChordEditor source;
        private readonly IInstrumentMeasure host;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public ChordEditorWithStateWatcher(IChordEditor source, IInstrumentMeasure host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.host = host;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public int IndexInBlock => source.IndexInBlock;

        public bool Grace => source.Grace;

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        public void Add(params Pitch[] pitches)
        {
            source.Add(pitches);
            notifyEntityChanged.Invalidate(host);
        }

        public void ApplyLayout(IChordLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(host);
        }

        public void Clear()
        {
            source.Clear();
            notifyEntityChanged.Invalidate(host);
        }

        public IEnumerable<INoteEditor> EditNotes()
        {
            return source.EditNotes().Select(n => n.UseStateWatcher(host, notifyEntityChanged));
        }

        public IEnumerable<INote> EnumerateNotes()
        {
            return source.EnumerateNotes();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IChordLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public void Set(params Pitch[] pitches)
        {
            source.Set(pitches);
            notifyEntityChanged.Invalidate(host);
        }
    }
}
