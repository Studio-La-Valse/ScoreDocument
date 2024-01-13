using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface INoteGroupLayout : IScoreElementLayout<INoteGroupLayout>
    {
        double StemLength { get; }
        double BeamAngle { get; }
    }
}
