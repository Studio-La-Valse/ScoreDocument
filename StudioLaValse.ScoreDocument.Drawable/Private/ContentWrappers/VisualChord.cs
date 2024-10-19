using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualChord : BaseContentWrapper
    {
        private readonly double canvasLeft;
        private readonly double canvasTopStaffGroup;
        private readonly IChord chord;
        private readonly IStaffGroup staffGroup;
        private readonly IInstrumentMeasure instrumentMeasure;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly bool drawDirectionUp;
        

        public double Scale => chord.Scale;
        public RythmicDuration DisplayDuration => chord.RythmicDuration;
        public Glyph Glyph
        {
            get
            {
                var glyph = DisplayDuration.PowerOfTwo.Value switch
                {
                    1 => glyphLibrary.NoteHeadWhole(Scale),
                    2 => glyphLibrary.NoteHeadWhite(Scale),
                    _ => glyphLibrary.NoteHeadBlack(Scale)
                };

                return glyph;
            }
        }

        public VisualChord(IChord chord,
                           double canvasLeft,
                           double canvasTopStaffGroup,
                           IStaffGroup staffGroup,
                           IInstrumentMeasure instrumentMeasureReader,
                           IVisualNoteFactory noteFactory,
                           IVisualRestFactory restFactory,
                           IGlyphLibrary glyphLibrary,
                           bool drawDirectionUp)
        {
            this.chord = chord;
            this.canvasLeft = canvasLeft;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.staffGroup = staffGroup;
            this.instrumentMeasure = instrumentMeasureReader;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.glyphLibrary = glyphLibrary;
            this.drawDirectionUp = drawDirectionUp;
        }



        public IEnumerable<BaseContentWrapper> GetNotes()
        {
            var notes = this.chord.ReadNotes();
            var restStaffIndex = this.chord.StaffIndex;
            var restIsVisible = restStaffIndex >= staffGroup.NumberOfStaves.Value;
            if (!notes.Any() && restIsVisible)
            {
                var restLineIndex = this.chord.Line;
                var canvasTop = canvasTopStaffGroup + staffGroup.DistanceFromTop(restStaffIndex, restLineIndex);

                yield return restFactory.Build(chord, canvasLeft, canvasTop);
                yield break;
            }

            var noteMirror = NoteMirror.NoMirror;
            var previousNoteMirror = NoteMirror.NoMirror;
            var previousStaff = -1;
            var previousLine = -1;

            var notesWithLineIndex = notes.Select(note =>
            {
                var clef = instrumentMeasure.GetClef(note.StaffIndex, note.Position);
                var lineIndex = clef.LineIndexAtPitch(note.Pitch);
                return (note, clef, lineIndex);
            });
            notesWithLineIndex = drawDirectionUp ?
                notesWithLineIndex.OrderByDescending(x => x.note.StaffIndex.Value).ThenByDescending(x => x.lineIndex) :
                notesWithLineIndex.OrderBy(x => x.note.StaffIndex.Value).ThenBy(x => x.lineIndex);

            var glyphWidth = Glyph.Width();
            foreach (var (note, clef, lineIndex) in notesWithLineIndex)
            {
                var canvasLeft = this.canvasLeft;
                var noteStaffIndex = note.StaffIndex;
                var noteIsVisible = noteStaffIndex < staffGroup.NumberOfStaves;
                if (!noteIsVisible)
                {
                    continue;
                }

                var canvasTop = canvasTopStaffGroup + staffGroup.DistanceFromTop(noteStaffIndex, lineIndex);
                var accidental = GetAccidental(note, instrumentMeasure);

                noteMirror = NoteMirror.NoMirror;
                if (previousStaff == noteStaffIndex && Math.Abs(previousLine - lineIndex) == 1 && previousNoteMirror == NoteMirror.NoMirror)
                {
                    noteMirror = drawDirectionUp ? NoteMirror.Right : NoteMirror.Left;
                }

                var offset = noteMirror switch
                {
                    NoteMirror.NoMirror => 0,
                    NoteMirror.Left => glyphWidth * -1,
                    NoteMirror.Right => glyphWidth,
                    _ => throw new NotImplementedException()
                };
                canvasLeft += offset;

                yield return noteFactory.Build(note, clef, accidental, canvasLeft, canvasTop);

                previousLine = lineIndex;
                previousStaff = noteStaffIndex;
                previousNoteMirror = noteMirror;
            }
        }

        public Accidental? GetAccidental(INote note, IInstrumentMeasure instrumentMeasure)
        {
            var noteLayout = note;
            var forceAccidental = noteLayout.ForceAccidental;

            if (forceAccidental == AccidentalDisplay.ForceOff)
            {
                return null;
            }

            if (forceAccidental == AccidentalDisplay.Default)
            {
                return instrumentMeasure.GetAccidental(note.Pitch, note.Position, noteLayout.StaffIndex);
            }

            if (forceAccidental == AccidentalDisplay.ForceAccidental)
            {
                return note.Pitch.Shift == 0 ? null : (Accidental)note.Pitch.Shift;
            }

            return (Accidental)note.Pitch.Shift;
        }




        public List<Line> GetOverflowLines()
        {
            var notes = this.chord.ReadNotes();
            if (!notes.Any())
            {
                return [];
            }

            var linesFromChord = new List<Line>();
            var staffGroupLayout = staffGroup;

            foreach (var (staff, canvasTopStaff) in staffGroup.EnumerateFromTop(this.canvasTopStaffGroup))
            {
                var staffLayout = staff;
                var notesOnStaff = notes
                    .Where(c => c.StaffIndex == staff.IndexInStaffGroup)
                    .OrderBy(c => c.Pitch.IndexOnKlavier);

                if (notesOnStaff.Any())
                {
                    var lowestNote = notesOnStaff.First();
                    var highestNote = notesOnStaff.Last();
                    foreach (var note in new[] { highestNote, lowestNote })
                    {
                        var lineLength = chord.SpaceRight * 0.5;
                        var overflowLines = OverflowLinesFromNote(note, lineLength, canvasTopStaff, staff);

                        foreach (var line in overflowLines)
                        {
                            if (linesFromChord.Any(l => l.Start.Y.AlmostEqualTo(line.Start.Y)))
                            {
                                continue;
                            }

                            linesFromChord.Add(line);
                        }
                    }
                }
            }

            return linesFromChord;
        }
        public IEnumerable<Line> OverflowLinesFromNote(INote note, double width, double canvasTopStaff, IStaff staff)
        {
            Line fromHeight(double height)
            {
                return new Line(canvasLeft - (width / 2), height, canvasLeft + (width / 2), height);
            }

            var lineIndex = instrumentMeasure.GetClef(staff.IndexInStaffGroup, note.Position).LineIndexAtPitch(note.Pitch);
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

                    var height = canvasTopStaff + staff.DistanceFromTop(i);
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

                    var height = canvasTopStaff + staff.DistanceFromTop(i);
                    yield return fromHeight(height);
                }
            }
        }






        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return GetOverflowLines().Select(l => 
            {
                return new DrawableLineHorizontal(
                    l.Start.Y,
                    l.Start.X,
                    l.Start.DistanceTo(l.End),
                    staffGroup.HorizontalStaffLineThickness * staffGroup.Scale,
                    staffGroup.Color.Value.FromPrimitive());
            });
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return GetNotes();
        }
    }
}
