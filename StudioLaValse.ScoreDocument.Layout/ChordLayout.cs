namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a layout element for a chord.
    /// </summary>
    public class ChordLayout : ILayoutElement<ChordLayout>
    {
        /// <summary>
        /// The total x offset for the chord.
        /// </summary>
        public double XOffset { get; set; }


        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="xOffset"></param>
        public ChordLayout(double xOffset = 0)
        {
            XOffset = xOffset;
        }



        /// <inheritdoc/>
        public ChordLayout Copy()
        {
            return new ChordLayout(XOffset);
        }
    }
}