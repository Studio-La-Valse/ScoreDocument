namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A struct representing a page size.
    /// </summary>
    public struct PageSize
    {
        /// <summary>
        /// The default a0 size.
        /// </summary>
        public static PageSize A0 => new()
        {
            Width = 841,
            Height = 1189
        };
        /// <summary>
        /// The default a1 size.
        /// </summary>
        public static PageSize A1 => new()
        {
            Width = 594,
            Height = 841
        };
        /// <summary>
        /// The default a2 size.
        /// </summary>
        public static PageSize A2 => new()
        {
            Width = 420,
            Height = 594
        };
        /// <summary>
        /// The default a3 size.
        /// </summary>
        public static PageSize A3 => new()
        {
            Width = 297,
            Height = 420
        };
        /// <summary>
        /// The default a4 size.
        /// </summary>
        public static readonly PageSize A4 = new()
        {
            Width = 210,
            Height = 297
        };
        /// <summary>
        /// The default a5 size.
        /// </summary>
        public static PageSize A5 => new()
        {
            Width = 148,
            Height = 210
        };
        /// <summary>
        /// Create a custom page size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static PageSize Custom(int width, int height)
        {
            return new PageSize()
            {
                Width = width,
                Height = height
            };
        }
        /// <summary>
        /// The width of the page.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// The height of the page.
        /// </summary>
        public int Height { get; private set; }
    }
}
