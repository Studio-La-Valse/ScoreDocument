﻿using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Drawable.Text;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Models;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.VisualParents
{
    public sealed class VisualRest : BaseVisualNote
    {
        public Glyph GlyphPrototype
        {
            get
            {
                var duration = 1M;

                var glyphs = new[]
                {
                    GlyphLibrary.RestWhole,
                    GlyphLibrary.RestHalf,
                    GlyphLibrary.RestQuarter,
                    GlyphLibrary.RestEighth,
                    GlyphLibrary.RestSixteenth,
                    GlyphLibrary.RestThirtySecond,
                };

                for (int i = 0; i < 6; i++)
                {
                    if (DisplayDuration.Decimal >= duration)
                        return glyphs[i];

                    duration /= 2;
                }

                return GlyphLibrary.RestEighth;
            }
        }
        public override DrawableScoreGlyph Glyph
        {
            get
            {
                var glyph = GlyphPrototype;

                if (measureElement.Grace)
                {
                    glyph.Scale = 0.5;
                }

                return new DrawableScoreGlyph(
                    XPosition,
                    HeightOnCanvas,
                    glyph,
                    HorizontalTextOrigin.Center,
                    VerticalTextOrigin.Center,
                    DisplayColor);
            }
        }
        public override int Line =>
            4;


        public VisualRest(IChord note, double canvasLeft, double canvasTop, double scale, ColorARGB color, ISelection<IUniqueScoreElement> selection) :
            base(note, canvasLeft, canvasTop, scale, color, selection)
        {

        }
    }
}
