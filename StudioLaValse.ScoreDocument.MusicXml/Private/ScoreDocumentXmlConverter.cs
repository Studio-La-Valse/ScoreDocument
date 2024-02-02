﻿using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
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
                    var ribbon = scoreEditor.EditInstrumentRibbons().First(r => r.ReadLayout().AbbreviatedName == id);
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
                var instrument = Instrument.TryGetFromName(name);
                scoreEditor.AddInstrumentRibbon(instrument);

                var ribbon = scoreEditor.EditInstrumentRibbon(scoreEditor.NumberOfInstruments - 1);
                var layout = ribbon.ReadLayout();
                ribbon.ApplyLayout(new InstrumentRibbonLayout(name, partListNode.Attributes().Single(a => a.Name == "id").Value, layout.NumberOfStaves));
            }
        }

        private void PrepareMeasures(XElement part, IScoreDocumentEditor scoreEditor)
        {
            var lastKeySignature = 0;
            var lastBeats = 4;
            var lastBeatsType = 4;

            var measures = part.Elements().Where(e => e.Name == "measure");
            var n = 0;
            foreach(var measure in measures)
            {
                lastKeySignature = part.Descendants().FirstOrDefault(d => d.Name == "fifths")?.Value.ToIntOrNull() ?? lastKeySignature;
                lastBeats = part.Descendants().FirstOrDefault(d => d.Name == "beats")?.Value.ToIntOrNull() ?? lastBeats;
                lastBeatsType = part.Descendants().FirstOrDefault(d => d.Name == "beat-type")?.Value.ToIntOrNull() ?? lastBeatsType;

                var newSystem = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-system")?.Value.Equals("yes") ?? false;
                var newPage = part.Descendants().FirstOrDefault(d => d.Name == "print")?.Attribute("new-page")?.Value.Equals("yes") ?? false;
                var timeSignature = new TimeSignature(lastBeats, lastBeatsType);
                scoreEditor.AppendScoreMeasure(timeSignature);

                var appendedMeasure = scoreEditor.EditScoreMeasure(scoreEditor.NumberOfMeasures - 1);
                var keySignature = new KeySignature(Step.C.MoveAlongCircleOfFifths(lastKeySignature), MajorOrMinor.Major);
                var width = measure.Attribute("width")?.Value.ToIntOrNull();
                var layout = new ScoreMeasureLayout(
                    keySignature: keySignature,
                    isNewSystem: newSystem || n % 4 == 0,
                    width: width ?? 100
                );
                appendedMeasure.ApplyLayout(layout);

                n++;
            }
        }
    }
}