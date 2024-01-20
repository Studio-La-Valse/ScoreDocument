namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base class for all score builder implementations.
    /// </summary>
    public abstract class BaseScoreBuilder
    {
        private readonly IScoreDocumentEditor scoreDocumentCore;
        private readonly IScoreDocumentReader scoreDocumentReader;

        internal BaseScoreBuilder(IScoreDocumentEditor scoreDocumentCore, IScoreDocumentReader scoreDocumentReader)
        {
            this.scoreDocumentCore = scoreDocumentCore;
            this.scoreDocumentReader = scoreDocumentReader;
        }

        /// <summary>
        /// Constructs a score builder as an extension of another.
        /// </summary>
        /// <param name="baseScoreBuilder"></param>
        public BaseScoreBuilder(BaseScoreBuilder baseScoreBuilder) : this(baseScoreBuilder.scoreDocumentCore, baseScoreBuilder.scoreDocumentReader)
        {

        }

        /// <summary>
        /// Specify how to edit the score document. 
        /// Note that by default, the action will be executed immediately. Different implementations may have different behaviors.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual BaseScoreBuilder Edit(Action<IScoreDocumentEditor> action)
        {
            Edit(action, scoreDocumentCore);
            return this;
        }

        /// <summary>
        /// Specify how the specified action should be executed. Override this method to implement any overrides on the provided score document editor.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="scoreDocumentEditor"></param>
        protected virtual void Edit(Action<IScoreDocumentEditor> action, IScoreDocumentEditor scoreDocumentEditor)
        {
            action.Invoke(scoreDocumentEditor);
        }

        /// <summary>
        /// Builds the score document
        /// </summary>
        /// <returns>A <see cref="IScoreMeasureReader"/> that was built by the action.</returns>
        public virtual IScoreDocumentReader Build()
        {
            return scoreDocumentReader;
        }
    }
}
