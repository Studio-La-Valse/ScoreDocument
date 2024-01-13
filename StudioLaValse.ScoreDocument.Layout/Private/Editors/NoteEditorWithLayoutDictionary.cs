using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class NoteEditorWithLayoutDictionary : INoteEditor
    {
        private readonly INoteEditor noteEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public NoteEditorWithLayoutDictionary(INoteEditor noteEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.noteEditor = noteEditor;
            this.layoutDictionary = layoutDictionary;
        }

        public Pitch Pitch { get => noteEditor.Pitch; set => noteEditor.Pitch = value; }

        public int Voice => noteEditor.Voice;

        public bool Grace => noteEditor.Grace;

        public Position Position => noteEditor.Position;

        public RythmicDuration RythmicDuration => noteEditor.RythmicDuration;

        public Tuplet Tuplet => noteEditor.Tuplet;

        Pitch INote.Pitch => ((INote)noteEditor).Pitch;

        public int Id => noteEditor.Id;

        public IMeasureElementLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(noteEditor);
        }

        public void ApplyLayout(IMeasureElementLayout layout)
        {
            layoutDictionary.Assign(noteEditor, layout);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return noteEditor.Equals(other);
        }
    }
}
