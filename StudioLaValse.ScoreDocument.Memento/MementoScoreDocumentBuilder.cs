namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// An implementation of the <see cref="BaseScoreBuilder"/> that create a score document from a score document memento.
    /// </summary>
    public class MementoScoreDocumentBuilder : BaseScoreBuilder
    {
        private MementoScoreDocumentBuilder(BaseScoreBuilder baseScoreBuilder) : base(baseScoreBuilder)
        {

        }

        /// <summary>
        /// Create a new instance of a <see cref="BaseScoreBuilder"/> from a score document memento.
        /// </summary>
        /// <param name="memento"></param>
        /// <returns></returns>
        public static BaseScoreBuilder Create(ScoreDocumentMemento memento)
        {
            var title = memento.Layout.Title;
            var subTitle = memento.Layout.SubTitle;
            var scoreBuilder = ScoreBuilder.CreateDefault(title, subTitle)
                .Edit(editor =>
                {
                    editor.ApplyMemento(memento);
                });

            return new MementoScoreDocumentBuilder(scoreBuilder);
        }
    }
}
