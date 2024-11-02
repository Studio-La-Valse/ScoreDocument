namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal static class DoubleExtensions
    {
        public static double ToDegrees(this double radians)
        {
            //1rad × 180/π = 57,296°
            return radians * 180 / Math.PI;
        }

        public static double ToRadians(this double degrees)
        {
            //1° × π/180 = 0,01745rad
            return degrees * Math.PI / 180;
        }
    }
}
