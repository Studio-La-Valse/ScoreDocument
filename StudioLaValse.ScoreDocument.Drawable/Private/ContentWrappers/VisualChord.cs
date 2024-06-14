using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualChord : BaseContentWrapper
    {
        private readonly IChordReader chord;
        private readonly double canvasLeft;
        private readonly double canvasTopStaffGroup;
        private readonly double lineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IStaffGroupReader staffGroup;
        private readonly IInstrumentMeasureReader instrumentMeasureReader;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IScoreDocumentLayout scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly List<Line> overFlowLines;

        public IChordLayout Layout => chord.ReadLayout();
        public double XOffset => Layout.XOffset;


        public VisualChord(IChordReader chord,
                           double canvasLeft,
                           double canvasTopStaffGroup,
                           double lineSpacing,
                           double scoreScale,
                           double instrumentScale,
                           IStaffGroupReader staffGroup,
                           IInstrumentMeasureReader instrumentMeasureReader,
                           IVisualNoteFactory noteFactory,
                           IVisualRestFactory restFactory,
                           IScoreDocumentLayout scoreDocumentLayout,
                           IUnitToPixelConverter unitToPixelConverter)
        {
            this.chord = chord;
            this.canvasLeft = canvasLeft;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.lineSpacing = lineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.staffGroup = staffGroup;
            this.instrumentMeasureReader = instrumentMeasureReader;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
            overFlowLines = GetOverflowLines();
        }



        public IEnumerable<BaseContentWrapper> GetNotes()
        {
            var scoreScale = scoreDocumentLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;

            var notes = chord.ReadNotes();
            if (!notes.Any())
            {
                var canvasTop = canvasTopStaffGroup + unitToPixelConverter.UnitsToPixels(staffGroup.EnumerateStaves().First().DistanceFromTop(4, lineSpacing, scoreScale, instrumentScale));
                yield return restFactory.Build(chord, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale);
                yield break;
            }

            foreach (var note in notes)
            {
                var staffIndex = note.ReadLayout().StaffIndex;
                if (staffIndex >= staffGroup.ReadLayout().NumberOfStaves)
                {
                    continue;
                }

                var clef = instrumentMeasureReader.GetClef(staffIndex, chord.Position);
                var lineIndex = clef.LineIndexAtPitch(note.Pitch);
                var offsetDots = lineIndex % 2 == 0;
                var canvasTop = canvasTopStaffGroup + unitToPixelConverter.UnitsToPixels(staffGroup.DistanceFromTop(staffIndex, lineIndex, lineSpacing, scoreDocumentLayout));
                var accidental = GetAccidental(note);
                yield return noteFactory.Build(note, canvasLeft + XOffset, canvasTop, lineSpacing, scoreScale, instrumentScale, offsetDots, accidental);
            }
        }


        public Accidental? GetAccidental(INoteReader note)
        {
            var noteLayout = note.ReadLayout();
            var forceAccidental = noteLayout.ForceAccidental;

            if (forceAccidental == AccidentalDisplay.ForceOff)
            {
                return null;
            }

            if (forceAccidental == AccidentalDisplay.Default)
            {
                return instrumentMeasureReader.GetAccidental(note.Pitch, note.Position, noteLayout.StaffIndex);
            }

            if (forceAccidental == AccidentalDisplay.ForceAccidental)
            {
                return note.Pitch.Shift == 0 ? null : (Accidental)note.Pitch.Shift;
            }

            return (Accidental)note.Pitch.Shift;
        }


        public List<Line> GetOverflowLines()
        {
            var notes = chord.ReadNotes();
            if (!notes.Any())
            {
                return [];
            }

            var canvasTopStaff = canvasTopStaffGroup;
            List<Line> linesFromChord = [];
            var staffGroupLayout = staffGroup.ReadLayout();
            foreach (var staff in staffGroup.EnumerateStaves())
            {
                var staffLayout = staff.ReadLayout();
                var notesOnStaff = notes
                    .Where(c => c.ReadLayout().StaffIndex == staff.IndexInStaffGroup)
                    .OrderBy(c => c.Pitch.IndexOnKlavier);

                if (notesOnStaff.Any())
                {
                    var lowestNote = notesOnStaff.First();
                    var highestNote = notesOnStaff.Last();
                    foreach (var note in new[] { highestNote, lowestNote })
                    {
                        var overflowLines = OverflowLinesFromNote(note, 2.5 * scoreScale * instrumentScale, canvasTopStaff, staff);

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

                canvasTopStaff += unitToPixelConverter.UnitsToPixels(staff.CalculateHeight(lineSpacing, scoreScale, instrumentScale));
                canvasTopStaff += unitToPixelConverter.UnitsToPixels(staffLayout.DistanceToNext);
            }

            return linesFromChord;
        }
        public IEnumerable<Line> OverflowLinesFromNote(INoteReader note, double width, double canvasTopStaff, IStaffReader staff)
        {
            Line fromHeight(double height)
            {
                return new Line(canvasLeft - (width / 2), height, canvasLeft + (width / 2), height);
            }

            var lineIndex = instrumentMeasureReader.GetClef(staff.IndexInStaffGroup, note.Position).LineIndexAtPitch(note.Pitch);
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

                    var height = canvasTopStaff + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(i, lineSpacing, scoreScale, instrumentScale));
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

                    var height = canvasTopStaff + unitToPixelConverter.UnitsToPixels(staff.DistanceFromTop(i, lineSpacing, scoreScale, instrumentScale));
                    yield return fromHeight(height);
                }
            }
        }






        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return overFlowLines.Select(l => 
            {
                return new DrawableLineHorizontal(l.Start.Y,
                                                  l.Start.X,
                                                  l.Start.DistanceTo(l.End),
                                                  scoreDocumentLayout.HorizontalStaffLineThickness * scoreScale * instrumentScale,
                                                  scoreDocumentLayout.PageForegroundColor.FromPrimitive());
            });
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return GetNotes();
        }
    }
}
