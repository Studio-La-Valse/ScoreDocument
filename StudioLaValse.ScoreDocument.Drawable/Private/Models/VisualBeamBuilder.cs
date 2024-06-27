using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal sealed class VisualBeamBuilder : IVisualBeamBuilder
    {
        private readonly IScoreDocument scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IGlyphLibrary glyphLibrary;

        public VisualBeamBuilder(IScoreDocument scoreDocumentLayout, IUnitToPixelConverter unitToPixelConverter, IGlyphLibrary glyphLibrary)
        {
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
            this.glyphLibrary = glyphLibrary;
        }


        public IChord GetLayout(VisualStem stem)
        {
            return stem.Chord;
        }


        public IEnumerable<BaseDrawableElement> Build(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamThickness, double beamSpacing, double scale, double positionSpace)
        {
            if (!stems.Any())
            {
                return [];
            }

            if(stems.Count() == 1 && !GetLayout(stems.First()).ReadBeamTypes().Any())
            {
                return [];
            }

            // Draw beams in the opposite direction of the first stem direction.
            var drawBeamCanvasUp = !stems.First().VisuallyUp;

            beamThickness *= scale;
            beamSpacing *= scale;
            beamThickness = unitToPixelConverter.UnitsToPixels(beamThickness);
            beamSpacing = unitToPixelConverter.UnitsToPixels(beamSpacing);

            return stems.Count() == 1
                ? ([AsFlag(stems.First(), scale, scoreDocumentLayout.PageForegroundColor.FromPrimitive())])
                : AsGroup(stems, beamDefinition, beamSpacing, beamThickness, drawBeamCanvasUp, scoreDocumentLayout.PageForegroundColor.FromPrimitive(), positionSpace);
        }

        public DrawableScoreGlyph AsFlag(VisualStem stem, double scale, ColorARGB color)
        {
            var flags = stem.VisuallyUp ?
                new[]
                {
                    glyphLibrary.FlagEighthUp(scale),
                    glyphLibrary.FlagSixteenthUp(scale),
                    glyphLibrary.FlagThirtySecondUp(scale),
                    glyphLibrary.FlagSixtyFourthUp(scale),
                } :
                new[]
                {
                    glyphLibrary.FlagEighthDown(scale),
                    glyphLibrary.FlagSixteenthDown(scale),
                    glyphLibrary.FlagThirtySecondDown(scale),
                    glyphLibrary.FlagSixtyFourthDown(scale),
                };

            DrawableScoreGlyph flag = null!;

            var flagIndex = 3;

            for (PowerOfTwo i = 64; i >= 8; i /= 2)
            {
                var beam = GetLayout(stem).ReadBeamType(i);
                if (beam is not null and BeamType.Flag)
                {
                    var glyph = flags[flagIndex];

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

        public IEnumerable<DrawableBeam> AsGroup(IEnumerable<VisualStem> stems, Ruler beamDefinition, double beamSpacing, double beamThickness, bool crossGroup, ColorARGB color, double positionSpace)
        {
            List<DrawableBeam> beams = [];

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
                            beams.Add(AsHookStart(stem, beamIndex, beamDefinition, beamSpacing, beamThickness, crossGroup, color, positionSpace));
                            continue;
                        }

                        if (beamType == BeamType.HookEnd)
                        {
                            beams.Add(AsHookEnd(stem, beamIndex, beamDefinition, beamSpacing, beamThickness, crossGroup, color, positionSpace));
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

        public static DrawableBeam BetweenTwoStems(VisualStem left, VisualStem right, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool drawBeamCanvasUp, ColorARGB color)
        {
            var stemUp = left.VisuallyUp;

            // Always move the ruler agains the direction of the stem.
            // If the stem goes up (from the note), move the ruler downwards.
            // Starts at the tip, moves towards the note.
            var ruler = stemUp ?
                // Move the ruler downwards. Note the inverted y coordinates of the canvas. Y = 0 is top of canvas, etc.
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                // Move the ruler upwards.
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            // Unless... When we draw betweeen two stems and it is unclear which direction to choose.
            // Then choose the direction of the group as a whole.
            // Note that the direction of the beam thickness never changes.
            if (left.VisuallyUp != right.VisuallyUp)
            {
                ruler = drawBeamCanvasUp ?
                    // Move the ruler upwards. Note the inverted y coordinates of the canvas. Y = 0 is top of canvas, etc.
                    beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness * -1)) :
                    // Move the ruler downwards.
                    beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness));
            }

            var startPoint = ruler.IntersectVerticalRay(left.End);
            var endPoint = ruler.IntersectVerticalRay(right.End);

            return new DrawableBeam(startPoint, endPoint, beamThickness, drawBeamCanvasUp, color);
        }

        public DrawableBeam AsHookEnd(VisualStem stem, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool drawBeamCanvasUp, ColorARGB color, double positionSpace)
        {
            var stemUp = stem.VisuallyUp;

            // Always move the ruler against the direction of the stem.
            // Hooks are not affected by cross beams. Beams are always drawn in the direction of the group.
            // The ruler is moved agains the direction of the stem.
            var ruler = stemUp ?
                // Move the ruler downwards. Note the inverted y coordinates of the canvas. Y = 0 is top of canvas, etc.
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                // Move the ruler upwards.
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            var endPoint = ruler.IntersectVerticalRay(stem.End);
            
            var length = positionSpace /2 ;
            var startPoint = endPoint.Move(length * -1, ruler.Angle.ToRadians());

            return new DrawableBeam(startPoint, endPoint, beamThickness, drawBeamCanvasUp, color);
        }

        public DrawableBeam AsHookStart(VisualStem stem, int beamIndex, Ruler beamDefinition, double beamSpacing, double beamThickness, bool drawBeamCanvasUp, ColorARGB color, double positionSpace)
        {
            var stemUp = stem.VisuallyUp;

            // always move the ruler against the direction of the stem.
            // Hooks are not affected by cross beams. Beams are always drawn in the direction of the group.
            // The ruler is moved agains the direction of the stem.
            var ruler = stemUp ?
                // Move the ruler downwards. Note the inverted y coordinates of the canvas. Y = 0 is top of canvas, etc.
                beamDefinition.OffsetY(beamIndex * (beamSpacing + beamThickness)) :
                // Move the ruler upwards.
                beamDefinition.OffsetY(beamIndex * ((beamSpacing + beamThickness) * -1));

            var startPoint = ruler.IntersectVerticalRay(stem.End);

            var length = positionSpace /2;
            var endPoint = startPoint.Move(length, ruler.Angle.ToRadians());

            return new DrawableBeam(startPoint, endPoint, beamThickness, drawBeamCanvasUp, color);
        }
    }
}
