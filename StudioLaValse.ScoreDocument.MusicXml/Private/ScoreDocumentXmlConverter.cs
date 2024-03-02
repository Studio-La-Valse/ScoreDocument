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

        public void Create(XDocument xDocument, IScoreDocumentEditor scoreEditor, IScoreLayoutBuilder scoreLayoutBuilder)
        {
            scoreEditor.Clear();

            foreach (var element in xDocument.Elements())
            {
                if (element.Name == "score-partwise")
                {
                    ProcessScorePartWise(element, scoreEditor, scoreLayoutBuilder);
                    return;
                }
            }

            throw new Exception("No score-partwise found in music xml.");
        }

        private void ProcessScorePartWise(XElement scorePartwise, IScoreDocumentEditor scoreEditor, IScoreLayoutBuilder scoreLayoutBuilder)
        {
            foreach (var element in scorePartwise.Elements())
            {
                if (element.Name == "part-list")
                {
                    PrepareParts(element, scoreEditor, scoreLayoutBuilder);
                }
            }

            foreach (var element in scorePartwise.Elements())
            {
                if (element.Name == "part")
                {
                    PrepareMeasures(element, scoreEditor, scoreLayoutBuilder);
                    break;
                }
            }

            foreach (var element in scorePartwise.Elements())
            {
                if (element.Name == "part")
                {
                    var id = element.Attributes().Single(a => a.Name == "id").Value;
                    var ribbon = scoreEditor.ReadInstrumentRibbons().First(r => scoreLayoutBuilder.InstrumentRibbonLayout(r).DisplayName == id);
                    scorePartXmlConverter.Create(element, ribbon, scoreLayoutBuilder);
                }
            }
        }

        private void PrepareParts(XElement partList, IScoreDocumentEditor scoreEditor, IScoreLayoutBuilder scoreLayoutBuilder)
        {
            var partNodes = partList.Elements().Where(d => d.Name == "score-part");

            foreach (var partListNode in partNodes)
            {
                var name = partListNode.Elements().Single(d => d.Name == "part-name").Value;
                var instrument = Instrument.TryGetFromName(name);
                scoreEditor.AddInstrumentRibbon(instrument);

                var ribbon = scoreEditor.ReadInstrumentRibbon(scoreEditor.NumberOfInstruments - 1);
                var layout = new InstrumentRibbonLayout(ribbon.Instrument)
                {
                    DisplayName = partListNode.Attributes().Single(a => a.Name == "id").Value
                };
                scoreLayoutBuilder.Apply(ribbon, layout);
            }
        }

        private void PrepareMeasures(XElement part, IScoreDocumentEditor scoreEditor, IScoreLayoutBuilder scoreLayoutBuilder)
        {
            var lastKeySignature = 0;
            var lastBeats = 4;
            var lastBeatsType = 4;

            var measures = part.Elements().Where(e => e.Name == "measure");
            var n = 0;
            foreach (var measure in measures)
            {
                lastKeySignature = part.Descendants().FirstOrDefault(d => d.Name == "fifths")?.Value.ToIntOrNull() ?? lastKeySignature;
                lastBeats = part.Descendants().FirstOrDefault(d => d.Name == "beats")?.Value.ToIntOrNull() ?? lastBeats;
                lastBeatsType = part.Descendants().FirstOrDefault(d => d.Name == "beat-type")?.Value.ToIntOrNull() ?? lastBeatsType;

                var newSystem = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-system")?.Value.Equals("yes") ?? false;
                var newPage = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-page")?.Value.Equals("yes") ?? false;
                var timeSignature = new TimeSignature(lastBeats, lastBeatsType);
                scoreEditor.AppendScoreMeasure(timeSignature);

                var appendedMeasure = scoreEditor.ReadScoreMeasure(scoreEditor.NumberOfMeasures - 1);
                var keySignature = new KeySignature(Step.C.MoveAlongCircleOfFifths(lastKeySignature), MajorOrMinor.Major);
                appendedMeasure.EditKeySignature(keySignature);

                var width = measure.Attribute("width")?.Value.ToIntOrNull();
                var measureLayout = new ScoreMeasureLayout()
                {
                    Width = width ?? 100,
                };
                scoreLayoutBuilder.Apply(appendedMeasure, measureLayout);

                n++;
            }
        }
    }
}