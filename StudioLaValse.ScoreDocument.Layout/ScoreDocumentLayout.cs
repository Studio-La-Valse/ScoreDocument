using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a score document.
    /// </summary>
    public class ScoreDocumentLayout
    {
        /// <inheritdoc/>
        public string Title { get; set; }
        /// <inheritdoc/>
        public string SubTitle { get; set; }
        /// <inheritdoc/>
        public Func<IEnumerable<IScoreMeasure>, bool> BreakSystem { get; set; }
        /// <inheritdoc/>
        public PageSize PageSize { get; set; }



        /// <summary>
        /// Create a new layout.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="breakSystem"></param>
        public ScoreDocumentLayout(string title = "", string subTitle = "", Func<IEnumerable<IScoreMeasure>, bool>? breakSystem = null, PageSize? pageSize = null)
        {
            Title = title;
            SubTitle = subTitle;
            BreakSystem = breakSystem ??= e => false;
            PageSize = pageSize ?? PageSize.A4;
        }



        /// <inheritdoc/>
        public ScoreDocumentLayout Copy()
        {
            return new ScoreDocumentLayout(Title, SubTitle, BreakSystem);
        }
    }
}