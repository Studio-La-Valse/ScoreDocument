﻿namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStem : BaseContentWrapper
    {
        private readonly double thickness;
        private readonly IScoreDocument scoreDocumentLayout;


        public XY Origin { get; }
        public XY End { get; }
        public IChord Chord { get; }

        public double Thickness => this.thickness;
        public bool VisuallyUp => End.Y < Origin.Y;

        public VisualStem(XY origin, XY end, double thickness, IChord chord, IScoreDocument scoreDocumentLayout)
        {
            this.thickness = thickness;
            this.scoreDocumentLayout = scoreDocumentLayout;

            Origin = origin;
            End = end;
            Chord = chord;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return new DrawableLine(Origin, End, color: scoreDocumentLayout.PageForegroundColor.Value.FromPrimitive(), Thickness);
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
    }
}
