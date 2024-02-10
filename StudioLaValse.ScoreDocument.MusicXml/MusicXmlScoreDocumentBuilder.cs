using StudioLaValse.ScoreDocument.MusicXml.Private;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml
{
    /// <summary>
    /// An implementation of the <see cref="BaseScoreBuilder"/> that creates a score document from a music xml file.
    /// </summary>
    public class MusicXmlScoreDocumentBuilder : BaseScoreBuilder
    {
        private MusicXmlScoreDocumentBuilder(BaseScoreBuilder baseScoreBuilder) : base(baseScoreBuilder)
        {
            
        }

        /// <summary>
        /// Create the score document from the music xml file.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
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
