using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal class BlockChainXmlConverter
    {
        public BlockChainXmlConverter()
        {

        }

        internal static readonly string[] sourceArray = ["hook start", "continue", "begin"];

        public void ProcessElements(IEnumerable<XElement> elements, IMeasureBlockChainEditor editor, int divisionsOfOneQuarter)
        {
            editor.Clear();

            var groups = CreateGroups(elements);
            foreach(var group in groups)
            {
                var rythmicDuration = GetRythmicDurationOrThrow(group, divisionsOfOneQuarter);

                editor.Append(rythmicDuration, false);
                var measureBlock = editor.ReadBlocks().Last();
                FillBlock(group, divisionsOfOneQuarter, measureBlock);
            }
        }

        private IEnumerable<IEnumerable<XElement>> CreateGroups(IEnumerable<XElement> elements)
        {
            var groups = new List<List<XElement>>();
            var makeNewGroup = true;
            foreach (var element in elements.Where(e => e.IsNoteOrForward()))
            {
                if (makeNewGroup)
                {
                    groups.Add([]);
                }

                groups.Last().Add(element);

                var beams = element.GetBeams();
                foreach (var beam in beams)
                {
                    if (sourceArray.Contains(beam))
                    {
                        makeNewGroup = false;
                    }
                }
            }
            return groups;
        }

        private RythmicDuration GetRythmicDurationOrThrow(IEnumerable<XElement> group, int divisionsOfOneQuarter)
        {
            var duration = group
                .Select(e =>
                {
                    GetRythmicInformationFromNode(e, divisionsOfOneQuarter, out var actualDuration, out var displayDuration, out var grace);
                    return actualDuration;
                })
                .Sum();

            if (!RythmicDuration.TryConstruct(duration, out var rythmicDuration))
            {
                throw new Exception("A valid rythmic duration is required for a note or measure element with a duration, but could not be found.");
            }
            return rythmicDuration;
        }

        private void FillBlock(IEnumerable<XElement> elements, int divisionsOfOneQuarter, IMeasureBlockEditor measureBlock)
        {
            foreach(var element in elements)
            {
                GetRythmicInformationFromNode(element, divisionsOfOneQuarter, out var actualDuration, out var displayDuration, out var grace);

                GetInformationFromMeasureElement(element, out var chord, out var rest, out var staff, out var forward, out var backup, out var pitch, out var stemUp);

                //create a new chord in the block if the 'chord' attribute is not specified, or if there are no chords in the block.
                if (!chord || !measureBlock.ReadChords().Any())
                {
                    // In case the display duration of the note (or rest, in case of a forward) is not available, create one from the calculated actual duration.
                    if (displayDuration is null)
                    {
                        if (!RythmicDuration.TryConstruct(actualDuration, out displayDuration))
                        {
                            throw new Exception("A valid rythmic duration is required for a note or measure element with a duration, but could not be found.");
                        }
                    }

                    measureBlock.AppendChord(displayDuration);
                }

                //Always add to the last chord in the block
                var chordDocument = measureBlock.ReadChords().Last();

                if (pitch.HasValue)
                {
                    chordDocument.Add(pitch.Value);
                    var note = chordDocument.ReadNotes().Single(n => n.Pitch.Equals(pitch));
                    note.SetStaffIndex(staff ?? 0);
                }
            }
        }


        private static void GetRythmicInformationFromNode(XElement measureElement, int durationOfOneQuarter, out Fraction actualDuration, out RythmicDuration? displayDuration, out bool grace)
        {
            displayDuration = null;
            grace = measureElement.Descendants().Any(d => d.Name == "grace");

            var typeString = measureElement.Descendants().SingleOrDefault(d => d.Name == "type")?.Value;
            Tuplet? tupletInformation = null;
            if (measureElement.Descendants().SingleOrDefault(d => d.Name == "time-modification") is XElement timeModificationElement)
            {
                //bijvoorbeeld 3, van 3 achtsten in de plaats van 1 kwart = 2 achtsten
                var actualNumberValue = timeModificationElement.Descendants().Single(d => d.Name == "actual-notes").Value;
                // bijvoorbeeld 2, van 3 achtsten in de plaats van 1 kwart = 2 achtsten
                var normalNumberValue = timeModificationElement.Descendants().Single(d => d.Name == "normal-notes").Value;
                // bijvoorbeel 8, van 3 achtsten in de plaats van 1 kwart = 2 achtsten
                var normalTypeValue = timeModificationElement.Descendants().SingleOrDefault(d => d.Name == "normal-type")?.Value;

                var normalRythmicType = normalTypeValue ?? typeString ?? throw new Exception("Cannot handle tuplet information if the type xml node does not exist.");
                var normalRythmicDuration = normalRythmicType.FromTypeString();
                var normalRythmicDots = timeModificationElement.Descendants().Where(d => d.Name == "normal-dot").Count();
                RythmicDuration normalDuration = new(normalRythmicDuration, normalRythmicDots);
                var tupletContent = Enumerable.Range(0, actualNumberValue.ToIntOrThrow())
                    .Select(e => normalDuration)
                    .ToArray();
                tupletInformation = new Tuplet(normalDuration, tupletContent);
            }

            if (typeString is null)
            {
                var _duration = measureElement.Descendants().FirstOrDefault(d => d.Name == "duration");
                var noteDuration = _duration?.Value.ToIntOrNull() ?? 0;
                actualDuration = new Duration(noteDuration, durationOfOneQuarter * 4).Simplify();
            }
            else
            {
                var type = typeString.FromTypeString();
                var dots = measureElement.Descendants().Where(d => d.Name == "dot").Count();
                displayDuration = new RythmicDuration(type, dots);
                actualDuration = displayDuration;
                if (tupletInformation is not null)
                {
                    actualDuration = tupletInformation.ToActualDuration(displayDuration);
                }
            }
        }

        private static void GetInformationFromMeasureElement(XElement measureElement,
            out bool chord, out bool rest, out int? staff, out bool forward, out bool backup, out Pitch? pitch, out bool stemUp)
        {
            pitch = null;
            chord = measureElement.Element("chord") != null;
            rest = measureElement.Element("rest") != null;
            stemUp = measureElement.Descendants().SingleOrDefault(d => d.Name == "stem")?.Value == "up";

            forward = measureElement.Name == "forward";
            backup = measureElement.Name == "backup";

            staff = measureElement.Descendants().SingleOrDefault(d => d.Name == "staff")?.Value.ToIntOrThrow() - 1;
            if (forward || backup || rest)
            {
                return;
            }

            pitch = measureElement.ParsePitch();
        }
    }
}