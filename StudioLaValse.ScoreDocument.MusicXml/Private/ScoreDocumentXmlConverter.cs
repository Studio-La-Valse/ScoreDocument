using StudioLaValse.ScoreDocument.Layout;
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

            foreach (var element in xDocument.Elements())
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
            foreach (var element in scorePartwise.Elements())
            {
                if (element.Name == "part-list")
                {
                    PrepareParts(element, scoreEditor);
                }
            }

            foreach (var element in scorePartwise.Elements())
            {
                if (element.Name == "part")
                {
                    PrepareMeasures(element, scoreEditor);
                    break;
                }
            }

            foreach (var element in scorePartwise.Elements())
            {
                if (element.Name == "part")
                {
                    var id = element.Attributes().Single(a => a.Name == "id").Value;
                    var ribbon = scoreEditor.ReadInstrumentRibbons().First(r => r.ReadLayout().DisplayName == id);
                    scorePartXmlConverter.Create(element, ribbon);
                }
            }
        }

        private void PrepareParts(XElement partList, IScoreDocumentEditor scoreEditor)
        {
            var partNodes = partList.Elements().Where(d => d.Name == "score-part");

            foreach (var partListNode in partNodes)
            {
                var name = partListNode.Elements().Single(d => d.Name == "part-name").Value;
                if (!Instrument.TryGetFromName(name, out var instrument))
                {
                    instrument = Instrument.Piano;
                }
                scoreEditor.AddInstrumentRibbon(instrument);

                var ribbon = scoreEditor.ReadInstrumentRibbon(scoreEditor.NumberOfInstruments - 1);
                ribbon.SetDisplayName(partListNode.Attributes().Single(a => a.Name == "id").Value);
            }
        }

        private void PrepareMeasures(XElement part, IScoreDocumentEditor scoreEditor)
        {
            var lastKeySignature = 0;
            var lastBeats = 4;
            var lastBeatsType = 4;

            var measures = part.Elements().Where(e => e.Name == "measure");
            foreach (var measure in measures)
            {
                lastKeySignature = part.Descendants().FirstOrDefault(d => d.Name == "fifths")?.Value.ToIntOrNull() ?? lastKeySignature;
                lastBeats = part.Descendants().FirstOrDefault(d => d.Name == "beats")?.Value.ToIntOrNull() ?? lastBeats;
                lastBeatsType = part.Descendants().FirstOrDefault(d => d.Name == "beat-type")?.Value.ToIntOrNull() ?? lastBeatsType;

                var newSystem = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-system")?.Value.Equals("yes") ?? false;
                var newPage = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-page")?.Value.Equals("yes") ?? false;
                TimeSignature timeSignature = new(lastBeats, lastBeatsType);
                scoreEditor.AppendScoreMeasure(timeSignature);

                var appendedMeasure = scoreEditor.ReadScoreMeasure(scoreEditor.NumberOfMeasures - 1);
                KeySignature keySignature = new(Step.C.MoveAlongCircleOfFifths(lastKeySignature), MajorOrMinor.Major);
                appendedMeasure.SetKeySignature(keySignature);
            }
        }
    }
}