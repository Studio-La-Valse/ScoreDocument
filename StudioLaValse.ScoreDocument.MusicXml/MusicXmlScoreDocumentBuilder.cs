using StudioLaValse.ScoreDocument.MusicXml.Private;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml
{
    public class MusicXmlScoreDocumentBuilder : BaseScoreBuilder
    {
        private MusicXmlScoreDocumentBuilder(BaseScoreBuilder baseScoreBuilder) : base(baseScoreBuilder)
        {
            
        }

        public static BaseScoreBuilder Create(XDocument document)
        {
            var chainConverter = new BlockChainXmlConverter();
            var measureConverter = new ScorePartMeasureXmlConverter(chainConverter);
            var partConverter = new ScorePartXmlConverter(measureConverter);
            var converter = new ScoreDocumentXmlConverter(partConverter);
            var scoreBuilder = ScoreBuilder.CreateDefault(" " , " ")
                .Edit(editor =>
                {
                    converter.Create(document, editor);
                });
            return new MusicXmlScoreDocumentBuilder(scoreBuilder);
        }
    }
}
