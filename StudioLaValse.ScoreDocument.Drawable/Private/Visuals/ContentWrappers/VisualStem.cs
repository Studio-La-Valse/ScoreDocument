﻿namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualStem : BaseContentWrapper
    {
        private static readonly double thickness = 0.08;
        private readonly ColorARGB color;

        public bool VisuallyUp
        {
            get
            {
                return End.Y < Origin.Y;
            }
        }

        public XY Origin { get; }
        public XY End { get; }
        public Dictionary<int, BeamType> Beams { get; }




        public VisualStem(XY origin, XY end, Dictionary<int, BeamType> beams, ColorARGB color)
        {
            this.color = color;
            Origin = origin;
            End = end;
            Beams = beams;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var list = new List<BaseDrawableElement>()
            {
                new DrawableLine(Origin, End, color: color, thickness: thickness),
            };

            return list;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return new List<BaseContentWrapper>();
        }
    }
}
