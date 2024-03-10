using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;

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
            IChordReader[] chords = measureBlock.ReadChords().ToArray();
            if (chords.Length == 0)
            {
                yield break;
            }

            Glyph note = GlyphLibrary.NoteHeadBlack;
            note.Scale = 1;

            double canvasLeft = this.canvasLeft;
            double noteWidth = note.Width;
            IChordReader firstChord = chords.First();
            bool firstStemUp = Layout.StemLength.Value > 0;
            XY firstStemOrigin = ConstructStemOrigin(firstChord, staffGroup, canvasTopStaffGroup, canvasLeft, firstStemUp, noteWidth);
            XY firstStemTip = new(firstStemOrigin.X, firstStemOrigin.Y + Layout.StemLength.Value);
            Ruler beamDefinition = new(firstStemTip, Layout.BeamAngle.Value);
            List<VisualStem> stems = [];
            decimal groupLength = chords.Select(c => c.RythmicDuration).Sum().Decimal;
            foreach (IChordReader? chord in chords)
            {
                yield return new VisualChord(chord, canvasLeft, canvasTopStaffGroup, staffGroup, instrumentMeasure, noteFactory, restFactory, colorARGB, scoreLayoutDictionary);

                if (chord.ReadNotes().Any(note => note.RythmicDuration.Decimal <= 0.5M))
                {
                    XY chordOrigin = ConstructChordOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, true);
                    XY stemIntersection = beamDefinition.IntersectVerticalRay(chordOrigin);
                    bool stemUp = stemIntersection.Y < chordOrigin.Y;
                    XY stemOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, stemUp, noteWidth);
                    stemIntersection = beamDefinition.IntersectVerticalRay(stemOrigin);

                    VisualStem visualStem = new(stemOrigin, stemIntersection, chord, ColorARGB.Black);
                    stems.Add(visualStem);
                }

                canvasLeft += MathUtils.Map((double)chord.RythmicDuration.Decimal, 0d, (double)groupLength, 0, allowedSpace);
            }

            yield return new VisualBeamGroup(stems, beamDefinition, measureBlock.Grace ? 0.5 : 1, colorARGB, visualBeamBuilder);
        }

        public XY ConstructStemOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp, double noteWidth)
        {
            XY chordOrigin = ConstructChordOrigin(chord, staffGroup, staffGroupCanvasTop, chordCanvasLeft, stemUp);
            double offset = stemUp ?
                noteWidth / 2 :
                -noteWidth / 2;

            return new XY(chordOrigin.X + offset, chordOrigin.Y);
        }

        public XY ConstructChordOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp)
        {
            StaffGroupLayout staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);

            if (!chord.ReadNotes().Any())
            {
                int line = 4;
                var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing.Value;
                double heightOnCanvas = staffGroup.EnumerateStaves(1).First().HeightFromLineIndex(staffGroupCanvasTop, line, lineSpacing);
                return new XY(chordCanvasLeft, heightOnCanvas);
            }

            IOrderedEnumerable<INoteReader> notesFromVisualHighToLow = chord
                .ReadNotes()
                .OrderBy(note => scoreLayoutDictionary.NoteLayout(note).StaffIndex)
                .ThenByDescending(n => n.Pitch.IndexOnKlavier);

            INoteReader anchorNote = stemUp ?
                notesFromVisualHighToLow.Last() :
                notesFromVisualHighToLow.First();

            NoteLayout noteLayout = scoreLayoutDictionary.NoteLayout(anchorNote);
            Clef clef = instrumentMeasure.GetClef(noteLayout.StaffIndex, anchorNote.Position, scoreLayoutDictionary);
            int noteStaffIndex = scoreLayoutDictionary.NoteLayout(anchorNote).StaffIndex;
            IStaffReader staff = staffGroup.EnumerateStaves(noteStaffIndex + 1).ElementAt(noteStaffIndex);
            double heightOriginOnCanvas = staffGroup.HeightOnCanvas(staffGroupCanvasTop, noteLayout.StaffIndex, clef.LineIndexAtPitch(anchorNote.Pitch), scoreLayoutDictionary);
            heightOriginOnCanvas += stemUp ?
                -0.2 : 0.2;

            double horizontalPositionStart = chordCanvasLeft;
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
