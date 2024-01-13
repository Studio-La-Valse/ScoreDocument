using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class StaffSystemEditorWithLayoutDictionary : IStaffSystemEditor
    {
        private readonly IStaffSystemEditor staffSystemEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;
        public int Id => staffSystemEditor.Id;

        public StaffSystemEditorWithLayoutDictionary(IStaffSystemEditor staffSystemEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.staffSystemEditor = staffSystemEditor;
            this.layoutDictionary = layoutDictionary;
        }




        public IEnumerable<IStaffGroup> EnumerateStaffGroups()
        {
            return staffSystemEditor.EnumerateStaffGroups();
        }
        public IEnumerable<IStaffGroupEditor> EditStaffGroups()
        {
            return staffSystemEditor.EditStaffGroups().Select(e => e.UseLayout(layoutDictionary));
        }



        public IEnumerable<IScoreMeasure> EnumerateMeasures()
        {
            return staffSystemEditor.EnumerateMeasures();
        }


        public bool Equals(IUniqueScoreElement? other)
        {
            return staffSystemEditor.Equals(other);
        }



        public IStaffSystemLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(staffSystemEditor);
        }
        public void ApplyLayout(IStaffSystemLayout layout)
        {
            layoutDictionary.Assign(staffSystemEditor, layout);
        }


        public IStaffGroupEditor EditStaffGroup(int indexInScore)
        {
            return staffSystemEditor.EditStaffGroup(indexInScore).UseLayout(layoutDictionary);
        }
    }
}
