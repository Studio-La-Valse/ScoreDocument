using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private
{
    internal class ScoreBuilderWithLayoutDictionary : BaseScoreBuilder
    {
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ScoreBuilderWithLayoutDictionary(BaseScoreBuilder builder, IScoreLayoutDictionary layoutDictionary) : base(builder)
        {
            this.layoutDictionary = layoutDictionary;
        }

        protected override void Edit(Action<IScoreDocumentEditor> action, IScoreDocumentEditor scoreDocumentEditor)
        {
            action.Invoke(scoreDocumentEditor.UseLayout(layoutDictionary));
        }

        public override IScoreDocumentReader Build()
        {
            return base.Build();
        }
    }
}
