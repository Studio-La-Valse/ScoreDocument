using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.ScoreDocument.Drawable;

/// <summary>
/// A generic adapter to convert units to pixels.
/// </summary>
public interface IUnitToPixelConverter
{
    /// <summary>
    /// Convert units to pixels.
    /// </summary>
    /// <param name="units"></param>
    /// <returns></returns>
    double UnitsToPixels(double units);

    /// <summary>
    /// Convert pixels to units.
    /// </summary>
    /// <param name="pixels"></param>
    /// <returns></returns>
    double PixelsToUnit(double pixels);
}
