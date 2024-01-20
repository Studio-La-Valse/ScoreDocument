namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The default implementation of the score builder.
    /// </summary>
    public class ScoreBuilder : BaseScoreBuilder
    {
        private ScoreBuilder(ScoreDocumentCore score) : base(score, score)
        {

        }

        /// <summary>
        /// Creates the default implementation of the scorebuilder by providing a title and subtitle.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <returns></returns>
        public static BaseScoreBuilder CreateDefault(string title, string subtitle)
        {
            var keyGenerator = new IncrementalIntGeneratorFactory().CreateKeyGenerator();
            var contentTable = new ScoreContentTable(keyGenerator);
            var layout = new ScoreDocumentLayout(title, subtitle);
            var score = new ScoreDocumentCore(contentTable, layout, keyGenerator);
            return new ScoreBuilder(score);
        }
    }
}
