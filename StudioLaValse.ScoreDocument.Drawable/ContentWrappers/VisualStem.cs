using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Layout.Models;

namespace StudioLaValse.ScoreDocument.Drawable.ContentWrappers
{
    public sealed class VisualStem : BaseContentWrapper
    {
        private static readonly double thickness = 0.08;
        private readonly ColorARGB color;

        private double Length
        {
            get
            {
                return End.Y - Origin.Y;
            }
        }
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
                new DrawableLineVertical(Origin.X, Origin.Y, Length * -1, color: color, thickness: thickness),
            };

            return list;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return new List<BaseContentWrapper>();
        }
    }
}
