﻿using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class ScoreMeasureEditorWithLayoutDictionary : IScoreMeasureEditor
    {
        private readonly IScoreMeasureEditor scoreMeasureEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ScoreMeasureEditorWithLayoutDictionary(IScoreMeasureEditor scoreMeasureEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.scoreMeasureEditor = scoreMeasureEditor;
            this.layoutDictionary = layoutDictionary;
        }

        public int IndexInScore => scoreMeasureEditor.IndexInScore;

        public TimeSignature TimeSignature => scoreMeasureEditor.TimeSignature;

        public int Id => scoreMeasureEditor.Id;

        public IEnumerable<IInstrumentMeasureEditor> EditMeasures()
        {
            return scoreMeasureEditor.EditMeasures().Select(e => e.UseLayout(layoutDictionary));
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return scoreMeasureEditor.EnumerateMeasures();
        }

        public IScoreMeasureLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(scoreMeasureEditor);
        }

        public void ApplyLayout(IScoreMeasureLayout layout)
        {
            layoutDictionary.Assign(scoreMeasureEditor, layout);
        }

        public IInstrumentMeasureEditor EditMeasure(int ribbonIndex)
        {
            return scoreMeasureEditor.EditMeasure(ribbonIndex);
        }

        public IStaffSystemEditor EditStaffSystemOrigin()
        {
            return scoreMeasureEditor.EditStaffSystemOrigin().UseLayout(layoutDictionary);
        }

        public IStaffSystem GetStaffSystemOrigin()
        {
            return scoreMeasureEditor.GetStaffSystemOrigin();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return scoreMeasureEditor.Equals(other);
        }
    }
}
