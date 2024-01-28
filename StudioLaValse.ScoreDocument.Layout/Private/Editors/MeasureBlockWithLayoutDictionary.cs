using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class MeasureBlockWithLayoutDictionary : IMeasureBlockEditor
    {
        private readonly IMeasureBlockEditor source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public MeasureBlockWithLayoutDictionary(IMeasureBlockEditor source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public bool Grace => source.Grace;

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;

        public void AppendChord(RythmicDuration rythmicDuration)
        {
            source.AppendChord(rythmicDuration);
        }

        public void ApplyLayout(INoteGroupLayout layout)
        {
            layoutDictionary.Assign(source, layout);
        }

        public void Clear()
        {
            source.Clear();
        }

        public IEnumerable<IChordEditor> EditChords()
        {
            return source.EditChords();
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
            return layoutDictionary.GetOrCreate(source);
        }

        public void Rebeam()
        {
            source.Rebeam();
        }

        public void Splice(int index)
        {
            source.Splice(index);
        }
    }
}
