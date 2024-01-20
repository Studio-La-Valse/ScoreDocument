using StudioLaValse.ScoreDocument.Layout.Private;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Extensions to the <see cref="BaseScoreBuilder"/> class.
    /// </summary>
    public static class ScoreBuilderExtensions
    {
        /// <summary>
        /// Attach a layout dictionary to the specified score builder. 
        /// All read and write operations for layout elements will be written to and read from the layout dictionary. 
        /// This enables a user to use a single score document with different layout documents to get different visualizations from a single score.
        /// Note that even though this method returns a new instance, all all read and write operations are propagated back to the original instance. 
        /// This also means that if the orignal instance changes, the new implementation changes with it.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dictionary"></param>
        /// <returns>A new implementation of the score builder that reads and writes all layout operation to and from the layout dictionary.</returns>
        public static BaseScoreBuilder UseLayoutDictionary(this BaseScoreBuilder builder, IScoreLayoutDictionary dictionary)
        {
            return new ScoreBuilderWithLayoutDictionary(builder, dictionary);
        }
    }
}
