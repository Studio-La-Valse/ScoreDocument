using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an instrument ribbon editor.
    /// </summary>
    public interface IInstrumentRibbonEditor : IInstrumentRibbon<IInstrumentMeasureEditor>, IScoreElementEditor
    {

    }
}
