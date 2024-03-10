using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a score document.
    /// </summary>
    public class ScoreDocumentLayout : ILayoutElement<ScoreDocumentLayout>
    {
        private readonly ScoreDocumentStyleTemplate styleTemplate;


        public ScoreDocumentLayout(ScoreDocumentStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
        }



        /// <inheritdoc/>
        public ScoreDocumentLayout Copy()
        {
            var copy = new ScoreDocumentLayout(styleTemplate);
            return copy;
        }
    }
}