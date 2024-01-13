using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ScoreDocumentEditor
{
    internal class MeasureBlockEditorWithStateWatcher : IMeasureBlockEditor
    {
        private readonly IMeasureBlockEditor source;
        private readonly IInstrumentMeasure host;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;

        public MeasureBlockEditorWithStateWatcher(IMeasureBlockEditor source, IInstrumentMeasure host, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged)
        {
            this.source = source;
            this.host = host;
            this.notifyEntityChanged = notifyEntityChanged;
        }

        public bool Grace => source.Grace;

        public Duration Duration => source.Duration;

        public int Id => source.Id;

        public void AppendChord(RythmicDuration rythmicDuration)
        {
            source.AppendChord(rythmicDuration);
            notifyEntityChanged.Invalidate(host);
        }

        public void ApplyLayout(INoteGroupLayout layout)
        {
            source.ApplyLayout(layout);
            notifyEntityChanged.Invalidate(host);
        }

        public void Clear()
        {
            source.Clear();
            notifyEntityChanged.Invalidate(host);
        }

        public IEnumerable<IChordEditor> EditChords()
        {
            return source.EditChords().Select(e => e.UseStateWatcher(host, notifyEntityChanged));
        }

        public IEnumerable<IChord> EnumerateChords()
        {
            return source.EnumerateChords();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public INoteGroupLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public void Rebeam()
        {
            source.Rebeam();
            notifyEntityChanged.Invalidate(host);
        }

        public void Splice(int index)
        {
            source.Splice(index);
            notifyEntityChanged.Invalidate(host);
        }
    }
}
