using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class ChordEditorWithLayoutDictionary : IChordEditor
    {
        private readonly IChordEditor chordEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ChordEditorWithLayoutDictionary(IChordEditor chordEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.chordEditor = chordEditor;
            this.layoutDictionary = layoutDictionary;
        }

        public int Voice => chordEditor.Voice;

        public bool Grace => chordEditor.Grace;

        public Position Position => chordEditor.Position;

        public RythmicDuration RythmicDuration => chordEditor.RythmicDuration;

        public Tuplet Tuplet => chordEditor.Tuplet;

        public int Id => chordEditor.Id;

        public int IndexInBlock => chordEditor.IndexInBlock;

        public void Add(params Pitch[] pitches)
        {
            chordEditor.Add(pitches);
        }

        public void Set(params Pitch[] pitches)
        {
            chordEditor.Set(pitches);
        }

        public void ApplyLayout(IMeasureElementContainerLayout layout)
        {
            layoutDictionary.Assign(chordEditor, layout);
        }

        public void Clear()
        {
            chordEditor.Clear();
        }

        public IEnumerable<INoteEditor> EditNotes()
        {
            return chordEditor.EditNotes().Select(e => e.UseLayout(layoutDictionary));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return chordEditor.Equals(other);
        }

        public IEnumerable<INote> EnumerateNotes()
        {
            return chordEditor.EnumerateNotes();
        }

        public IMeasureElementContainerLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(chordEditor);
        }

    }
}
