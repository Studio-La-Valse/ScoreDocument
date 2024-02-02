﻿namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.ContentWrappers
{
    internal sealed class VisualChord : BaseContentWrapper
    {
        private readonly IChordReader chord;
        private readonly double scale;
        private readonly double canvasLeft;
        private readonly double canvasTopStaffGroup;
        private readonly IStaffGroupReader staffGroup;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly ColorARGB color;






        public VisualChord(IChordReader chord, double canvasLeft, double canvasTopStaffGroup, IStaffGroupReader staffGroup, IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, ColorARGB color)
        {
            this.chord = chord;
            scale = chord.Grace ? 0.5 : 1;
            this.canvasLeft = canvasLeft;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.staffGroup = staffGroup;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.color = color;
        }



        public IEnumerable<BaseContentWrapper> GetNotes()
        {
            var notes = chord.ReadNotes();
            if (!notes.Any())
            {
                var canvasTop = staffGroup.ReadStaves().First().HeightFromLineIndex(canvasTopStaffGroup, 4);
                yield return restFactory.Build(chord, canvasLeft, canvasTop, scale, color);
                yield break;
            }

            foreach (var note in notes)
            {
                var canvasTop = note.HeightOnCanvas(staffGroup, canvasTopStaffGroup);
                yield return noteFactory.Build(note, canvasLeft, canvasTop, scale, color);
            }
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
            foreach (var staff in staffGroup.ReadStaves())
            {
                var notesOnStaff = notes
                    .Where(c => c.ReadLayout().StaffIndex == staffIndex)
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

                canvasTopStaff += staff.CalculateHeight();
                canvasTopStaff += staff.ReadLayout().DistanceToNext;
            }
        }
        public IEnumerable<DrawableLineHorizontal> OverflowLinesFromNote(INoteReader note, double width, double canvasTopStaff, IStaffReader staff)
        {
            DrawableLineHorizontal fromHeight(double height)
            {
                return new DrawableLineHorizontal(height, canvasLeft - width / 2, width, 0.1, color);
            }

            var lineIndex = note.GetClef().LineIndexAtPitch(note.Pitch);
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

                    var height = staff.HeightFromLineIndex(canvasTopStaff, i);
                    yield return (fromHeight(height));
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

                    var height = staff.HeightFromLineIndex(canvasTopStaff, i);
                    yield return (fromHeight(height));
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
