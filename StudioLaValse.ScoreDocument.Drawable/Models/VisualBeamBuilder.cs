﻿using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using System.Diagnostics;

namespace StudioLaValse.ScoreDocument.Drawable.Models
{
    public sealed class VisualBeamBuilder : IVisualBeamBuilder
    {



        public VisualBeamBuilder()
        {

        }



        public IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double scale, ColorARGB color)
        {
            var groupUp = true;

            if (stems.Any())
            {
                groupUp = stems.First().VisuallyUp;
            }

            var beamThickness = 0.8 * scale;
            var beamSpacing = 0.2 * scale;

            if (!stems.Any())
            {
                return new List<BaseDrawableElement>();
            }

            if (stems.Count() == 1 && stems.First().Beams.Any())
            {
                return new List<BaseDrawableElement>() { AsFlag(stems.First(), scale, color) };
            }

            return AsGroup(stems, beamDefinition, beamSpacing, beamThickness, groupUp, color);
        }

        private static DrawableScoreGlyph AsFlag(VisualStem stem, double scale, ColorARGB color)
        {
            var flags = stem.VisuallyUp ?
                new[]
                {
                    GlyphLibrary.FlagEighthUp,
                    GlyphLibrary.FlagSixteenthUp,
                    GlyphLibrary.FlagThirtySecondUp,
                    GlyphLibrary.FlagSixtyFourthUp,
                } :
                new[]
                {
                    GlyphLibrary.FlagEighthDown,
                    GlyphLibrary.FlagSixteenthDown,
                    GlyphLibrary.FlagThirtySecondDown,
                    GlyphLibrary.FlagSixtyFourthDown,
                };

            DrawableScoreGlyph flag = null!;

            var flagIndex = -1;

            for (int i = 8; i <= 64; i *= 2)
            {
                flagIndex++;

                if (stem.Beams.TryGetValue(i, out var beam) && beam == BeamType.Flag)
                {
                    var glyph = flags[flagIndex];

                    glyph.Scale = scale;

                    flag = new DrawableScoreGlyph(
                        stem.Origin.X,
                        stem.End.Y,
                        glyph,
                        color);
                }
            }

            if (flag is null)
            {
                throw new UnreachableException("Invalid beaming information.");
            }

            return flag;
        }

        private static IEnumerable<DrawableTrapezoid> AsGroup(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamSpacing, double beamThickness, bool groupUp, ColorARGB color)
        {
            var beams = new List<DrawableTrapezoid>();

            for (var i = 8; i <= 128; i *= 2)
            {
                var beamIndex = (int)PowerOfTwo.FromInt(i)!.Power - 3;

                VisualStem? openingStem = null;

                foreach (var stem in stems)
                {
                    var stemHasBeam = stem.Beams.TryGetValue(i, out var beamType);

                    if (!stemHasBeam)
                    {
                        continue;
                    }

                    //beam not started so look for: start, hook open, hook end, else invalid.
                    if (openingStem is null)
                    {
                        if (beamType == BeamType.Start)
                        {
                            openingStem = stem;
                            continue;
                        }

                        if (beamType == BeamType.HookStart)
                        {
                            beams.Add(AsHookStart(stem, 1, beamIndex, beamDefinition, beamSpacing, beamThickness, groupUp, color));
                            continue;
                        }

                        if (beamType == BeamType.HookEnd)
                        {
                            beams.Add(AsHookEnd(stem, 1, beamIndex, beamDefinition, beamSpacing, beamThickness, groupUp, color));
                            continue;
                        }

                        throw new Exception($"Invalid beaming information: beam has been started, but stem is not start, hook start or hook end. Stem beamtype: {beamType}");
                    }

                    //beam startd so look for: continue or end, else invalid.
                    if (beamType == BeamType.Continue)
                    {
                        continue;
                    }

                    if (beamType == BeamType.End)
                    {
                        beams.Add(BetweenTwoStems(openingStem, stem, beamIndex, beamDefinition, beamSpacing, beamThickness, groupUp, color));
                        openingStem = null;
                        continue;
                    }

                    throw new Exception($"Invalid beaming information: beam has started but stem does not continue or end beam. Stem beamtype: {beamType}");
                }
            }

            return beams;
        }

        private static DrawableTrapezoid BetweenTwoStems(VisualStem left, VisualStem right, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool groupUp, ColorARGB color)
        {
            var stemUp = left.VisuallyUp;

            var ruler = left.VisuallyUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamSpacing)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamSpacing) * -1));

            if (stemUp != groupUp)
            {
                ruler = stemUp ? ruler.OffsetY(beamThickness * -1) : ruler.OffsetY(beamThickness);
            }

            var thickness = stemUp ?
                beamThickness : beamThickness * -1;

            var startPoint = ruler.IntersectVerticalRay(left.End);

            var endPoint = ruler.IntersectVerticalRay(right.End);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }

        private static DrawableTrapezoid AsHookEnd(VisualStem stem, double length, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool GroupUp, ColorARGB color)
        {
            var stemUp = stem.VisuallyUp;

            var ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamSpacing)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamSpacing) * -1));

            if (stemUp != GroupUp)
            {
                ruler = stemUp ? ruler.OffsetY(beamThickness * -1) : ruler.OffsetY(beamThickness);
            }

            var thickness = stemUp ?
                beamThickness : beamThickness * -1;

            var endPoint = ruler.IntersectVerticalRay(stem.End);

            var startPoint = endPoint.Move(length * -1, ruler.Angle);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }

        private static DrawableTrapezoid AsHookStart(VisualStem stem, double length, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool groupUp, ColorARGB color)
        {
            var stemUp = stem.VisuallyUp;

            var ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamSpacing)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamSpacing) * -1));

            if (stemUp != groupUp)
            {
                ruler = stemUp ? ruler.OffsetY(beamThickness * -1) : ruler.OffsetY(beamThickness);
            }

            var thickness = stemUp ?
                beamThickness : beamThickness * -1;

            var startPoint = ruler.IntersectVerticalRay(stem.End);

            var endPoint = startPoint.Move(length, ruler.Angle);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }
    }
}
