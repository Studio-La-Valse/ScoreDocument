using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Models;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.VisualParents
{
    public class VisualNoteGroup : BaseContentWrapper
    {
        private readonly IMeasureBlockReader noteGroup;
        private readonly IStaffGroupReader staffGroup;
        private readonly double canvasTopStaffGroup;
        private readonly double canvasLeft;
        private readonly double spacing;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualBeamBuilder visualBeamBuilder;
        private readonly ColorARGB colorARGB;

        public VisualNoteGroup(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double spacing, IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, IVisualBeamBuilder visualBeamBuilder, ColorARGB colorARGB)
        {
            this.noteGroup = noteGroup;
            this.staffGroup = staffGroup;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.canvasLeft = canvasLeft;
            this.spacing = spacing;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualBeamBuilder = visualBeamBuilder;
            this.colorARGB = colorARGB;
        }

        public IEnumerable<BaseContentWrapper> Create(IMeasureBlockReader noteGroup, IStaffGroupReader staffGroup, double canvasTopStaffGroup, double canvasLeft, double spacing)
        {
            var contentWrappers = new List<BaseContentWrapper>();
            if (!noteGroup.ReadChords().Any())
            {
                return contentWrappers;
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

            foreach (var chord in noteGroup.ReadChords())
            {
                var visualChord = new VisualChord(chord, canvasLeft, canvasTopStaffGroup, staffGroup, noteFactory, restFactory, colorARGB);
                contentWrappers.Add(visualChord);

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

                canvasLeft += spacing;
            }

            var beamGroup = new VisualBeamGroup(stems, beamDefinition, noteGroup.Grace ? 0.5 : 1, colorARGB, visualBeamBuilder);
            contentWrappers.Add(beamGroup);

            return contentWrappers;
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

            var heightOriginOnCanvas = anchorNote.HeightOnCanvas(staffGroup, staffGroupCanvasTop);
            var horizontalPositionStart = chordCanvasLeft;
            return new XY(horizontalPositionStart, heightOriginOnCanvas);
        }

        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>();
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return Create(noteGroup, staffGroup, canvasTopStaffGroup, canvasLeft, spacing);
        }
    }
}
