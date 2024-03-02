using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using StudioLaValse.ScoreDocument.Drawable.Private.Models;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal class VisualNoteGroup : BaseContentWrapper
    {
        private readonly IMeasureBlockReader measureBlock;
        private readonly IStaffGroupReader staffGroup;
        private readonly IInstrumentMeasureReader instrumentMeasure;
        private readonly double canvasTopStaffGroup;
        private readonly double canvasLeft;
        private readonly double allowedSpace;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualBeamBuilder visualBeamBuilder;
        private readonly ColorARGB colorARGB;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        public MeasureBlockLayout Layout => scoreLayoutDictionary.MeasureBlockLayout(measureBlock);



        public VisualNoteGroup(IMeasureBlockReader measureBlock, IStaffGroupReader staffGroup, IInstrumentMeasureReader instrumentMeasure, double canvasTopStaffGroup, double canvasLeft, double allowedSpace, IVisualNoteFactory noteFactory, IVisualRestFactory restFactory, IVisualBeamBuilder visualBeamBuilder, ColorARGB colorARGB, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.measureBlock = measureBlock;
            this.staffGroup = staffGroup;
            this.instrumentMeasure = instrumentMeasure;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.canvasLeft = canvasLeft;
            this.allowedSpace = allowedSpace;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualBeamBuilder = visualBeamBuilder;
            this.colorARGB = colorARGB;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        public IEnumerable<BaseContentWrapper> Create()
        {
            var chords = measureBlock.ReadChords().ToArray();
            if (chords.Length == 0)
            {
                yield break;
            }

            var note = GlyphLibrary.NoteHeadBlack;
            note.Scale = 1;

            var canvasLeft = this.canvasLeft;
            var noteWidth = note.Width;
            var firstChord = chords.First();
            var firstStemUp = Layout.StemLength > 0;
            var firstStemOrigin = ConstructStemOrigin(firstChord, staffGroup, canvasTopStaffGroup, canvasLeft, firstStemUp, noteWidth);
            var firstStemTip = new XY(firstStemOrigin.X, firstStemOrigin.Y + Layout.StemLength);
            var beamDefinition = new Ruler(firstStemTip, 0);
            var stems = new List<VisualStem>();
            var groupLength = chords.Select(c => c.RythmicDuration).Sum().Decimal;
            foreach (var chord in chords)
            {
                yield return new VisualChord(chord, canvasLeft, canvasTopStaffGroup, staffGroup, instrumentMeasure, noteFactory, restFactory, colorARGB, scoreLayoutDictionary);

                if (chord.ReadNotes().Any(note => note.RythmicDuration.Decimal <= 0.5M))
                {
                    var chordOrigin = ConstructChordOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, true);
                    var stemIntersection = beamDefinition.IntersectVerticalRay(chordOrigin);
                    var stemUp = stemIntersection.Y < chordOrigin.Y;
                    var stemOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, stemUp, noteWidth);
                    stemIntersection = beamDefinition.IntersectVerticalRay(stemOrigin);

                    var visualStem = new VisualStem(stemOrigin, stemIntersection, chord, ColorARGB.Black);
                    stems.Add(visualStem);
                }

                canvasLeft += MathUtils.Map((double)chord.RythmicDuration.Decimal, 0d, (double)groupLength, 0, allowedSpace);
            }

            yield return new VisualBeamGroup(stems, beamDefinition, measureBlock.Grace ? 0.5 : 1, colorARGB, visualBeamBuilder);
        }

        public XY ConstructStemOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp, double noteWidth)
        {
            var chordOrigin = ConstructChordOrigin(chord, staffGroup, staffGroupCanvasTop, chordCanvasLeft, stemUp);
            var offset = stemUp ?
                noteWidth / 2 :
                -noteWidth / 2;

            return new XY(chordOrigin.X + offset, chordOrigin.Y);
        }

        public XY ConstructChordOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp)
        {
            var staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);

            if (!chord.ReadNotes().Any())
            {
                var line = 4;
                var heightOnCanvas = staffGroup.EnumerateStaves(staffGroupLayout.NumberOfStaves).First().HeightFromLineIndex(staffGroupCanvasTop, line, scoreLayoutDictionary);
                return new XY(chordCanvasLeft, heightOnCanvas);
            }

            var notesFromVisualHighToLow = chord
                .ReadNotes()
                .OrderBy(note => scoreLayoutDictionary.NoteLayout(note).StaffIndex)
                .ThenByDescending(n => n.Pitch.IndexOnKlavier);

            var anchorNote = stemUp ?
                notesFromVisualHighToLow.Last() :
                notesFromVisualHighToLow.First();

            var noteLayout = scoreLayoutDictionary.NoteLayout(anchorNote);
            var clef = instrumentMeasure.GetClef(noteLayout.StaffIndex, anchorNote.Position, scoreLayoutDictionary);
            var staff = staffGroup.EnumerateStaves(staffGroupLayout.NumberOfStaves).ElementAt(scoreLayoutDictionary.NoteLayout(anchorNote).StaffIndex);
            var heightOriginOnCanvas = staffGroup.HeightOnCanvas(staffGroupCanvasTop, noteLayout.StaffIndex, clef.LineIndexAtPitch(anchorNote.Pitch), scoreLayoutDictionary);
            heightOriginOnCanvas += stemUp ?
                -0.2 : 0.2;

            var horizontalPositionStart = chordCanvasLeft;
            return new XY(horizontalPositionStart, heightOriginOnCanvas);
        }

        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return [];
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return Create();
        }
    }
}
