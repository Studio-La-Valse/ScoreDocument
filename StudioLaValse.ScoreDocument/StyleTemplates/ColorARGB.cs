namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A color struct.
    /// From Red, Green, Blue and Alpha channels.
    /// </summary>
    public struct ColorARGB
    {
        /// <summary>
        /// The alpha channel.
        /// Expects values from 0-255 inclusive.
        /// </summary>
        public required int A { get; init; }
        /// <summary>
        /// The red channel.
        /// Expects values from 0-255 inclusive.
        /// </summary>
        public required int R { get; init; }
        /// <summary>
        /// The green channel.
        /// Expects values from 0-255 inclusive.
        /// </summary>
        public required int G { get; init; }
        /// <summary>
        /// The blue channel.
        /// Expects values from 0-255 inclusive.
        /// </summary>
        public required int B { get; init; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        public ColorARGB()
        {

        }
    }
}
