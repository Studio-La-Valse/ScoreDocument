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

        public void Create(XElement scorePart, IInstrumentRibbonEditor ribbonEditor)
        {
            int durationOfOneQuarter = 4;
            foreach (XElement element in scorePart.Elements())
            {
                if (element.Name == "measure")
                {
                    int measureIndex = element.Attributes().Single(a => a.Name == "number").Value.ToIntOrThrow() - 1;
                    IInstrumentMeasureEditor measureToEdit = ribbonEditor.ReadMeasure(measureIndex);
                    measureConverter.Create(element, measureToEdit, ref durationOfOneQuarter);
                }
            }
        }
    }
}