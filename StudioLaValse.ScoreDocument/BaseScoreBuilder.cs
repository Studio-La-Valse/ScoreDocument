namespace StudioLaValse.ScoreDocument
{
    public abstract class BaseScoreBuilder
    {
        private readonly IScoreDocumentEditor scoreDocumentCore;
        private readonly IScoreDocumentReader scoreDocumentReader;

        internal BaseScoreBuilder(IScoreDocumentEditor scoreDocumentCore, IScoreDocumentReader scoreDocumentReader)
        {
            this.scoreDocumentCore = scoreDocumentCore;
            this.scoreDocumentReader = scoreDocumentReader;
        }

        public BaseScoreBuilder(BaseScoreBuilder baseScoreBuilder) : this(baseScoreBuilder.scoreDocumentCore, baseScoreBuilder.scoreDocumentReader)
        {

        }


        public virtual BaseScoreBuilder Edit(Action<IScoreDocumentEditor> action)
        {
            Edit(action, scoreDocumentCore);
            return this;
        }

        protected virtual void Edit(Action<IScoreDocumentEditor> action, IScoreDocumentEditor scoreDocumentEditor)
        {
            action.Invoke(scoreDocumentEditor);
        }

        public virtual IScoreDocumentReader Build()
        {
            return scoreDocumentReader;
        }
    }
}
