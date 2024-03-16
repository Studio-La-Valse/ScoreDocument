using StudioLaValse.ScoreDocument.Layout;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal class ScorePartMeasureXmlConverter
    {
        private readonly BlockChainXmlConverter blockChainXmlConverter;

        public ScorePartMeasureXmlConverter(BlockChainXmlConverter blockChainXmlConverter)
        {
            this.blockChainXmlConverter = blockChainXmlConverter;
        }

        public void Create(XElement measure, IInstrumentMeasureEditor measureEditor, IScoreDocumentLayout pageViewLayout, ref int durationOfOneQuarter)
        {
            measureEditor.Clear();

            XElement? attributes = measure.Elements().FirstOrDefault(e => e.Name == "attributes");
            if (attributes is not null)
            {
                ProcessMeasureAttributes(attributes, ref durationOfOneQuarter, out IEnumerable<ClefChange>? clefChanges);
                InstrumentMeasureLayout layout = pageViewLayout.InstrumentMeasureLayout(measureEditor);
                foreach (ClefChange clefChange in clefChanges)
                {
                    layout.AddClefChange(clefChange);
                }
                measureEditor.Apply(layout);
            }

            IEnumerable<int> voices = measure.ExtractVoices();
            foreach (int voice in voices)
            {
                measureEditor.AddVoice(voice);
                IMeasureBlockChainEditor blockChainEditor = measureEditor.ReadBlockChainAt(voice);
                blockChainEditor.DivideEqual(measureEditor.TimeSignature.Denominator);

                IEnumerable<XElement> elements = measure.ExtractElements(voice);
                blockChainXmlConverter.ProcessElements(elements, blockChainEditor, durationOfOneQuarter, pageViewLayout);
            }
        }

        private void ProcessMeasureAttributes(XElement measureAttributes, ref int divisions, out IEnumerable<ClefChange> clefChanges)
        {
            List<ClefChange> _clefChanges = [];

            foreach (XElement element in measureAttributes.Elements())
            {
                if (element.Name == "clef")
                {
                    string sign = element.Descendants().Single(d => d.Name == "sign").Value.ToLower();
                    Clef clef = sign switch
                    {
                        "g" => Clef.Treble,
                        "f" => Clef.Bass,
                        _ => throw new NotSupportedException("Unknown clef species found in XML document: " + sign)
                    };
                    int staff = element.Attributes().Single(a => a.Name == "number").Value.ToIntOrThrow() - 1;
                    ClefChange clefChange = new(clef, staff, new Position(0, 4));
                    _clefChanges.Add(clefChange);
                }

                if (element.Name == "divisions")
                {
                    divisions = element.Value.ToIntOrThrow();
                }
            }

            clefChanges = _clefChanges;
        }
    }
}