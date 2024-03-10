using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal class ScoreDocumentXmlConverter
    {
        private readonly ScorePartXmlConverter scorePartXmlConverter;

        public ScoreDocumentXmlConverter(ScorePartXmlConverter scorePartXmlConverter)
        {
            this.scorePartXmlConverter = scorePartXmlConverter;
        }

        public void Create(XDocument xDocument, IScoreDocumentEditor scoreEditor)
        {
            scoreEditor.Clear();

            foreach (XElement element in xDocument.Elements())
            {
                if (element.Name == "score-partwise")
                {
                    ProcessScorePartWise(element, scoreEditor);
                    return;
                }
            }

            throw new Exception("No score-partwise found in music xml.");
        }

        private void ProcessScorePartWise(XElement scorePartwise, IScoreDocumentEditor scoreEditor)
        {
            foreach (XElement element in scorePartwise.Elements())
            {
                if (element.Name == "part-list")
                {
                    PrepareParts(element, scoreEditor);
                }
            }

            foreach (XElement element in scorePartwise.Elements())
            {
                if (element.Name == "part")
                {
                    PrepareMeasures(element, scoreEditor);
                    break;
                }
            }

            foreach (XElement element in scorePartwise.Elements())
            {
                if (element.Name == "part")
                {
                    string id = element.Attributes().Single(a => a.Name == "id").Value;
                    IInstrumentRibbonEditor ribbon = scoreEditor.ReadInstrumentRibbons().First(r => r.ReadLayout().DisplayName.Value == id);
                    scorePartXmlConverter.Create(element, ribbon);
                }
            }
        }

        private void PrepareParts(XElement partList, IScoreDocumentEditor scoreEditor)
        {
            IEnumerable<XElement> partNodes = partList.Elements().Where(d => d.Name == "score-part");

            foreach (XElement? partListNode in partNodes)
            {
                string name = partListNode.Elements().Single(d => d.Name == "part-name").Value;
                if(!Instrument.TryGetFromName(name, out Instrument? instrument))
                {
                    instrument = Instrument.Piano;
                }
                scoreEditor.AddInstrumentRibbon(instrument);

                IInstrumentRibbonEditor ribbon = scoreEditor.ReadInstrumentRibbon(scoreEditor.NumberOfInstruments - 1);
                var layout = ribbon.ReadLayout();
                layout.DisplayName.Value = partListNode.Attributes().Single(a => a.Name == "id").Value;
                ribbon.ApplyLayout(layout);
            }
        }

        private void PrepareMeasures(XElement part, IScoreDocumentEditor scoreEditor)
        {
            int lastKeySignature = 0;
            int lastBeats = 4;
            int lastBeatsType = 4;

            IEnumerable<XElement> measures = part.Elements().Where(e => e.Name == "measure");
            foreach (XElement? measure in measures)
            {
                lastKeySignature = part.Descendants().FirstOrDefault(d => d.Name == "fifths")?.Value.ToIntOrNull() ?? lastKeySignature;
                lastBeats = part.Descendants().FirstOrDefault(d => d.Name == "beats")?.Value.ToIntOrNull() ?? lastBeats;
                lastBeatsType = part.Descendants().FirstOrDefault(d => d.Name == "beat-type")?.Value.ToIntOrNull() ?? lastBeatsType;

                bool newSystem = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-system")?.Value.Equals("yes") ?? false;
                bool newPage = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-page")?.Value.Equals("yes") ?? false;
                TimeSignature timeSignature = new(lastBeats, lastBeatsType);
                scoreEditor.AppendScoreMeasure(timeSignature);

                IScoreMeasureEditor appendedMeasure = scoreEditor.ReadScoreMeasure(scoreEditor.NumberOfMeasures - 1);
                KeySignature keySignature = new(Step.C.MoveAlongCircleOfFifths(lastKeySignature), MajorOrMinor.Major);
                appendedMeasure.EditKeySignature(keySignature);
            }
        }
    }
}