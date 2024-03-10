using StudioLaValse.ScoreDocument.Core.Primitives.Extensions;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal class BlockChainXmlConverter
    {
        public BlockChainXmlConverter()
        {

        }

        public void ProcessElements(IEnumerable<XElement> elements, IMeasureBlockChainEditor editor, int divisionsOfOneQuarter)
        {
            Position position = new(0, 4);
            foreach (XElement element in elements)
            {
                ProcessMeasureElement(element, editor, divisionsOfOneQuarter, ref position);
            }
        }

        private void ProcessMeasureElement(XElement measureElement, IMeasureBlockChainEditor measureBlockChain, int divisionsOfOneQuarter, ref Position position)
        {
            if (!measureElement.IsNoteOrForwardOrBackup())
            {
                return;
            }

            GetRythmicInformationFromNode(measureElement, divisionsOfOneQuarter, out Fraction? actualDuration, out RythmicDuration? displayDuration, out bool grace);

            GetInformationFromMeasureElement(measureElement, out bool chord, out bool rest, out int? staff, out bool forward, out bool backup, out Pitch? pitch, out bool stemUp);

            if (backup)
            {
                position -= actualDuration;
                return;
            }

            if (forward)
            {
                position += actualDuration;
                return;
            }

            if (chord && !grace)
            {
                position -= actualDuration;
            }

            Position _position = new(position.Numerator, position.Denominator);
            IMeasureBlockEditor measureBlock = measureBlockChain.ReadBlocks().First(b => b.ContainsPosition(_position));

            //create a new chord in the block if the no 'chord' attribute is specified, or if there are no chords in the block.
            if (!chord || !measureBlock.ReadChords().Any())
            {
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
            IChordEditor chordDocument = measureBlock.ReadChords().Last();

            if (pitch.HasValue)
            {
                chordDocument.Add(pitch.Value);
                INoteEditor note = chordDocument.ReadNotes().Single(n => n.Pitch.Equals(pitch));
                NoteLayout layout = note.ReadLayout();
                layout.StaffIndex = staff ?? 0;
                note.ApplyLayout(layout);
            }

            if (!grace)
            {
                position += actualDuration;
            }
        }



        private static void GetRythmicInformationFromNode(XElement measureElement, int durationOfOneQuarter, out Fraction actualDuration, out RythmicDuration? displayDuration, out bool grace)
        {
            displayDuration = null;
            grace = measureElement.Descendants().Any(d => d.Name == "grace");

            string? typeString = measureElement.Descendants().SingleOrDefault(d => d.Name == "type")?.Value;
            Tuplet? tupletInformation = null;
            if (measureElement.Descendants().SingleOrDefault(d => d.Name == "time-modification") is XElement timeModificationElement)
            {
                //bijvoorbeeld 3, van 3 achtsten in de plaats van 1 kwart = 2 achtsten
                string actualNumberValue = timeModificationElement.Descendants().Single(d => d.Name == "actual-notes").Value;
                // bijvoorbeeld 2, van 3 achtsten in de plaats van 1 kwart = 2 achtsten
                string normalNumberValue = timeModificationElement.Descendants().Single(d => d.Name == "normal-notes").Value;
                // bijvoorbeel 8, van 3 achtsten in de plaats van 1 kwart = 2 achtsten
                string? normalTypeValue = timeModificationElement.Descendants().SingleOrDefault(d => d.Name == "normal-type")?.Value;

                string normalRythmicType = normalTypeValue ?? typeString ?? throw new Exception("Cannot handle tuplet information if the type xml node does not exist.");
                PowerOfTwo normalRythmicDuration = normalRythmicType.FromTypeString();
                int normalRythmicDots = timeModificationElement.Descendants().Where(d => d.Name == "normal-dot").Count();
                RythmicDuration normalDuration = new(normalRythmicDuration, normalRythmicDots);
                RythmicDuration[] tupletContent = Enumerable.Range(0, actualNumberValue.ToIntOrThrow())
                    .Select(e => normalDuration)
                    .ToArray();
                tupletInformation = new Tuplet(normalDuration, tupletContent);
            }

            if (typeString is null)
            {
                XElement? _duration = measureElement.Descendants().FirstOrDefault(d => d.Name == "duration");
                int noteDuration = _duration?.Value.ToIntOrNull() ?? 0;
                actualDuration = new Duration(noteDuration, durationOfOneQuarter * 4).Simplify();
            }
            else
            {
                PowerOfTwo type = typeString.FromTypeString();
                int dots = measureElement.Descendants().Where(d => d.Name == "dot").Count();
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