using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal class MeasureBlock
    {
        public List<Chord> Chords { get; } = [];
    }

    internal class Chord
    {
        public List<XElement> Notes { get; } = [];

        public RythmicDuration? RythmicDuration { get; set; }

        public GraceBlock? Grace { get; set; }
    }

    internal class GraceBlock
    {
        public List<Chord> Chords { get; } = [];
    }

    internal class BlockChainXmlConverter
    {
        public BlockChainXmlConverter()
        {

        }

        internal static readonly string[] beamTypesThatIndicateAGroupIsNotClosedYet = ["hook start", "continue", "begin"];

        public void ProcessElements(IEnumerable<XElement> elements, IMeasureBlockChainEditor editor, int divisionsOfOneQuarter)
        {
            editor.Clear();

            var blocks = CreateBlocks(elements, divisionsOfOneQuarter);

            var blockSizes = blocks.Select(block =>
            {
                var chordDurations = block.Chords.Select(chord =>
                {
                    if (chord.RythmicDuration is not null)
                    {
                        return chord.RythmicDuration;
                    }
                    throw new UnreachableException();
                });
                var sum = chordDurations.Sum();
                if (!RythmicDuration.TryConstruct(sum, out var rythmicDuration))
                {
                    throw new Exception("A valid rythmic duration is required for a note or measure element with a duration, but could not be found.");
                }
                return rythmicDuration;
            }).ToArray();

            editor.Divide(blockSizes);

            foreach(var (blockEditor, block) in editor.ReadBlocks().Zip(blocks))
            {
                FillBlock(block, blockEditor);
            }
        }

        private IEnumerable<MeasureBlock> CreateBlocks(IEnumerable<XElement> elements, int divisionsOfOneQuarter)
        {
            if (!elements.Any())
            {
                return [];
            }

            var blocks = new List<MeasureBlock>();

            GraceBlock? pendingGrace = null;
            MeasureBlock? pendingBlock = null;
            var notesOrForwards = elements.Where(e => e.IsNoteOrForward());
            var makeNewBlock = true;
            foreach (var element in notesOrForwards)
            {
                GetRythmicInformationFromNode(element, divisionsOfOneQuarter, out var actualDuration, out var displayDuration, out var grace);

                if (element.IsGrace())
                {
                    pendingGrace ??= new GraceBlock();

                    if (!element.IsChord())
                    {
                        pendingGrace.Chords.Add(new Chord());
                    }
                    var targetGrace = pendingGrace.Chords.Last();
                    targetGrace.Notes.Add(element);
                    continue;
                }

                if (element.IsChord())
                {
                    if(pendingBlock is null || pendingBlock.Chords.Count == 0)
                    {
                        throw new UnreachableException();
                    }
                }
                else
                {
                    if (pendingBlock is null || makeNewBlock)
                    {
                        pendingBlock = new MeasureBlock();
                        blocks.Add(pendingBlock);
                    }

                    var newChord = new Chord();
                    if (pendingGrace is not null)
                    {
                        newChord.Grace = pendingGrace;
                        pendingGrace = null;
                    }
                    pendingBlock.Chords.Add(newChord);
                }

                var chord = pendingBlock.Chords.Last();
                chord.RythmicDuration = displayDuration;

                if (element.IsNoteOrRest() && element.TryParsePitch(out _))
                {
                    chord!.Notes.Add(element);

                    var beams = element.GetBeams();
                    makeNewBlock = true;
                    foreach (var beam in beams)
                    {
                        if (beamTypesThatIndicateAGroupIsNotClosedYet.Contains(beam))
                        {
                            makeNewBlock = false;
                            break;
                        }
                    }
                }
            }

            return blocks;
        }

        private void FillBlock(MeasureBlock block, IMeasureBlockEditor measureBlock)
        {
            foreach(var chord in block.Chords)
            {
                var duration = chord.RythmicDuration ?? throw new UnreachableException();
                measureBlock.AppendChord(duration);
                var addedChord = measureBlock.ReadChords().Last();
                foreach(var note in chord.Notes)
                {
                    var staffIndex = note.StaffIndex() ?? 0;
                    addedChord.Add(note.ParsePitch());
                    var addedNote = addedChord.ReadNotes().Last();
                    addedNote.SetStaffIndex(staffIndex);
                }
                FillGrace(chord.Grace, addedChord);
            }
        }
        
        private void FillGrace(GraceBlock? chord, IGraceableEditor<IGraceGroupEditor> targetChord)
        {
            if(chord is null)
            {
                return;
            }

            targetChord.Grace();
            var addedGrace = targetChord.ReadGraceGroup() ?? throw new UnreachableException();
            foreach (var graceChord in chord.Chords)
            {
                addedGrace.AppendChord();
                var addedGraceChord = addedGrace.ReadChords().Last();
                foreach (var note in graceChord.Notes)
                {
                    var staffIndex = note.StaffIndex() ?? 0;
                    addedGraceChord.Add(note.ParsePitch());
                    var addedNote = addedGraceChord.ReadNotes().Last();
                    addedNote.SetStaffIndex(staffIndex);
                }
                
                FillGrace(graceChord.Grace, addedGraceChord);
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

                if(RythmicDuration.TryConstruct(actualDuration, out var rythmicDuration))
                {
                    displayDuration = rythmicDuration;
                }

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
    }
}