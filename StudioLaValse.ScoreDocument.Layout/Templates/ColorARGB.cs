namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ColorARGB : ICloneable
    {
        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public object Clone()
        {
            return new ColorARGB()
            {
                A = A,
                R = R,
                G = G,
                B = B
            };
        }
    }
}
