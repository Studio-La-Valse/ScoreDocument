namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal sealed class Ruler
    {
        public XY Origin { get; }
        /// <summary>
        /// Angle in Degrees.
        /// </summary>
        public double Angle { get; }


        public Ruler(XY origin, double angle)
        {
            Origin = origin;
            Angle = angle;

            var absAngle = Math.Abs(Angle);
            if (absAngle > 0 && (absAngle % 90).IsAlmostEqualTo(0))
            {
                throw new ArgumentOutOfRangeException(nameof(angle), "The angle of a beam definition cannot be equal to 90!");
            }
        }

        public Ruler OffsetY(double offset)
        {
            return new Ruler(new XY(Origin.X, Origin.Y + offset), Angle);
        }

        public XY IntersectVerticalRay(XY point)
        {
            if (Angle.IsAlmostEqualTo(0))
            {
                return new XY(point.X, Origin.Y);
            }

            var radians = Angle.ToRadians();
            var a = Origin.X - point.X;
            var o = a * Math.Tan(radians);
            var z = Origin.Y - point.Y - o;
            XY intersection = new(point.X, point.Y + z);
            return intersection;
        }
    }
}
