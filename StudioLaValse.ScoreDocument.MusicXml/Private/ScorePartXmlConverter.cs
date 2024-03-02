using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal class ScorePartXmlConverter
    {
        private readonly ScorePartMeasureXmlConverter measureConverter;

        public ScorePartXmlConverter(ScorePartMeasureXmlConverter measureConverter)
        {
            this.measureConverter = measureConverter;
        }

        public void Create(XElement scorePart, IInstrumentRibbonEditor ribbonEditor, IScoreLayoutBuilder scoreLayoutBuilder)
        {
            var durationOfOneQuarter = 4;
            foreach (var element in scorePart.Elements())
            {
                if (element.Name == "measure")
                {
                    var measureIndex = element.Attributes().Single(a => a.Name == "number").Value.ToIntOrThrow() - 1;
                    var measureToEdit = ribbonEditor.ReadMeasure(measureIndex);
                    measureConverter.Create(element, measureToEdit, scoreLayoutBuilder, ref durationOfOneQuarter);
                }
            }
        }
    }
}