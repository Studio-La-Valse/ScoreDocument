using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using StudioLaValse.ScoreDocument.Reader;
using System.Text.RegularExpressions;

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


        public IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamThickness, double beamSpacing, double scale, ColorARGB color)
        {
            if (!stems.Any() || !stems.First().Chord.ReadBeamTypes().Any())
            {
                return [];
            }

            var crossGroup = stems.Any(s => s.VisuallyUp) && stems.Any(s => !s.VisuallyUp);

            var _beamThickness = beamThickness * scale;
            var _beamSpacing = beamSpacing * scale;

            return stems.Count() == 1
                ? ([AsFlag(stems.First(), scale, color)])
                : AsGroup(stems, beamDefinition, _beamSpacing, _beamThickness, crossGroup, color);
        }

        public DrawableScoreGlyph AsFlag(VisualStem stem, double scale, ColorARGB color)
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

            var flagIndex = 3;

            for (PowerOfTwo i = 64; i >= 8; i /= 2)
            {
                var beam = GetLayout(stem).ReadBeamType(i);
                if (beam is not null and BeamType.Flag)
                {
                    var glyph = flags[flagIndex];

                    glyph.Scale = scale;

                    flag = new DrawableScoreGlyph(
                        stem.Origin.X - stem.Thickness / 2,
                        stem.End.Y,
                        glyph,
                        HorizontalTextOrigin.Left,
                        VerticalTextOrigin.Center,
                        color);
                    break;
                }
                flagIndex--;
            }

            return flag is null ? throw new UnreachableException("Invalid beaming information.") : flag;
        }

        public IEnumerable<DrawableTrapezoid> AsGroup(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamSpacing, double beamThickness, bool crossGroup, ColorARGB color)
        {
            List<DrawableTrapezoid> beams = [];

            for (PowerOfTwo i = 8; i <= 128; i = i.Double())
            {
                if (!PowerOfTwo.TryCreate(i, out var powerOfTwo))
                {
                    throw new UnreachableException();
                }

                var beamIndex = powerOfTwo.Power - 3;


                VisualStem? openingStem = null;

                foreach (var stem in stems)
                {
                    var beamType = GetLayout(stem).ReadBeamType(i);
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
                            beams.Add(AsHookStart(stem, 1, beamIndex, beamDefinition, beamSpacing, beamThickness, crossGroup, color));
                            continue;
                        }

                        if (beamType == BeamType.HookEnd)
                        {
                            beams.Add(AsHookEnd(stem, 1, beamIndex, beamDefinition, beamSpacing, beamThickness, crossGroup, color));
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
                        beams.Add(BetweenTwoStems(openingStem, stem, beamIndex, beamDefinition, beamSpacing, beamThickness, crossGroup, color));
                        openingStem = null;
                        continue;
                    }

                    throw new Exception($"Invalid beaming information: beam has started but stem does not continue or end beam. Stem beamtype: {beamType}");
                }
            }

            return beams;
        }

        public static DrawableTrapezoid BetweenTwoStems(VisualStem left, VisualStem right, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool crossGroup, ColorARGB color)
        {
            var stemUp = left.VisuallyUp;
            var ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            var thickness = stemUp ?
                beamThickness :
                beamThickness * -1;

            var startPoint = ruler.IntersectVerticalRay(left.End);

            var endPoint = ruler.IntersectVerticalRay(right.End);

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }

        public static DrawableTrapezoid AsHookEnd(VisualStem stem, double length, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool crossGroup, ColorARGB color)
        {
            var stemUp = stem.VisuallyUp;

            var ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            if (crossGroup)
            {
                ruler = stemUp ?
                    ruler.OffsetY(beamThickness * -1) :
                    ruler.OffsetY(beamThickness);
            }

            var thickness = stemUp ?
                beamThickness :
                beamThickness * -1;

            var endPoint = ruler.IntersectVerticalRay(stem.End);

            var startPoint = endPoint.Move(length * -1, ruler.Angle.ToRadians());

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }

        public static DrawableTrapezoid AsHookStart(VisualStem stem, double length, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool crossGroup, ColorARGB color)
        {
            var stemUp = stem.VisuallyUp;

            var ruler = stemUp ?
                //draw beams downwards
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                //draw beams upwards
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            if (crossGroup)
            {
                ruler = stemUp ?
                    ruler.OffsetY(beamThickness * -1) :
                    ruler.OffsetY(beamThickness);
            }

            var thickness = stemUp ?
                beamThickness :
                beamThickness * -1;

            var startPoint = ruler.IntersectVerticalRay(stem.End);

            var endPoint = startPoint.Move(length, ruler.Angle.ToRadians());

            return new DrawableTrapezoid(startPoint, endPoint, thickness, color);
        }
    }
}
