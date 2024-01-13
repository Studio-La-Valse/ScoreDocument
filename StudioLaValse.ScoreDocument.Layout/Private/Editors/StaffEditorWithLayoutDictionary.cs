using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;


namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class StaffEditorWithLayoutDictionary : IStaffEditor
    {
        private readonly IStaffEditor staffEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public StaffEditorWithLayoutDictionary(IStaffEditor staffEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.staffEditor = staffEditor;
            this.layoutDictionary = layoutDictionary;
        }

        public int IndexInStaffGroup => staffEditor.IndexInStaffGroup;

        public int Id => staffEditor.Id;

        public void ApplyLayout(IStaffLayout layout)
        {
            layoutDictionary.Assign(staffEditor, layout);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return staffEditor.Equals(other);
        }

        public IStaffLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(staffEditor);
        }

        void IStaffEditor.ApplyLayout(IStaffLayout ReadLayout)
        {
            staffEditor.ApplyLayout(ReadLayout);
        }
    }
}
