namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualChord : BaseContentWrapper
    {
        private readonly IChordReader chord;
        private readonly double scale;
        private readonly double canvasLeft;
        private readonly double canvasTopStaffGroup;
        private readonly IStaffGroupReader staffGroup;
        private readonly IInstrumentMeasureReader instrumentMeasureReader;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly ColorARGB color;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        public ChordLayout Layout => scoreLayoutDictionary.ChordLayout(chord);
        public double XOffset => Layout.XOffset;


        public VisualChord(IChordReader chord, double canvasLeft, double canvasTopStaffGroup, IStaffGroupReader staffGroup, IInstrumentMeasureReader instrumentMeasureReader, IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, ColorARGB color, IScoreLayoutProvider scoreLayoutDictionary)
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
            IEnumerable<INoteReader> notes = chord.ReadNotes();
            if (!notes.Any())
            {
                var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing.Value;
                var canvasTop = staffGroup.EnumerateStaves(1).First().HeightFromLineIndex(canvasTopStaffGroup, 4, lineSpacing);
                yield return restFactory.Build(chord, canvasLeft, canvasTop, scale, color);
                yield break;
            }

            foreach (INoteReader note in notes)
            {
                int staffIndex = scoreLayoutDictionary.NoteLayout(note).StaffIndex;
                if(staffIndex >= scoreLayoutDictionary.StaffGroupLayout(staffGroup).NumberOfStaves.Value)
                {
                    continue;
                }

                var clef = instrumentMeasureReader.GetClef(staffIndex, chord.Position, scoreLayoutDictionary);
                var lineIndex = clef.LineIndexAtPitch(note.Pitch);
                var offsetDots = lineIndex % 2 == 0;
                var canvasTop = staffGroup.HeightOnCanvas(canvasTopStaffGroup, staffIndex, lineIndex, scoreLayoutDictionary);
                Accidental? accidental = GetAccidental(note);
                yield return noteFactory.Build(note, canvasLeft + XOffset, canvasTop, scale, offsetDots, accidental, color);
            }
        }


        public Accidental? GetAccidental(INoteReader note)
        {
            NoteLayout noteLayout = scoreLayoutDictionary.NoteLayout(note);
            AccidentalDisplay forceAccidental = noteLayout.ForceAccidental.Value;

            if(forceAccidental == AccidentalDisplay.ForceOff)
            {
                return null;
            }

            if(forceAccidental == AccidentalDisplay.Default)
            {
                return instrumentMeasureReader.GetAccidental(note.Pitch, note.Position, noteLayout.StaffIndex, scoreLayoutDictionary);
            }

            if (forceAccidental == AccidentalDisplay.ForceAccidental)
            {
                return note.Pitch.Shift == 0 ? null : (Accidental)note.Pitch.Shift;
            }

            return (Accidental)note.Pitch.Shift;
        }


        public IEnumerable<DrawableLineHorizontal> GetOverflowLines()
        {
            IEnumerable<INoteReader> notes = chord.ReadNotes();
            if (!notes.Any())
            {
                yield break;
            }

            var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing.Value;
            double canvasTopStaff = canvasTopStaffGroup;
            List<DrawableLineHorizontal> linesFromChord = [];
            StaffGroupLayout staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
            foreach (IStaffReader staff in staffGroup.EnumerateStaves(staffGroupLayout.NumberOfStaves.Value))
            {
                StaffLayout staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                IOrderedEnumerable<INoteReader> notesOnStaff = notes
                    .Where(c => scoreLayoutDictionary.NoteLayout(c).StaffIndex == staff.IndexInStaffGroup)
                    .OrderBy(c => c.Pitch.IndexOnKlavier);

                if (notesOnStaff.Any())
                {
                    INoteReader lowestNote = notesOnStaff.First();
                    INoteReader highestNote = notesOnStaff.Last();
                    foreach (INoteReader? note in new[] { highestNote, lowestNote })
                    {
                        IEnumerable<DrawableLineHorizontal> overflowLines = OverflowLinesFromNote(note, 2, canvasTopStaff, staff);

                        foreach (DrawableLineHorizontal line in overflowLines)
                        {
                            if (linesFromChord.Any(l => l.Y1.AlmostEqualTo(line.Y1)))
                            {
                                continue;
                            }

                            yield return line;
                        }
                    }
                }

                canvasTopStaff += staff.CalculateHeight(lineSpacing);
                canvasTopStaff += staffLayout.DistanceToNext.Value;
            }
        }
        public IEnumerable<DrawableLineHorizontal> OverflowLinesFromNote(INoteReader note, double width, double canvasTopStaff, IStaffReader staff)
        {
            DrawableLineHorizontal fromHeight(double height)
            {
                return new DrawableLineHorizontal(height, canvasLeft - (width / 2), width, 0.1, color);
            }

            var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing.Value;
            int lineIndex = instrumentMeasureReader.GetClef(staff.IndexInStaffGroup, note.Position, scoreLayoutDictionary).LineIndexAtPitch(note.Pitch);
            bool overflowTop = lineIndex < -1;
            bool overflowBottom = lineIndex > 9;

            if (overflowTop)
            {
                foreach (int i in Enumerable.Range(lineIndex, Math.Abs(lineIndex)))
                {
                    if (Math.Abs(i) % 2 != 0)
                    {
                        continue;
                    }

                    double height = staff.HeightFromLineIndex(canvasTopStaff, i, lineSpacing);
                    yield return fromHeight(height);
                }
            }

            if (overflowBottom)
            {
                foreach (int i in Enumerable.Range(10, lineIndex - 9))
                {
                    if (i % 2 != 0)
                    {
                        continue;
                    }

                    double height = staff.HeightFromLineIndex(canvasTopStaff, i, lineSpacing);
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
