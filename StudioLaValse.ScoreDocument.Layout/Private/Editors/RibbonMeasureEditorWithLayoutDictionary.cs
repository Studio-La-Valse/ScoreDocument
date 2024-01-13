using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class RibbonMeasureEditorWithLayoutDictionary : IInstrumentMeasureEditor
    {
        private readonly IInstrumentMeasureEditor ribbonMeasureEditor;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public RibbonMeasureEditorWithLayoutDictionary(IInstrumentMeasureEditor ribbonMeasureEditor, IScoreLayoutDictionary layoutDictionary)
        {
            this.ribbonMeasureEditor = ribbonMeasureEditor;
            this.layoutDictionary = layoutDictionary;
        }

        public int MeasureIndex => ribbonMeasureEditor.MeasureIndex;

        public TimeSignature TimeSignature => ribbonMeasureEditor.TimeSignature;

        public int RibbonIndex => ribbonMeasureEditor.RibbonIndex;

        public Instrument Instrument => ribbonMeasureEditor.Instrument;

        public int Id => ribbonMeasureEditor.Id;

        public void AddVoice(int voice)
        {
            ribbonMeasureEditor.AddVoice(voice);
        }

        public void AppendBlock(int voice, Duration duration, bool grace)
        {
            ribbonMeasureEditor.AppendBlock(voice, duration, grace);
        }

        public void ClearVoice(int voice)
        {
            ribbonMeasureEditor.ClearVoice(voice);
        }

        public IEnumerable<IMeasureBlockEditor> EditBlocks(int voice)
        {
            return ribbonMeasureEditor.EditBlocks(voice);
        }

        public void PrependBlock(int voice, Duration duration, bool grace)
        {
            ribbonMeasureEditor.PrependBlock(voice, duration, grace);
        }

        public IInstrumentMeasureLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(ribbonMeasureEditor);
        }

        public void ApplyLayout(IInstrumentMeasureLayout layout)
        {
            layoutDictionary.Assign(ribbonMeasureEditor, layout);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return ribbonMeasureEditor.Equals(other);
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return ribbonMeasureEditor.EnumerateVoices();
        }

        public IEnumerable<IMeasureBlock> EnumerateBlocks(int voice)
        {
            return ribbonMeasureEditor.EnumerateBlocks(voice);
        }

        public void Clear()
        {
            ribbonMeasureEditor.Clear();
        }
    }
}
