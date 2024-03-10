using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a layout element for a chord.
    /// </summary>
    public class ChordLayout : ILayoutElement<ChordLayout>
    {
        private readonly ChordStyleTemplate styleTemplate;

        /// <summary>
        /// The total x offset for the chord.
        /// </summary>
        public double XOffset { get; set; }


        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="styleTemplate"></param>
        public ChordLayout(ChordStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;

            XOffset = 0;
        }



        /// <inheritdoc/>
        public ChordLayout Copy()
        {
            var copy = new ChordLayout(styleTemplate)
            {
                XOffset = XOffset
            };
            return copy;
        }
    }
}