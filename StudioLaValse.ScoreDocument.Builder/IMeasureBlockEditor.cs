using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a measure block editor.
    /// </summary>
    public interface IMeasureBlockEditor : IMeasureBlock<IChordEditor, IMeasureBlockEditor>, ILayoutEditor<MeasureBlockLayout>, IScoreElementEditor
    {
        /// <summary>
        /// Clears the content of the measure block.
        /// </summary>
        void Clear();
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        void AppendChord(RythmicDuration rythmicDuration);
        /// <summary>
        /// Splice a chord from the specified index.
        /// </summary>
        /// <param name="index"></param>
        void Splice(int index);
    }
}
