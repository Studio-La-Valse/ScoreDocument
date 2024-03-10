﻿using StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using System.Diagnostics;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal sealed class VisualBeamBuilder : IVisualBeamBuilder
    {
        public VisualBeamBuilder()
        {

        }


        public IChordReader GetLayout(VisualStem stem)
        {
            return stem.Chord;
        }


        public IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double scale, ColorARGB color)
        {
            if (!stems.Any() || !stems.First().Chord.ReadBeamTypes().Any())
            {
                return [];
            }

            bool groupUp = stems.First().VisuallyUp;

            double beamThickness = 0.8 * scale;
            double beamSpacing = 0.2 * scale;

            return stems.Count() == 1
                ? ([AsFlag(stems.First(), scale, color)])
                : AsGroup(stems, beamDefinition, beamSpacing, beamThickness, groupUp, color);
        }

        public DrawableScoreGlyph AsFlag(VisualStem stem, double scale, ColorARGB color)
        {
            Glyph[] flags = stem.VisuallyUp ?
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

            int flagIndex = -1;

            for (PowerOfTwo i = 8; i <= 64; i = i.Double())
            {
                flagIndex++;

                BeamType? beam = GetLayout(stem).ReadBeamType(i);
                if (beam is not null and BeamType.Flag)
                {
                    Glyph glyph = flags[flagIndex];

                    glyph.Scale = scale;

                    flag = new DrawableScoreGlyph(
                        stem.Origin.X,
                        stem.End.Y,
                        glyph,
                        color);
                }
            }

            return flag is null ? throw new UnreachableException("Invalid beaming information.") : flag;
        }

        public IEnumerable<DrawableTrapezoid> AsGroup(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamSpacing, double beamThickness, bool groupUp, ColorARGB color)
        {
            List<DrawableTrapezoid> beams = [];

            for (PowerOfTwo i = 8; i <= 128; i = i.Double())
            {
                if (!PowerOfTwo.TryCreate(i, out PowerOfTwo? powerOfTwo))
                {
                    throw new UnreachableException();
                }

                int beamIndex = powerOfTwo.Power - 3;


                VisualStem? openingStem = null;

                foreach (VisualStem stem in stems)
                {
                    BeamType? beamType = GetLayout(stem).ReadBeamType(i);
                    if (beamType is null)
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

        public static DrawableTrapezoid BetweenTwoStems(VisualStem left, VisualStem right, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool groupUp, ColorARGB color)
        {
            bool stemUp = left.VisuallyUp;
            Ruler ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            if (stemUp != groupUp)
            {
                ruler = stemUp ?
                    ruler.OffsetY(beamThickness) :
                    ruler.OffsetY(beamThickness * -1);
            }

            double thickness = stemUp ?
                beamThickness :
                beamThickness * -1;

            XY startPoint = ruler.IntersectVerticalRay(left.End);

            XY endPoint = ruler.IntersectVerticalRay(right.End);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }

        public static DrawableTrapezoid AsHookEnd(VisualStem stem, double length, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool GroupUp, ColorARGB color)
        {
            bool stemUp = stem.VisuallyUp;

            Ruler ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            if (stemUp != GroupUp)
            {
                ruler = stemUp ?
                    ruler.OffsetY(beamThickness) :
                    ruler.OffsetY(beamThickness * -1);
            }

            double thickness = stemUp ?
                beamThickness :
                beamThickness * -1;

            XY endPoint = ruler.IntersectVerticalRay(stem.End);

            XY startPoint = endPoint.Move(length * -1, ruler.Angle);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }

        public static DrawableTrapezoid AsHookStart(VisualStem stem, double length, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool groupUp, ColorARGB color)
        {
            bool stemUp = stem.VisuallyUp;

            Ruler ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            if (stemUp != groupUp)
            {
                ruler = stemUp ?
                    ruler.OffsetY(beamThickness) :
                    ruler.OffsetY(beamThickness * -1);
            }

            double thickness = stemUp ?
                beamThickness :
                beamThickness * -1;

            XY startPoint = ruler.IntersectVerticalRay(stem.End);

            XY endPoint = startPoint.Move(length, ruler.Angle);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }
    }
}
