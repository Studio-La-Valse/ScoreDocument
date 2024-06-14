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

/// <summary>
/// A class to convert millimeters to pixels, depending on screen dpi
/// </summary>
public class MmToPixelConverter : IUnitToPixelConverter
{
    private readonly double dpi;

    /// <summary>
    /// The default constructor
    /// </summary>
    public MmToPixelConverter()
    {
        this.dpi = 72;
    }

    /// <inheritdoc/>
    public double PixelsToUnit(double pixels)
    {
        // 1 inch = 25.4 mm
        // 1 px = 1 inch / dpi
        // 1 px = 25.4 mm / dpi
        var mm = dpi / 25.4 / pixels;
        return mm;
    }

    /// <inheritdoc/>
    public double UnitsToPixels(double mm)
    {
        // 1 inch = 25.4 mm
        // dpi = pixels / inch
        // pixels/mm = dpi * 25.4
        var pixels = dpi / 25.4 * mm;
        return pixels;
    }
}
