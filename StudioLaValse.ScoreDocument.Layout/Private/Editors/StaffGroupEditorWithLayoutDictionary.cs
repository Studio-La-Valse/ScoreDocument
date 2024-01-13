using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class StaffGroupEditorWithLayoutDictionary : IStaffGroupEditor
    {
        private readonly IStaffGroupEditor staffGruopEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public StaffGroupEditorWithLayoutDictionary(IStaffGroupEditor staffGruopEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.staffGruopEditor = staffGruopEditor;
            this.layoutDictionary = layoutDictionary;
        }

        public Instrument Instrument => staffGruopEditor.Instrument;

        public int Id => staffGruopEditor.Id;

        public int IndexInScore => staffGruopEditor.IndexInScore;

        public IStaffEditor EditStaff(int staffIndex)
        {
            return staffGruopEditor.EditStaff(staffIndex).UseLayout(layoutDictionary);
        }

        public IEnumerable<IStaffEditor> EditStaves()
        {
            return staffGruopEditor.EditStaves().Select(e => e.UseLayout(layoutDictionary));
        }

        public IEnumerable<IRibbonMeasure> EnumerateMeasures()
        {
            return staffGruopEditor.EnumerateMeasures();
        }

        public IEnumerable<IStaff> EnumerateStaves()
        {
            return staffGruopEditor.EnumerateStaves();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return staffGruopEditor.Equals(other);
        }


        public IStaffGroupLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(staffGruopEditor);
        }

        public void ApplyLayout(IStaffGroupLayout layout)
        {
            staffGruopEditor.ApplyLayout(layout);
        }
    }
}
