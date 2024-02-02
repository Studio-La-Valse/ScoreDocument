using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;

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

        public void RemoveVoice(int voice)
        {
            ribbonMeasureEditor.RemoveVoice(voice);
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

        public void Clear()
        {
            ribbonMeasureEditor.Clear();
        }

        public IMeasureBlockChainEditor EditBlockChainAt(int voice)
        {
            return ribbonMeasureEditor.EditBlockChainAt(voice).UseLayout(layoutDictionary);
        }

        public IMeasureBlockChain BlockChainAt(int voice)
        {
            return ribbonMeasureEditor.BlockChainAt(voice);
        }
    }
}
