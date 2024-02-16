using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents
{
    internal class VisualNoteGroup : BaseContentWrapper
    {
        private readonly IMeasureBlockReader noteGroup;
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasTopStaffGroup;
        private readonly double canvasLeft;
        private readonly double allowedSpace;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualBeamBuilder visualBeamBuilder;
        private readonly ColorARGB colorARGB;

        public VisualNoteGroup(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double allowedSpace, IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, IVisualBeamBuilder visualBeamBuilder, ColorARGB colorARGB)
        {
            this.noteGroup = noteGroup;
            this.staffGroup = staffGroup;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.canvasLeft = canvasLeft;
            this.allowedSpace = allowedSpace;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualBeamBuilder = visualBeamBuilder;
            this.colorARGB = colorARGB;
        }

        public IEnumerable<BaseContentWrapper> Create(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double allowedSpace)
        {
            if (!noteGroup.ReadChords().Any())
            {
                yield break;
            }

            var note = GlyphLibrary.NoteHeadBlack;
            note.Scale = 1;

            var noteWidth = note.Width;
            var firstChord = noteGroup.ReadChords().First();
            var firstStemUp = noteGroup.ReadLayout().StemLength > 0;
            var firstStemOrigin = ConstructStemOrigin(firstChord, staffGroup, canvasTopStaffGroup, canvasLeft, firstStemUp, noteWidth);
            var firstStemTip = new XY(firstStemOrigin.X, firstStemOrigin.Y + noteGroup.ReadLayout().StemLength);
            var beamDefinition = new Ruler(firstStemTip, 0);
            var stems = new List<VisualStem>();
            var groupLength = noteGroup.EnumerateChords().Select(c => c.RythmicDuration).Sum().Decimal;

            foreach (var chord in noteGroup.ReadChords())
            {
                var xOffset = chord.ReadLayout().XOffset;   
                yield return new VisualChord(chord, canvasLeft + xOffset, canvasTopStaffGroup, staffGroup, noteFactory, restFactory, colorARGB);

                if (chord.ReadNotes().Any(note => note.RythmicDuration.Decimal <= 0.5M))
                {
                    var chordOrigin = ConstructChordOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, true);
                    var stemIntersection = beamDefinition.IntersectVerticalRay(chordOrigin);
                    var stemUp = stemIntersection.Y < chordOrigin.Y;
                    var stemOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, stemUp, noteWidth);
                    stemIntersection = beamDefinition.IntersectVerticalRay(stemOrigin);

                    var visualStem = new VisualStem(stemOrigin, stemIntersection, chord.ReadLayout().Beams, ColorARGB.Black);
                    stems.Add(visualStem);
                }

                canvasLeft += MathUtils.Map((double)chord.RythmicDuration.Decimal, 0d, (double)groupLength, 0, allowedSpace);
            }

            yield return new VisualBeamGroup(stems, beamDefinition, noteGroup.Grace ? 0.5 : 1, colorARGB, visualBeamBuilder);
        }

        public static XY ConstructStemOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp, double noteWidth)
        {
            var chordOrigin = ConstructChordOrigin(chord, staffGroup, staffGroupCanvasTop, chordCanvasLeft, stemUp);
            var offset = stemUp ?
                noteWidth / 2 :
                -noteWidth / 2;

            return new XY(chordOrigin.X + offset, chordOrigin.Y);
        }

        public static XY ConstructChordOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp)
        {
            if (!chord.ReadNotes().Any())
            {
                var line = 4;
                var heightOnCanvas = staffGroup.ReadStaves().First().HeightFromLineIndex(staffGroupCanvasTop, line);
                return new XY(chordCanvasLeft, heightOnCanvas);
            }

            var notesFromVisualHighToLow = chord
                .ReadNotes()
                .OrderBy(note => note.ReadLayout().StaffIndex)
                .ThenByDescending(n => n.Pitch.IndexOnKlavier);

            var anchorNote = stemUp ?
                notesFromVisualHighToLow.Last() :
                notesFromVisualHighToLow.First();

            var staff = staffGroup.ReadStaves().ElementAt(anchorNote.ReadLayout().StaffIndex);
            var heightOriginOnCanvas = anchorNote.HeightOnCanvas(staffGroup, staffGroupCanvasTop);
            heightOriginOnCanvas += stemUp ?
                -0.2 : 0.2;

            var horizontalPositionStart = chordCanvasLeft;
            return new XY(horizontalPositionStart, heightOriginOnCanvas);
        }

        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>();
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return Create(noteGroup, staffGroup, canvasTopStaffGroup, canvasLeft, allowedSpace);
        }
    }
}
