using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IGraceGroupLayout : IMeasureBlockLayout
    {
        bool OccupySpace { get; }
        double ChordSpacing { get; }
        RythmicDuration ChordDuration { get; }
        double Scale { get; }
    }
}