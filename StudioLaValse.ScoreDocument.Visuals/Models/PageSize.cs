namespace StudioLaValse.ScoreDocument.Drawable.Models
{
    public struct PageSize
    {
        public static PageSize A0 => new PageSize()
        {
            Width = 841,
            Height = 1189
        };
        public static PageSize A1 => new PageSize()
        {
            Width = 594,
            Height = 841
        };
        public static PageSize A2 => new PageSize()
        {
            Width = 420,
            Height = 594
        };
        public static PageSize A3 => new PageSize()
        {
            Width = 297,
            Height = 420
        };
        public static PageSize A4 => new PageSize()
        {
            Width = 210,
            Height = 297
        };
        public static PageSize A5 => new PageSize()
        {
            Width = 148,
            Height = 210
        };
        public static PageSize Custom(int width, int height)
        {
            return new PageSize()
            {
                Width = width,
                Height = height
            };
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}
