﻿using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStem : BaseContentWrapper
    {
        private readonly double thickness;
        private readonly ColorARGB color;

        public bool VisuallyUp => End.Y < Origin.Y;

        public XY Origin { get; }
        public XY End { get; }
        public IChordReader Chord { get; }

        public double Thickness => this.thickness;

        public VisualStem(XY origin, XY end, double thickness, IChordReader chord, ColorARGB color)
        {
            this.color = color;
            this.thickness = thickness;

            Origin = origin;
            End = end;
            Chord = chord;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return new DrawableLine(Origin, End, color: color, Thickness);
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
    }
}
