using StudioLaValse.ScoreDocument.Layout.Private;

namespace StudioLaValse.ScoreDocument.Layout
{
    public static class ScoreBuilderExtensions
    {
        public static BaseScoreBuilder UseLayoutDictionary(this BaseScoreBuilder builder, IScoreLayoutDictionary dictionary)
        {
            return new ScoreBuilderWithLayoutDictionary(builder, dictionary);
        }
    }
}
