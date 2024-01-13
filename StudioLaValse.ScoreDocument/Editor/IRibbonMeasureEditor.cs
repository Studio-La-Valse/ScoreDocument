using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IRibbonMeasureEditor : IRibbonMeasure
    {
        int MeasureIndex { get; }
        int RibbonIndex { get; }


        IEnumerable<IMeasureBlockEditor> EditBlocks(int voice);


        void Clear();
        void ClearVoice(int voice);
        void AddVoice(int voice);


        void PrependBlock(int voice, Duration duration, bool grace);
        void AppendBlock(int voice, Duration duration, bool grace);


        void ApplyLayout(IRibbonMeasureLayout layout);
        IRibbonMeasureLayout ReadLayout();
    }
}
