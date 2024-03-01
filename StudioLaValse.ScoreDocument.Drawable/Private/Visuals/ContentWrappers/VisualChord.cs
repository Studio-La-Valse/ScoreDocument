using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualChord : BaseContentWrapper
    {
        private readonly IChordReader chord;
        private readonly double scale;
        private readonly double canvasLeft;
        private readonly double canvasTopStaffGroup;
        private readonly IStaffGroup staffGroup;
        private readonly IInstrumentMeasureReader instrumentMeasureReader;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly ColorARGB color;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;

        public ChordLayout Layout => scoreLayoutDictionary.GetOrDefault(chord);
        public double XOffset => Layout.XOffset;


        public VisualChord(IChordReader chord, double canvasLeft, double canvasTopStaffGroup, IStaffGroup staffGroup, IInstrumentMeasureReader instrumentMeasureReader, IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, ColorARGB color, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.chord = chord;
            scale = chord.Grace ? 0.5 : 1;
            this.canvasLeft = canvasLeft;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.staffGroup = staffGroup;
            this.instrumentMeasureReader = instrumentMeasureReader;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.color = color;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }



        public IEnumerable<BaseContentWrapper> GetNotes()
        {
            var notes = chord.ReadNotes();
            if (!notes.Any())
            {
                var canvasTop = staffGroup.EnumerateStaves(1).First().HeightFromLineIndex(canvasTopStaffGroup, 4, scoreLayoutDictionary);
                yield return restFactory.Build(chord, canvasLeft, canvasTop, scale, color);
                yield break;
            }

            foreach (var note in notes)
            {
                var staffIndex = scoreLayoutDictionary.GetOrDefault(note).StaffIndex;
                var clef = instrumentMeasureReader.GetClef(staffIndex, chord.Position, scoreLayoutDictionary);
                var lineIndex = clef.LineIndexAtPitch(note.Pitch);
                var offsetDots = lineIndex % 2 == 0;
                var canvasTop = staffGroup.HeightOnCanvas(canvasTopStaffGroup, staffIndex, lineIndex, scoreLayoutDictionary);
                var accidental = GetAccidental(note);
                yield return noteFactory.Build(note, canvasLeft + XOffset, canvasTop, scale, offsetDots, accidental, color);
            }
        }


        public Accidental? GetAccidental(INote note)
        {
            var noteLayout = scoreLayoutDictionary.GetOrDefault(note);
            var forceAccidental = noteLayout.ForceAccidental;
            if (forceAccidental != AccidentalDisplay.Default)
            {
                if (forceAccidental == AccidentalDisplay.ForceOn)
                {
                    return (Accidental)note.Pitch.Shift;
                }
                else
                {
                    return null;
                }
            }

            var accidental = instrumentMeasureReader.GetAccidental(note.Pitch, note.Position, noteLayout.StaffIndex, scoreLayoutDictionary);
            return accidental;
        }


        public IEnumerable<DrawableLineHorizontal> GetOverflowLines()
        {
            var notes = chord.ReadNotes();
            if (!notes.Any())
            {
                yield break;
            }

            var canvasTopStaff = canvasTopStaffGroup;
            var linesFromChord = new List<DrawableLineHorizontal>();
            var staffIndex = 0;
            var staffGroupLayout = scoreLayoutDictionary.GetOrDefault(staffGroup);
            foreach (var staff in staffGroup.EnumerateStaves(staffGroupLayout.NumberOfStaves))
            {
                var staffLayout = scoreLayoutDictionary.GetOrDefault(staff);
                var notesOnStaff = notes
                    .Where(c => scoreLayoutDictionary.GetOrDefault(c).StaffIndex == staffIndex)
                    .OrderBy(c => c.Pitch.IndexOnKlavier);
                if (notesOnStaff.Any())
                {
                    var lowestNote = notesOnStaff.First();
                    var highestNote = notesOnStaff.Last();
                    foreach (var note in new[] { highestNote, lowestNote })
                    {
                        var overflowLines = OverflowLinesFromNote(note, 2, canvasTopStaff, staff);

                        foreach (var line in overflowLines)
                        {
                            if (linesFromChord.Any(l => l.Y1.AlmostEqualTo(line.Y1)))
                            {
                                continue;
                            }

                            yield return line;
                        }
                    }
                }

                canvasTopStaff += staff.CalculateHeight(scoreLayoutDictionary);
                canvasTopStaff += staffLayout.DistanceToNext;
            }
        }
        public IEnumerable<DrawableLineHorizontal> OverflowLinesFromNote(INoteReader note, double width, double canvasTopStaff, IStaff staff)
        {
            DrawableLineHorizontal fromHeight(double height)
            {
                return new DrawableLineHorizontal(height, canvasLeft - width / 2, width, 0.1, color);
            }

            var lineIndex = instrumentMeasureReader.GetClef(staff.IndexInStaffGroup, note.Position, scoreLayoutDictionary).LineIndexAtPitch(note.Pitch);
            var overflowTop = lineIndex < -1;
            var overflowBottom = lineIndex > 9;

            if (overflowTop)
            {
                foreach (var i in Enumerable.Range(lineIndex, Math.Abs(lineIndex)))
                {
                    if (Math.Abs(i) % 2 != 0)
                    {
                        continue;
                    }

                    var height = staff.HeightFromLineIndex(canvasTopStaff, i, scoreLayoutDictionary);
                    yield return fromHeight(height);
                }
            }

            if (overflowBottom)
            {
                foreach (var i in Enumerable.Range(10, lineIndex - 9))
                {
                    if (i % 2 != 0)
                    {
                        continue;
                    }

                    var height = staff.HeightFromLineIndex(canvasTopStaff, i, scoreLayoutDictionary);
                    yield return fromHeight(height);
                }
            }
        }


        



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return GetOverflowLines();
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return GetNotes();
        }
    }
}
