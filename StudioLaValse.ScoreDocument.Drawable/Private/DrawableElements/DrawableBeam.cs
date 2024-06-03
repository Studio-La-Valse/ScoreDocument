using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;
internal class DrawableBeam : DrawablePolygon
{
    public DrawableBeam(XY bottomLeft, XY bottomRight, double thickness, bool canvasUp, ColorARGB fill) : base(ToPoints(bottomLeft, bottomRight, thickness, canvasUp), fill)
    {
        
    }

    private static List<XY> ToPoints(XY bottomLeft, XY bottomRight, double thickness, bool canvasUp)
    {
        return canvasUp ?
            [
                bottomLeft, bottomRight, bottomRight - new XY(0, thickness), bottomLeft - new XY(0, thickness)
            ] :
            new List<XY>()
            {
                bottomLeft, bottomRight, bottomRight + new XY(0, thickness), bottomLeft + new XY(0, thickness)
            };
    }
}
