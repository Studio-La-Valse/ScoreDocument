namespace StudioLaValse.ScoreDocument
{
    public class ScoreBuilder : BaseScoreBuilder
    {
        private ScoreBuilder(ScoreDocumentCore score) : base(score, score)
        {

        }

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
