using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class NoteEditorWithStateWatcher : INoteEditor
    {
        private readonly INoteEditor source;
        private readonly IRibbonMeasure host;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public NoteEditorWithStateWatcher(INoteEditor source, IRibbonMeasure scoreMeasure, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            host = scoreMeasure;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public Pitch Pitch
        {
            get
            {
                return source.Pitch;
            }
            set
            {
                source.Pitch = value;
                notifyEntityChanged.Invalidate(host);
            }
        }

        public int Voice => source.Voice;

        public bool Grace => source.Grace;

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        Pitch INote.Pitch => ((INote)source).Pitch;

        public void ApplyLayout(IMeasureElementLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(host);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IMeasureElementLayout ReadLayout()
        {
            return source.ReadLayout();
        }
    }
}
